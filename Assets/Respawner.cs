using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    public GameObject[] location = new GameObject[15];

    private void OnTriggerEnter(Collider other)
    {
        int rNumber = Random.Range(0, 14);
        other.gameObject.transform.position = location[rNumber].transform.position;
        other.gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
    }

}
