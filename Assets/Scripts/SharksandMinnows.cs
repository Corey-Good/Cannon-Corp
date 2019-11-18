using Photon.Pun;
using UnityEngine;

public class SharksandMinnows : MonoBehaviourPun
{
    public GameObject[] location = new GameObject[10];
    public static bool respawn = false;
    private GameObject player;

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

    private GameObject SpawnCharacter(string model)
    {
        int randomNumber = Random.Range(0, 10);
        return PhotonNetwork.Instantiate(model, location[randomNumber].transform.position, new Quaternion(0, 0, 0, 0));
    }

}
