using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class LobbyController : MonoBehaviourPunCallbacks
{
    private string roomName = "";

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void StartGame(Button button)
    {
        roomName = button.name;
        PhotonNetwork.JoinRoom(roomName);
        Debug.Log("Joining " + roomName);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join a room. :(");
        CreateRoom(roomName);
    }

    void CreateRoom(string room)
    {
        Debug.Log("Creating a room!");
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 20 };
        PhotonNetwork.CreateRoom(room, roomOps);
        Debug.Log("Room: " + room);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to create a room . . . ");
        CreateRoom(roomName);
    }

    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnDisable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("You have joined " + roomName);
        LoadGame();
    }

    private void LoadGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            int index = 0;
            Debug.Log("Starting game!");
            if(roomName == "FreeForAll")
            {
                index = 1;
            }
            else
            {
                index = 3;
            }
            PhotonNetwork.LoadLevel(index);
        }
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        Debug.Log("You are leaving the room");
    }
}
