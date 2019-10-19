using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class NetworkController : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Attempting to connect to server...");
    }

    public override void OnConnected()
    {
        Debug.Log("We are now connected to " + PhotonNetwork.ServerAddress + "!");
    }
}
