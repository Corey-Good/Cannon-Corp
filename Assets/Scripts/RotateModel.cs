using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateModel : MonoBehaviour
{
    public float rotateSpeed = 18f;
    // Update is called once per frame
    void FixedUpdate()
    {

        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);

    }
}
