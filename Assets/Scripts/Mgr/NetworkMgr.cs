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
        /* 접속에 필요한 게임 버전 설정 */
        PhotonNetwork.GameVersion = _gameVersion;
        /* 메인 서버 접속 시도 */
        PhotonNetwork.ConnectUsingSettings();
        /* 방 접속 버튼 비활성 */
  

    }

    /* 마스터 서버에 접속시 자동 실행 */
    public override void OnConnectedToMaster()
    {
        /*버튼 활성*/
    
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
  

        PhotonNetwork.ConnectUsingSettings();
    }


    /* 방에 접속을 시도 */
    public void Connect()
    {
        /* 클릭 후 중복 시도 방지 */
 

        if (PhotonNetwork.IsConnected)
        {
           // _connectionInfo.text = "방에 접속중..";
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
           // _joinBtn.interactable = false;
            //_connectionInfo.text = "off : 연결 끊김... 재연결 시동중..";

            PhotonNetwork.ConnectUsingSettings();
        }
    }

    /* 랜덤 빈방이 없을 경우 방 생성 참가자 수는 최대 2명 */
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
      //  _connectionInfo.text = "새로운 방 생성중 ..";
        Debug.Log($"방 생성 중 ");
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 2 });

    }

    /* 방 접속 성공 */
    public override void OnJoinedRoom()
    {
        /* 방 접속 상태 표시 */
       // _connectionInfo.text = "방 참가 성공 ";

        /* Scene 변경 */
        //PhotonNetwork.LoadLevel("Test1");

    }
}

