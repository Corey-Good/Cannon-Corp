using UnityEngine;
using Photon.Pun;

public class DangerZone : MonoBehaviour
{
    public static bool isInDanger = false;
    public GameObject dangerMessage;
    int photonID;
    // Start is called before the first frame update

    //public void Awake()
    //{
    //    if (!photonView.IsMine)
    //    {
    //        GameObject danger = GameObject.FindWithTag("DangerZone");
    //        danger.SetActive(false);
    //    }
    //}
    private void OnTriggerExit(Collider other)
    {
        
        //Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Player" && other.GetComponent<PhotonView>().GetInstanceID() == photonID)
        {
            isInDanger = true;
            dangerMessage.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        photonID = other.GetComponent<PhotonView>().GetInstanceID();
        //Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Player")
        {
            isInDanger = false;
            dangerMessage.SetActive(false);
        }
    }
}