using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

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
        
        //�Ŀ�
        [SerializeField]
        float _Power = 5.0f;

        //������
        [SerializeField]
        float _Gauge = 1.0f;

        //���
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

            if(Input.GetKey(KeyCode.Q)) //����
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

            }

            if (Input.GetKey(KeyCode.UpArrow))//����Ű ��
            {

            }

            if (Input.GetKey(KeyCode.DownArrow))//���ձ� �� �� �ϰ� �ӵ� ���
            {

            }

            if (Input.GetKey(KeyCode.LeftArrow))//�� �̵�
            {

            }

            if (Input.GetKey(KeyCode.RightArrow))//�� �̵�
            {

            }
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            throw new System.NotImplementedException();
        }
    }
    

}