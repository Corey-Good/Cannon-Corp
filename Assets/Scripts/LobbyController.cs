using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class LobbyController : MonoBehaviourPunCallbacks
{
    private string roomName = "";
    public Text status;

    public void Start()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
            status.text = "Connecting . . .";
        }
    }
    public override void OnConnectedToMaster()
    {
        status.text = "Now connected!!";
    }

    public void StartGame(Button button)
    {
        roomName = button.name;
        if(PhotonNetwork.IsConnectedAndReady)
        { 
            PhotonNetwork.JoinRoom(roomName);            
        }

    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {        
        CreateRoom(roomName);
    }

    void CreateRoom(string room)
    {
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 20 };
        PhotonNetwork.CreateRoom(room, roomOps);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        CreateRoom(roomName);
    }

    public override void OnJoinedRoom()
    {
        LoadGame();
    }

    private void LoadGame()
    {
        if(roomName == "FreeForAll")
            PhotonNetwork.LoadLevel(1);
        else
        {
            PhotonNetwork.LoadLevel(4);
        }
    }


}
