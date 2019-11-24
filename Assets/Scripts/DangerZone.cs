﻿using UnityEngine;
using Photon.Pun;

public class DangerZone : MonoBehaviour
{
    public static bool isInDanger = false;
    public GameObject dangerMessage;

    // Start is called before the first frame update

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<PhotonView>().ViewID == PlayerMovement.playerViewId)
        {
            isInDanger = true;
            dangerMessage.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<PhotonView>().ViewID == PlayerMovement.playerViewId)
        {
            isInDanger = false;
            dangerMessage.SetActive(false);
        }
    }
}