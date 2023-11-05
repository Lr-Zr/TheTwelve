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
            //달리다가 멈추는 조건
            _StopTime += Time.deltaTime;
            _JumpTime += Time.deltaTime;

            if(_Rigid.velocity.y<0)//낙하
            {
                _State = PlayerState.Falling;
                _Anim.SetAnim(_State);
            }
            

        }
        void Update()
        {
         
            Debug.Log(_State);

            //달리다가 멈추면 미끄러짐
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

            if (Input.GetKey(KeyCode.Q)) //공격
            {

            }

            if (Input.GetKey(KeyCode.W))//스킬
            {

            }

            if (Input.GetKey(KeyCode.E))//방어
            {

            }

            if (Input.GetKey(KeyCode.R))//잡기
            {

            }

            if (Input.GetKey(KeyCode.Space))//점프
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

            if (Input.GetKey(KeyCode.UpArrow))//조합키 상
            {

            }

            if (Input.GetKey(KeyCode.DownArrow))//조합기 하 및 하강 속도 향상
            {

            }

            if (Input.GetKey(KeyCode.LeftArrow))//좌 이동
            {
                /*이동 및 방향전환*/
                Move(-1);
                /* 땅에서 달릴 때 */

                if (_State == PlayerState.Idle&&!_IsJump)
                {
                    _State = PlayerState.Running;
                    _Anim.SetAnim(_State);
                }
                _StopTime = 0;



            }

            if (Input.GetKey(KeyCode.RightArrow))//우 이동
            {
                /* 땅에서 달릴 때 */
                if (_State == PlayerState.Idle && !_IsJump)
                {
                    _State = PlayerState.Running;
                    _Anim.SetAnim(_State);
                }
                _StopTime = 0;

                /*이동 및 방향전환*/
                Move(1);
                //this.transform.position += Vector3.right * Time.deltaTime * _MoveSpeed;
                /* 
                 * 공중에서 이동, 스킬이나 공격을 위한 bool이 필요함.
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
        void Move(int dir)      /*이동 및 방향전환*/
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