/************************************************************************/
/* Author:  */
/* Date Created: */
/* Last Modified Date: */
/* Modified By: */
/************************************************************************/

using Photon.Pun;
using UnityEngine;

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