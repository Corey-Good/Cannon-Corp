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
    
    public void Update()
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
        TypedLobby typedLobby = new TypedLobby();
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 20 };
        if (PhotonNetwork.IsConnectedAndReady)
        { 
            PhotonNetwork.JoinOrCreateRoom(roomName, roomOps, typedLobby);            
        }
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to make a room. . . .");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined a room! Now starting the game");
        LoadGame();
    }

    private void LoadGame()
    {
        if(roomName == "FreeForAll")
            PhotonNetwork.LoadLevel(1);
        else
        {
            PhotonNetwork.LoadLevel(3);
        }
    }

    public void ResetConnection()
    {
        PhotonNetwork.Disconnect();
    }
}
