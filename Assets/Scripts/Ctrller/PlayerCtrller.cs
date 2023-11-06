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


        //이동속도
        [SerializeField]
        float _MoveSpeed = 10.0f;

        //점프력
        [SerializeField]
        float _JumpingPower = 10.0f;

        //게이지
        [SerializeField]
        float _Gauge = 1.0f;

        //목숨
        [SerializeField]
        int _Life = 3;

        //미끄러지는 속도
        [SerializeField]
        float _SlideSpeed = 50.0f;

        [SerializeField]
        Vector3[] _Power;

        PhotonView _Pv;
        Rigidbody _Rigid;
        PlayerAnimation _Anim;
        PlayerEffect _Eff;
        PlayerState _State;

        //time
        float _RunTime = 0.0f;
        float _JumpTime = 0.25f;
        float _Floortime = 0.0f;//판정을 천천히


        bool _IsJump;
        bool _IsDJump;
        bool _IsOnesec;
        bool _IsRunning;
        bool _Stop;

        int dir;

        Vector3 _PrePos;
        void Start()
        {

            _Pv = GetComponent<PhotonView>();
            _Rigid = GetComponent<Rigidbody>();
            _Anim = GetComponent<PlayerAnimation>();
            _Eff = GetComponent<PlayerEffect>();

            GameMgr.Input.KeyAction -= OnKeyboard;
            GameMgr.Input.KeyAction += OnKeyboard;
            _IsJump = false;
            _IsDJump = false;
            _IsOnesec = false;
            _IsRunning = false;

        }

        private void FixedUpdate()
        {
            //달리다가 멈추는 조건
            _Floortime += Time.deltaTime;
            _JumpTime += Time.deltaTime;
            if (_RunTime > 1.0f)
            {
                _RunTime = 1.001f;
                _IsOnesec = true;
            }
            if (_Rigid.velocity.y < -0.05f && _IsJump)//낙하
            {
                _State = PlayerState.Falling;
                _Anim.SetAnim(_State);
            }

        }
        void Update()
        {

            OnFloor();//
                      //Debug.Log(_State);
                      //Debug.Log("running"+_IsRunning);
                      //Debug.Log(_RunTime);

            //달리다가 멈추면 미끄러짐


            if (_IsRunning && _State == PlayerState.Stop)//탄성 효과
            {
                _RunTime -= Time.deltaTime;
                if (_RunTime > 0.75f)
                {
                    //Debug.Log("이동");
                    _Rigid.AddForce(this.transform.forward * _SlideSpeed);
                }
                else
                {
                    _RunTime = 0f;
                    _IsRunning = false;
                    _IsOnesec = false;
                }

            }

            if (_PrePos == transform.position && _IsRunning)
            {
                _State = PlayerState.Stop;
                _Anim.SetAnim(_State);
                _Eff.Break(this.transform.position, dir);
            }

            _PrePos = transform.position;
        }

        void OnKeyboard()
        {
            //if (!_Pv.IsMine) return;

            if (Input.GetKey(KeyCode.Q)) //공격
            {

            }

            else if (Input.GetKey(KeyCode.W))//스킬
            {

            }

            else if (Input.GetKey(KeyCode.E))//방어
            {

            }

            else if (Input.GetKey(KeyCode.R))//잡기
            {

            }




            if (Input.GetKey(KeyCode.Space))//점프
            {
                if (!_IsJump && _State != PlayerState.Falling)
                {
                    Jump();
                    _State = PlayerState.Jumping;
                    _IsJump = true;

                    _Anim.SetAnim(_State);
                    _Anim.SetJump(_IsJump);
                    _JumpTime = 0;

                    _Eff.Jump(this.transform.position, 1);

                }
                else if (!_IsDJump && _JumpTime > 0.25)
                {
                    Jump();
                    _State = PlayerState.DoudbleJumping;
                    _IsDJump = true;
                    _Anim.SetAnim(_State);
                    _Anim.SetDJump(_IsDJump);
                    _JumpTime = 0;

                    _Eff.Jump(this.transform.position, 0);
                }

                _IsRunning = false;

            }





            if (Input.GetKey(KeyCode.UpArrow))//조합키 상
            {

            }

            else if (Input.GetKey(KeyCode.DownArrow))//조합기 하 및 하강 속도 향상
            {

            }

            if (Input.GetKey(KeyCode.LeftArrow))//좌 이동
            {
                /*이동 및 방향전환*/

                /* 땅에서 달릴 때 */

                if (_State == PlayerState.Idle || _State == PlayerState.Stop && !_IsJump)
                {

                    _IsRunning = true;
                    _State = PlayerState.Running;
                    _Anim.SetAnim(_State);
                }


                if (!_IsJump)
                {
                    dir = -1;
                    _RunTime += Time.deltaTime;
                    _Eff.Run(this.transform.position, dir);
                }
                Move(-1);

            }



            if (Input.GetKey(KeyCode.RightArrow))//우 이동
            {
                /*이동 및 방향전환*/

                /* 땅에서 달릴 때 */
                if (_State == PlayerState.Idle || _State == PlayerState.Stop && !_IsJump)
                {

                    _IsRunning = true;
                    _State = PlayerState.Running;
                    _Anim.SetAnim(_State);
                }

                if (!_IsJump)
                {
                    dir = 1;
                    _RunTime += Time.deltaTime;
                    _Eff.Run(this.transform.position, dir);
                }
                Move(1);



                /* 
                 * 공중에서 이동, 스킬이나 공격을 위한 bool이 필요함.
                 */
            }



        }




        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            throw new System.NotImplementedException();
        }

        void Jump()
        {
            _Rigid.velocity = Vector3.zero;
            _Rigid.AddForce(Vector3.up * _JumpingPower, ForceMode.Impulse);
        }
        void Move(int dir)      /*이동 및 방향전환*/
        {
            if (_State != PlayerState.Stop)
            {

                if (!_IsJump)
                {

                    this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right * dir), 1f);
                    this.transform.position += transform.forward * _MoveSpeed * Time.deltaTime;
                }
                else
                {
                    this.transform.position += Vector3.right * dir * _MoveSpeed * Time.deltaTime;
                }

            }
        }

        void OnFloor()
        {
            RaycastHit hit;
            Debug.DrawRay(this.transform.position, this.transform.up * -0.2f, Color.green);
            LayerMask mask = LayerMask.GetMask("Floor");
            if (Physics.Raycast(this.transform.position, this.transform.up * -1, out hit, 0.2f, mask))
            {
                if (_State != PlayerState.Idle && _JumpTime > 0.25 && _Floortime > 0.05 && !_IsRunning)
                {
                    _Rigid.velocity = Vector3.zero;
                    _Floortime = 0.0f;
                    _State = PlayerState.Idle;
                    _Anim.SetAnim(_State);
                    _IsJump = false;
                    _IsDJump = false;
                    _Anim.SetJump(_IsJump);
                    _Anim.SetDJump(_IsDJump);

                    Debug.Log("check");
                }

            }
        }
    }
}



