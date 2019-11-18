using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;


public class SpawnCharacter : MonoBehaviourPun
{
    public GameObject[] location = new GameObject[15];
    private int index = CharacterMenu.currentModelIndex;
    private string[] tankNames = new string[5] { "BaseTank", "FutureTank", "CatapultModel", "CartoonTank", "Tank2" };

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
            Debug.Log("This is the FLASH bug that has been haunting you! Boo");
        };
    }
}
