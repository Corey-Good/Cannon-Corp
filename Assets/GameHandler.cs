using Photon.Pun;
using UnityEngine;

public class GameHandler : MonoBehaviourPunCallbacks
{
    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        Debug.Log("You are leaving the room");
    }
}
