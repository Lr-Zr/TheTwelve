using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;
using Unity.VisualScripting;
using UnityEngine.Playables;
using System.IO.IsolatedStorage;

namespace nara
{

    public class PlayerCtrller : MonoBehaviourPun, IPunObservable
    {


        //�̵��ӵ�
        [SerializeField]
        float _MoveSpeed = 10.0f;

        //������
        [SerializeField]
        float _JumpingPower = 10.0f;

        //������
        [SerializeField]
        float _Gauge = 1.0f;

        //���
        [SerializeField]
        int _Life = 3;

        //�̲������� �ӵ�
        [SerializeField]
        float _SlideSpeed = 50.0f;

        //�ϰ�Ű ������ ���ǵ�
        [SerializeField]
        float _DownSpeed = 3.0f;

        //���� ��  �ൿ����
        [SerializeField]
        float _JumpRestriction = 0.25f;

        //���� ���� �ð�
        [SerializeField]
        float _RunRestriction = 1.0f;

        //���� ���� ���� �ð�
        [SerializeField]
        float _AttackRestriction = 0.75f;

        //�����ð�
        [SerializeField]
        float _SlideTime = 0.25f;

        [SerializeField]
        float _AtkStopTime = 0.25f;

        //�Ŀ�
        [SerializeField]
        Vector3[] _Power;





        PhotonView _Pv;
        Rigidbody _Rigid;
        PlayerAnimation _Anim;
        PlayerEffect _Eff;
        PlayerState _State;
        //���� �¿� move�Լ���
        int dir;
        //time
        public float _RunTime = 0.0f;
        float _JumpTime = 0.3f;
        float _Floortime = 0.0f;//
        float _AttackTime = 0.5f;


        bool _IsAttack;
        bool _IsJump;
        bool _IsDJump;
        bool _IsOnesec;//���� ���� 
        bool _IsDashAtk;
        bool _IsRunning;
        bool _Stop;


        //keydown time;
        float _KDwTime = 0.0f;
        float _KUpTime = 0.0f;

        //keydown bool;
        bool _IsKeyDown;
        bool _IsKeyUp;


        Vector3 _PrePos;
        void Start()
        {

            _Pv = GetComponent<PhotonView>();
            _Rigid = GetComponent<Rigidbody>();
            _Anim = GetComponent<PlayerAnimation>();
            _Eff = GetComponent<PlayerEffect>();

            GameMgr.Input.KeyAction -= OnKeyboard;
            GameMgr.Input.KeyAction += OnKeyboard;
            _State = PlayerState.Idle;
            _IsJump = false;
            _IsDJump = false;
            _IsOnesec = false;
            _IsRunning = false;
            _IsKeyDown = false;
            _IsKeyUp = false;
            _IsAttack = false;

        }

        private void FixedUpdate()
        {
            //�޸��ٰ� ���ߴ� ����
            _Floortime += Time.deltaTime;

            //������ �� ���� ���� ������ ������ �ð�
            _AttackTime += Time.deltaTime;
            if (_AttackTime > _AtkStopTime)
            {
                _IsAttack = false;
                _Anim.SetIsAttack(_IsAttack);
            }
            //Ŀ�ǵ� Ű�Է�
            _KUpTime += Time.deltaTime;
            _KDwTime += Time.deltaTime;
            if (_KUpTime > 0.2f) _IsKeyUp = false;
            if (_KDwTime > 0.2f) _IsKeyDown = false;


            //ü�� �ð�
            if (_IsJump)
                _JumpTime += Time.deltaTime;

            //�극��ŷ ���� ���� ���� RunTime;
            if (_RunTime > _RunRestriction)
            {
                _RunTime = _RunRestriction;
                _IsOnesec = true;
            }

            if (_RunTime > _RunRestriction)
            {
                _IsDashAtk = true;
            }
            else
                _IsDashAtk = false;
            //���ϻ��� 
            if (_Rigid.velocity.y < -0.05f && _IsJump)//����
            {
                _State = PlayerState.Falling;
                _Anim.SetAnim(_State);
            }
            
            
        }
        void Update()
        {
            //�ٴڿ� �ִ��� üũ �Լ�;
            OnFloor();
            //�극��ŷ �κ�
            Breaking();


        }

        void OnKeyboard()
        {
            //if (!_Pv.IsMine) return;

            if (Input.GetKey(KeyCode.DownArrow))//���ձ� �� �� �ϰ� �ӵ� ���
            {
                _IsKeyDown = true;
                _IsKeyUp = false;
                _KDwTime = 0;

                if (_IsJump && _JumpTime > _JumpRestriction)
                    Move(0);
            }

            else if (Input.GetKey(KeyCode.UpArrow))//����Ű ��
            {
                _KUpTime = 0;
                _IsKeyDown = false;
                _IsKeyUp = true;


            }


            if (Input.GetKey(KeyCode.LeftArrow))//�� �̵�
            {
                /*�̵� �� ������ȯ*/

                /* ������ �޸� �� */

                if (!_IsJump)
                {
                    _RunTime += Time.deltaTime;
                    dir = -1;
                    _IsRunning = true;
                    _State = PlayerState.Running;
                    _Anim.SetAnim(_State);

                    _Eff.EffectPlay(Effect.LRun);
                }
                Move(-1);

            }



            if (Input.GetKey(KeyCode.RightArrow))//�� �̵�
            {

                /* ������ �޸� �� */
                if (!_IsJump)
                {
                    _IsRunning = true;
                    _State = PlayerState.Running;
                    _Anim.SetAnim(_State);

                    dir = 1;
                    _RunTime += Time.deltaTime;
                    _Eff.EffectPlay(Effect.RRun);
                }
                /*�̵� �� ������ȯ*/
                Move(1);

            }





            if (Input.GetKey(KeyCode.Q)) //����
            {
                _AttackTime = 0;
                _Anim.TriggerAtk();

                _IsAttack = true;
                _Anim.SetIsAttack(_IsAttack);
            }

            else if (Input.GetKey(KeyCode.W))//��ų
            {

            }

            else if (Input.GetKey(KeyCode.E))//���
            {

            }

            else if (Input.GetKey(KeyCode.R))//���
            {

            }






            /* ���� */
            if (Input.GetKey(KeyCode.Space))
            {
                _RunTime = 0;
                if (!_IsJump && _State != PlayerState.Falling)
                {
                    Jump();
                    _State = PlayerState.Jumping;
                    _IsJump = true;

                    _Anim.SetAnim(_State);
                    _Anim.SetIsJump(_IsJump);
                    _JumpTime = 0;

                    if (dir > 0)
                        _Eff.EffectPlay(Effect.RJump);
                    else
                        _Eff.EffectPlay(Effect.LJump);
                }
                else if (!_IsDJump && _JumpTime > 0.25)
                {
                    Jump();
                    _State = PlayerState.DoudbleJumping;
                    _IsDJump = true;
                    _Anim.SetAnim(_State);
                    _Anim.SetIsDJump(_IsDJump);
                    _JumpTime = 0;

                    _Eff.EffectPlay(Effect.DJump);
                }

                _IsRunning = false;

            }










        }


        void Jump()
        {
            _Rigid.velocity = Vector3.zero;
            _Rigid.AddForce(Vector3.up * _JumpingPower, ForceMode.Impulse);
        }






        void Move(int dir)      /*�̵� �� ������ȯ*/
        {

            if (!_IsJump)
            {
                if (dir == 0) return;
                this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right * dir), 1f);
                this.transform.position += transform.forward * _MoveSpeed * Time.deltaTime;
            }
            else
            {
                if (dir == 0)
                {
                    this.transform.position -= Vector3.up * _DownSpeed * Time.deltaTime;

                }
                else
                {
                    this.transform.position += Vector3.right * dir * _MoveSpeed / 2.0f* Time.deltaTime;

                }
            }

        }




        void Breaking()
        {
            //�޸��ٰ� ���߸� �̲�����
            if (_IsRunning&&_IsOnesec)//ź�� ȿ��
            {
                _RunTime -= Time.deltaTime;
                if (_RunTime > _RunRestriction - _SlideTime)
                {
                    //Debug.Log("�̵�");
                    _Rigid.AddForce(this.transform.forward * _SlideSpeed);
                }
                else
                {
                    _RunTime = 0f;
                    _IsRunning = false;
                    _IsOnesec = false;
                    if (dir > 0)
                        _Eff.EffectPlay(Effect.RBreak);
                    else
                        _Eff.EffectPlay(Effect.LBreak);
                }

            }
            _PrePos = transform.position;
        }

        void OnFloor()
        {
            RaycastHit hit;
            Debug.DrawRay(this.transform.position, this.transform.up * -0.2f, Color.green);
            LayerMask mask = LayerMask.GetMask("Floor");
            if (Physics.Raycast(this.transform.position, this.transform.up * -1, out hit, 0.2f, mask))
            {
                if (_State != PlayerState.Idle && _JumpTime > _JumpRestriction && _Floortime > 0.05)
                {
                    if (_IsJump)
                        _Eff.EffectPlay(Effect.Land);
                    _Rigid.velocity = Vector3.zero;
                    _Floortime = 0.0f;

                    _State = PlayerState.Idle;
                    _Anim.SetAnim(_State);

                    _IsJump = false;
                    _IsDJump = false;
                    _Anim.SetIsJump(_IsJump);
                    _Anim.SetIsDJump(_IsDJump);

                    Debug.Log("check");
                   
                }

            }
        }




        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            throw new System.NotImplementedException();
        }

    }
}



