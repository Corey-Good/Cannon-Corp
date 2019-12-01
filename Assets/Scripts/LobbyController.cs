/************************************************************************/
/* Author:  */
/* Date Created: */
/* Last Modified Date: */
/* Modified By: */
/************************************************************************/

using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class LobbyController : MonoBehaviourPunCallbacks
{
    public Text status;
    private string roomName = "";
    public override void OnConnectedToMaster()
    {
        status.text = "Now connected!!";
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined a room! Now starting the game");
        LoadGame();
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to make a room. . . .");
    }

    public void ResetConnection()
    {
        PhotonNetwork.Disconnect();
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

    public void Update()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
            status.text = "Connecting . . .";
        }
        if (status.text == "Offline")
        {
            ResetConnection();
        }
    }
    private void LoadGame()
    {
        if (roomName == "FreeForAll")
        {
            PhotonNetwork.LoadLevel(1);
        }
        else if (roomName == "SharksAndMinnows")
        {
            PhotonNetwork.LoadLevel(3);
        }
        else
        {
            PhotonNetwork.LoadLevel(4);
        }
    }
}