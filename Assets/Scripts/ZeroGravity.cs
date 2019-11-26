using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ZeroGravity : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PhotonView>().ViewID == PlayerMovement.playerViewId)
        {
            other.gameObject.GetComponent<Rigidbody>().useGravity = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PhotonView>().ViewID == PlayerMovement.playerViewId)
        {
            other.gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
