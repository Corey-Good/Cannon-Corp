/************************************************************************/
/* Author:  */
/* Date Created: */
/* Last Modified Date: */
/* Modified By: */
/************************************************************************/

using UnityEngine;

public class Respawner : MonoBehaviour
{
    public GameObject[] location = new GameObject[15];

    private void OnTriggerEnter(Collider other)
    {
        int rNumber = Random.Range(0, 14);
        other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        other.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        other.gameObject.transform.position = location[rNumber].transform.position;
        other.gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
    }
}