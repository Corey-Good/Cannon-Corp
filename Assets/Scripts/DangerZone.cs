using UnityEngine;
using Photon.Pun;

public class DangerZone : MonoBehaviour
{
    public static bool isInDanger = false;
    public GameObject dangerMessage;

    // Start is called before the first frame update

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other.gameObject.GetComponent<PhotonView>().ViewID + " is exiting");
        //Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<PhotonView>().ViewID == PlayerMovement.playerViewId)
        {
            isInDanger = true;
            dangerMessage.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.GetComponent<PhotonView>().ViewID + " is entering");
        //Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<PhotonView>().ViewID == PlayerMovement.playerViewId)
        {
            isInDanger = false;
            dangerMessage.SetActive(false);
        }
    }
}