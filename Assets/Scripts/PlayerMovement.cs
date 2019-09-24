using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float movementForce = 1000f;
    public float rotateSpeed = 30f;




    // Update is called once per frame
    void FixedUpdate()
    {         
        

        if (Input.GetKey("w"))
        {
            transform.position += transform.forward * Time.deltaTime * movementForce;
        }
        if (Input.GetKey("s"))
        {
            transform.position += -transform.forward * Time.deltaTime * movementForce;
        }
        if (Input.GetKey("a"))
        {
            transform.Rotate(-Vector3.up * rotateSpeed * Time.deltaTime);
        }
        else if (Input.GetKey("d"))
        {
            transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
        }



        







    }

}
