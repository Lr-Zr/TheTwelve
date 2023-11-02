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
            //�޸��ٰ� ���ߴ� ����
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

            //�޸��ٰ� ���߸� �̲�����
            if (_StopTime < 0.3 && _State == PlayerState.Idle)
            {
                _Rigid.AddForce(this.transform.forward * _SlideSpeed);
                Debug.Log("ttt");
            }
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
                if (_State != PlayerState.Floating)
                {
                    _Rigid.AddForce(Vector3.up * _JumpingPower, ForceMode.Impulse);
                    _State = PlayerState.Floating;
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
                /* ������ �޸� �� */
                if (_State == PlayerState.Idle)
                {
                    _Anim.SetBool("Running", true);
                    _State = PlayerState.Running;
                }
                _StopTime = 0;

                /*�̵� �� ������ȯ*/
                this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 1f);
                //this.transform.position += Vector3.left * Time.deltaTime * _MoveSpeed;
                _Rigid.velocity = _MoveSpeed * transform.forward * Time.deltaTime;
                this.transform.position += _Rigid.velocity;


            }

            if (Input.GetKey(KeyCode.RightArrow))//�� �̵�
            {
                /* ������ �޸� �� */
                if (_State == PlayerState.Idle)
                {
                    _Anim.SetBool("Running", true);
                    _State = PlayerState.Running;
                }
                _StopTime = 0;

                /*�̵� �� ������ȯ*/
                this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 1f);
                _Rigid.velocity = _MoveSpeed * transform.forward * Time.deltaTime;
                this.transform.position += _Rigid.velocity;
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
    }


}