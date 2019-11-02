using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerZone : MonoBehaviour
{
    public static bool isInDanger = false;
    // Start is called before the first frame update
    void OnTriggerExit()
    {
        Debug.Log("You are in the Danger Zone");
        isInDanger = true;
    }
}
