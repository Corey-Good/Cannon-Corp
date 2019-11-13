using Photon.Pun;
using UnityEngine;

public class SharksMinnows : MonoBehaviourPun
{
    public GameObject[] location = new GameObject[15];
    public static bool respawn = false;
    private GameObject player;

    private void Awake()
    {
        respawn = true;
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            player = SpawnCharacter("Shark");
        }
        else
        {
            player = SpawnCharacter("Minnow");
        }
    }

    private void FixedUpdate()
    {
        if (LoadUI.currentHealth <= 0)
        {
            PhotonNetwork.Destroy(player);
            LoadUI.currentHealth = 75.0f;
            LoadUI.score = 0.0f;
            player = SpawnCharacter("Shark");
        }
    }

    private GameObject SpawnCharacter(string model)
    {
        return PhotonNetwork.Instantiate(model, location[Random.Range(0, 10)].transform.position, Quaternion.Euler(0, 0, 0));
    }
}