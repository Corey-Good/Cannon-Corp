﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;


public class SpawnCharacter : MonoBehaviourPun
{
    public GameObject[] location = new GameObject[15];
    public GameObject[] playerModel;
    private int index = CharacterMenu.currentModelIndex;
    private string[] tankNames = new string[5] { "FiringTank1", "FiringTank2", "FiringCatapult", "FiringCartoonTank", "FiringBoxTank"};



    // On Scene Load, spawn the charcter in one of the random locations, if fails, load Main Menu
    void Awake()
    {
        try
        {
            
            int randomNumber = Random.Range(0, 11);
            //GameObject a = Instantiate(playerModel[index], location[randomNumber].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
            PhotonNetwork.Instantiate(tankNames[index], location[randomNumber].transform.position, Quaternion.Euler(0, 0, 0));
        }
        catch (System.Exception)
        {
            Cursor.visible = true;
            SceneManager.LoadScene(0);
        }; 
    }
}
