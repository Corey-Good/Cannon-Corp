﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RPCFunction : MonoBehaviourPun
{
    public GameObject tankBody;
    public GameObject tankHead;

    private void FixedUpdate()
    {
        if (LoadUI.currentHealth <= 0)
        {
            photonView.RPC("RespawnCharacter", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    void RespawnCharacter()
    {
        LoadUI.currentHealth = 55.0f;
        LoadUI.score = 0.0f;
        //transform.position = SharksandMinnows.location[Random.Range(0, 10)].transform.position;
        transform.rotation = new Quaternion(0, 0, 0, 0);
        tag = "EnemyTank";
        tankBody.GetComponent<MeshRenderer>().material.color = Color.red;
        tankHead.GetComponent<MeshRenderer>().material.color = Color.red;
    }
}
