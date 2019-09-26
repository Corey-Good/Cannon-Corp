using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCharacter : MonoBehaviour
{
    public GameObject location;
    public GameObject[] playerModel;

    // Start is called before the first frame update
    void Start()
    {
        int index = CharacterMenu.currentModelIndex;

        GameObject a = Instantiate(playerModel[index], location.transform.position, Quaternion.Euler(0, 0, 0)) as GameObject; 
    }

}
