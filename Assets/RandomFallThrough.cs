using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFallThrough : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (Random.Range(0, 5) == 0)
        {
            GetComponent<BoxCollider>().enabled = false;
            Debug.Log("Fallthrough!");
        }
        else
        {
            GetComponent<BoxCollider>().enabled = true;
            Debug.Log("Turn off fallthrough!");
        }
    }
}
