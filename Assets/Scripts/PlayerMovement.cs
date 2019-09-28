using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float movementForce = 7.0f;
    private float rotateSpeed = 30.0f;




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
