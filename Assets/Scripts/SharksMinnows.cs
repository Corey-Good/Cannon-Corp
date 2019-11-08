using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SharksMinnows : MonoBehaviourPun
{
    public GameObject[] location = new GameObject[15];
    public static bool respawn = true;
    private bool needToRespawn = false;
    
    private void Awake()
    {
        int randomNumber = Random.Range(0, 11);
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1) 
        {
            PhotonNetwork.Instantiate("Shark", location[randomNumber].transform.position, Quaternion.Euler(0, 0, 0));
        }
        else
        {
            PhotonNetwork.Instantiate("Minnow", location[randomNumber].transform.position, Quaternion.Euler(0, 0, 0));
        }
    }

    private void FixedUpdate()
    {
        if(LoadUI.currentHealth < 0.0f)
        {
            needToRespawn = true;
        }

        if(needToRespawn)
        {
            LoadUI.currentHealth = 100.0f;
            int randomNumber = Random.Range(0, 11);
            PhotonNetwork.Instantiate("Shark", location[randomNumber].transform.position, Quaternion.Euler(0, 0, 0));
            needToRespawn = false;
        }
    }

}
