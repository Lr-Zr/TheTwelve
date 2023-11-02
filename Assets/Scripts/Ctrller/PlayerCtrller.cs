using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;
using Unity.VisualScripting;

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
        Animator _Anim;
        PlayerState _State;

        //time
        float _StopTime = 3.0f;

        public enum PlayerState
        {
            Idle,
            Running,
            Floating,

        }
        void Start()
        {

            _Pv = GetComponent<PhotonView>();
            _Rigid = GetComponent<Rigidbody>();
            _Anim = GetComponent<Animator>();
            GameMgr.Input.KeyAction -= OnKeyboard;
            GameMgr.Input.KeyAction += OnKeyboard;


        }

        private void FixedUpdate()
        {
            //달리다가 멈추는 조건
            if (_StopTime > 0.02 && _State == PlayerState.Running)
            {
                // _Rigid.AddForce(this.transform.forward * _MoveSpeed, ForceMode.Impulse);
                _Anim.SetBool("Running", false);
                _State = PlayerState.Idle;
                Debug.Log("dd");
            }

            _StopTime += Time.deltaTime;
        }
        void Update()
        {

            //달리다가 멈추면 미끄러짐
            if (_StopTime < 0.3 && _State == PlayerState.Idle)
            {
                _Rigid.AddForce(this.transform.forward * _SlideSpeed);
                Debug.Log("ttt");
            }
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
                if (_State != PlayerState.Floating)
                {
                    _Rigid.AddForce(Vector3.up * _JumpingPower, ForceMode.Impulse);
                    _State = PlayerState.Floating;
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
                /* 땅에서 달릴 때 */
                if (_State == PlayerState.Idle)
                {
                    _Anim.SetBool("Running", true);
                    _State = PlayerState.Running;
                }
                _StopTime = 0;

                /*이동 및 방향전환*/
                this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 1f);
                //this.transform.position += Vector3.left * Time.deltaTime * _MoveSpeed;
                _Rigid.velocity = _MoveSpeed * transform.forward * Time.deltaTime;
                this.transform.position += _Rigid.velocity;


            }

            if (Input.GetKey(KeyCode.RightArrow))//우 이동
            {
                /* 땅에서 달릴 때 */
                if (_State == PlayerState.Idle)
                {
                    _Anim.SetBool("Running", true);
                    _State = PlayerState.Running;
                }
                _StopTime = 0;

                /*이동 및 방향전환*/
                this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 1f);
                _Rigid.velocity = _MoveSpeed * transform.forward * Time.deltaTime;
                this.transform.position += _Rigid.velocity;
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
    }


}