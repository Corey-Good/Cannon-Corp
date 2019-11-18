using Photon.Pun;
using UnityEngine;

public class SharksandMinnows : MonoBehaviourPun
{
    public GameObject[] location = new GameObject[10];
    public static bool respawn = false;
    private GameObject player;
    public Material sharkSkin;

    private void Awake()
    {
        respawn = true;
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            player = SpawnCharacter("Shark");
            player.tag = "EnemyTank";
        }
        else
        {
            player = SpawnCharacter("Minnow");
            player.tag = "Player";
        }
    }

    private void FixedUpdate()
    {
        if (LoadUI.currentHealth <= 0)
        {
            LoadUI.currentHealth = 55.0f;
            LoadUI.score = 0.0f;
            player.transform.position = location[Random.Range(0, 10)].transform.position;
            player.transform.rotation = new Quaternion(0, 0, 0, 0);
            player.tag = "EnemyTank";
            player.GetComponent<MeshRenderer>().material.color = Color.red; 
        }
    }

    private GameObject SpawnCharacter(string model)
    {
        return PhotonNetwork.Instantiate(model, location[Random.Range(0, 10)].transform.position, Quaternion.Euler(0, 0, 0));
    }
}
