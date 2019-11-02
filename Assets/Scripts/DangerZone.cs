using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DangerZone : MonoBehaviour
{
    public static bool isInDanger = false;
    public GameObject dangerMessage;
    // Start is called before the first frame update

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isInDanger = true;
            dangerMessage.SetActive(true);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        isInDanger = false;
        dangerMessage.SetActive(false);
    }

}
