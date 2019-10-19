using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class NetworkController : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        if (!PhotonNetwork.IsConnected) { PhotonNetwork.ConnectUsingSettings(); }
        
       //PhotonNetwork.ConnectToMaster("192.168.1.84", 5055, "71e918df-014e-4de0-9504-eaa342d27b36");

       
    }

    public override void OnConnected()
    {
        Debug.Log("We are now connected to " + PhotonNetwork.ServerAddress + " server!");
    }

}
