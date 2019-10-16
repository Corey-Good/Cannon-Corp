using Photon.Pun;
using UnityEngine;

public class RoomController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private int multiplayerSceneIndex;

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
        Debug.Log("Joined Room!");
        StartGame();
    }

    private void StartGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Starting game! You are the master");
            PhotonNetwork.LoadLevel(multiplayerSceneIndex);
        }
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        Debug.Log("You are leaving the room");
    }
}
