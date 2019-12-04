using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class HighScore : MonoBehaviour
{
    Player[] playersInRoom;

    private void Awake()
    {
        playersInRoom = PhotonNetwork.PlayerList;
    }
    

    private void Update()
    {
        //Debug.Log("Players in Room" + playersInRoom[0]);
    }
}
