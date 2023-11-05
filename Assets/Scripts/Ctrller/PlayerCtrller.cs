using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;
using Unity.VisualScripting;
using UnityEngine.Playables;


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

        [SerializeField]
        Vector3[] _Power;

        PhotonView _Pv;
        Rigidbody _Rigid;
        PlayerAnimation _Anim;
        PlayerState _State;
        
        //time
        float _StopTime = 3.0f;
        float _JumpTime = 0.25f;

    

        bool _IsJump;
        bool _IsDJump;
        void Start()
        {
            
            _Pv = GetComponent<PhotonView>();
            _Rigid = GetComponent<Rigidbody>();
            _Anim = GetComponent<PlayerAnimation>();
            GameMgr.Input.KeyAction -= OnKeyboard;
            GameMgr.Input.KeyAction += OnKeyboard;

        }

        private void FixedUpdate()
        {
            //�޸��ٰ� ���ߴ� ����
            _StopTime += Time.deltaTime;
            _JumpTime += Time.deltaTime;

            if(_Rigid.velocity.y<0)//����
            {
                _State = PlayerState.Falling;
                _Anim.SetAnim(_State);
            }
            

        }
        void Update()
        {
         
            Debug.Log(_State);

            //�޸��ٰ� ���߸� �̲�����
            if (_StopTime < 0.3 && _State == PlayerState.Idle||_State==PlayerState.Stop)
            {
                _Rigid.AddForce(this.transform.forward * _SlideSpeed);
                _State = PlayerState.Stop;
                _Anim.SetAnim(_State);
                Debug.Log("ttt");
            }
            OnFloor();//

        }

        void OnKeyboard()
        {
            //if (!_Pv.IsMine) return;

            if (Input.GetKey(KeyCode.Q)) //����
            {

            }

            if (Input.GetKey(KeyCode.W))//��ų
            {

            }

            if (Input.GetKey(KeyCode.E))//���
            {

            }

            if (Input.GetKey(KeyCode.R))//���
            {

            }

            if (Input.GetKey(KeyCode.Space))//����
            {
                if (!_IsJump)
                {
                    Jump();
                    _State = PlayerState.Jumping;
                    _IsJump = true;
                    _Anim.SetAnim(_State);
                    _Anim.SetJump(_IsJump);
                    _JumpTime = 0;
                }
                else if (!_IsDJump && _JumpTime > 0.25)  
                {
                    Jump();
                    _State = PlayerState.DoudbleJumping;
                    _IsDJump = true ;
                    _Anim.SetAnim(_State);
                    _Anim.SetDJump(_IsDJump);
                    _JumpTime = 0;
                }

            }

            if (Input.GetKey(KeyCode.UpArrow))//����Ű ��
            {

            }

            if (Input.GetKey(KeyCode.DownArrow))//���ձ� �� �� �ϰ� �ӵ� ���
            {

            }

            if (Input.GetKey(KeyCode.LeftArrow))//�� �̵�
            {
                /*�̵� �� ������ȯ*/
                Move(-1);
                /* ������ �޸� �� */

                if (_State == PlayerState.Idle&&!_IsJump)
                {
                    _State = PlayerState.Running;
                    _Anim.SetAnim(_State);
                }
                _StopTime = 0;



            }

            if (Input.GetKey(KeyCode.RightArrow))//�� �̵�
            {
                /* ������ �޸� �� */
                if (_State == PlayerState.Idle && !_IsJump)
                {
                    _State = PlayerState.Running;
                    _Anim.SetAnim(_State);
                }
                _StopTime = 0;

                /*�̵� �� ������ȯ*/
                Move(1);
                //this.transform.position += Vector3.right * Time.deltaTime * _MoveSpeed;
                /* 
                 * ���߿��� �̵�, ��ų�̳� ������ ���� bool�� �ʿ���.
                 */
            }



            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                Debug.Log("left");
            }


        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            throw new System.NotImplementedException();
        }

        void Jump()
        {
            if (!_IsJump)
            {

               _Rigid.AddForce(Vector3.up * _JumpingPower, ForceMode.Impulse);
            }
            else
            {
                _Rigid.velocity = Vector3.zero;
                _Rigid.AddForce(Vector3.up * _JumpingPower, ForceMode.Impulse);

            }
        }
        void Move(int dir)      /*�̵� �� ������ȯ*/
        {

            this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right * dir), 1f);
            this.transform.position += transform.forward * _MoveSpeed * Time.deltaTime;

        }

        void OnFloor()
        {
            RaycastHit hit;
            Debug.DrawRay(this.transform.position, this.transform.up * -0f, Color.green);
            LayerMask mask = LayerMask.GetMask("Floor");
            if (Physics.Raycast(this.transform.position, this.transform.up * -1,out hit, 0.5f, mask))
            {
                if (_State != PlayerState.Idle&&_JumpTime>0.25)
                {
                    _State = PlayerState.Idle;
                    _Anim.SetAnim(_State);
                    _IsJump = false;
                    _IsDJump = false;
                    _Anim.SetDJump(_IsDJump);
                    Debug.Log("mask");
                }
            }
        }
    }


}