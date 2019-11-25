using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFallThrough : MonoBehaviour
{

    private void OnCollisionEnter(Collision collisionInfo)
    {
        if (Random.Range(0, 5) == 0)
        {
            GetComponent<BoxCollider>().enabled = false;
            Debug.Log("Fallthrough!");
        }
    }

    private void OnCollisionExit(Collision collisionInfo)
    {
        GetComponent<BoxCollider>().enabled = true;
    }

 

}
