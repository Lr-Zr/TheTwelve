using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkMgr : MonoBehaviourPunCallbacks
{
    string _gameVersion = "1";
    string _userID = "1";

    void Start()
    {
        /* ���ӿ� �ʿ��� ���� ���� ���� */
        PhotonNetwork.GameVersion = _gameVersion;
        /* ���� ���� ���� �õ� */
        PhotonNetwork.ConnectUsingSettings();
        /* �� ���� ��ư ��Ȱ�� */
  

    }

    /* ������ ������ ���ӽ� �ڵ� ���� */
    public override void OnConnectedToMaster()
    {
        /*��ư Ȱ��*/
    
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
  

        PhotonNetwork.ConnectUsingSettings();
    }


    /* �濡 ������ �õ� */
    public void Connect()
    {
        /* Ŭ�� �� �ߺ� �õ� ���� */
 

        if (PhotonNetwork.IsConnected)
        {
           // _connectionInfo.text = "�濡 ������..";
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
           // _joinBtn.interactable = false;
            //_connectionInfo.text = "off : ���� ����... �翬�� �õ���..";

            PhotonNetwork.ConnectUsingSettings();
        }
    }

    /* ���� ����� ���� ��� �� ���� ������ ���� �ִ� 2�� */
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
      //  _connectionInfo.text = "���ο� �� ������ ..";
        Debug.Log($"�� ���� �� ");
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 2 });

    }

    /* �� ���� ���� */
    public override void OnJoinedRoom()
    {
        /* �� ���� ���� ǥ�� */
       // _connectionInfo.text = "�� ���� ���� ";

        /* Scene ���� */
        //PhotonNetwork.LoadLevel("Test1");

    }
}

