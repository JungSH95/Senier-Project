using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonInit : MonoBehaviourPunCallbacks {

    private string gameVersion = "1.0";
    public string userId = "YouRang";
    public byte maxPlayer = 20;

    public JoyStick joyStick = null;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        joyStick = GameObject.FindWithTag("Controller").GetComponent<JoyStick>();
    }

    // Use this for initialization
    void Start () {
        PhotonNetwork.GameVersion = this.gameVersion;
        PhotonNetwork.NickName = userId;

        PhotonNetwork.ConnectUsingSettings();
	}

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connect To Master");
        PhotonNetwork.JoinRandomRoom();
    }

    // 룸 생성에 실패했을 때 사용
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Failed Join Room !!");
        PhotonNetwork.CreateRoom(null,
            new RoomOptions { MaxPlayers = this.maxPlayer });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room !!");
        GameObject obj = PhotonNetwork.Instantiate("Player1", new Vector3(0, 0, 0), Quaternion.identity);
        joyStick.Player = obj.transform;
    }
}
