using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

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
        
        //파워
        [SerializeField]
        float _Power = 5.0f;

        //게이지
        [SerializeField]
        float _Gauge = 1.0f;

        //목숨
        [SerializeField]
        int _Life = 3;



        PhotonView _Pv;
        void Start()
        {
            _Pv = GetComponent<PhotonView>();
            GameMgr.Input.KeyAction -= OnKeyboard;
            GameMgr.Input.KeyAction += OnKeyboard;
        }


        void Update()
        {
            

        }

        void OnKeyboard()
        {
            if (!_Pv.IsMine) return;

            if(Input.GetKey(KeyCode.Q)) //공격
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

            }

            if (Input.GetKey(KeyCode.UpArrow))//조합키 상
            {

            }

            if (Input.GetKey(KeyCode.DownArrow))//조합기 하 및 하강 속도 향상
            {

            }

            if (Input.GetKey(KeyCode.LeftArrow))//좌 이동
            {

            }

            if (Input.GetKey(KeyCode.RightArrow))//우 이동
            {

            }
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            throw new System.NotImplementedException();
        }
    }
    

}