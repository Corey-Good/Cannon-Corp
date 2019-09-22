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
        //if((transform.eulerAngles.z > 40 && transform.eulerAngles.z < 90) || (transform.eulerAngles.z > 270 && transform.eulerAngles.z < 320))
        //{
        //   transform.eulerAngles = new Vector3(transform.eulerAngles.z - 25, transform.eulerAngles.y, transform.eulerAngles.x);
        //    Debug.Log(transform.eulerAngles.z);
        //}
            
        

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
