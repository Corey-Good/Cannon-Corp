using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringBoard : MonoBehaviour
{
    private void OnCollisionEnter(Collision collisionInfo)
    {
        if (Random.Range(0, 3) == 0)
        {
            collisionInfo.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 5000, ForceMode.Acceleration);
            Debug.Log("Launch!!!");
        }
    }
}
