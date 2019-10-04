using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SpawnCharacter : MonoBehaviour
{
    public GameObject[] location = new GameObject[15];
    public GameObject[] playerModel;

    // Start is called before the first frame update
    void Awake()
    {
        try
        {
            int index = CharacterMenu.currentModelIndex;
            int randomNumber = Random.Range(0, 11);
            GameObject a = Instantiate(playerModel[index], location[randomNumber].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
        }
        catch (System.Exception)
        {
            Cursor.visible = true;
            SceneManager.LoadScene(0);
        }; 
    }

}
