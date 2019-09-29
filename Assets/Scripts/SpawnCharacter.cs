using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCharacter : MonoBehaviour
{
    public GameObject[] location = new GameObject[15];
    public GameObject[] playerModel;

    // Start is called before the first frame update
    void Start()
    {
        int index = CharacterMenu.currentModelIndex;
        int randomNumber = Random.Range(0, 11);
        GameObject a = Instantiate(playerModel[index], location[randomNumber].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject; 
    }

}
