using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpTesting : MonoBehaviour
{
    public Transform EndCube;




    public void FixedUpdate()
    {
       // transform.position = Vector3.Lerp(transform.position, EndCube.position, Time.time * 0.1f);
        transform.rotation = Quaternion.Lerp(transform.rotation, EndCube.rotation, Time.time * 0.1f);
    }
}
