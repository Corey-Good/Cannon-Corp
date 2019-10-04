using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float movementForce = (float)CharacterInfo.info[CharacterMenu.currentModelIndex]["movementForce"];
    private float rotateSpeed = (float)CharacterInfo.info[CharacterMenu.currentModelIndex]["rotationSpeed"];

    void FixedUpdate()
    {         
        

        if (Input.GetKey("w"))
        {
            transform.position += transform.forward * Time.deltaTime * movementForce;
        } else if (Input.GetKey("s"))
        {
            transform.position += -transform.forward * Time.deltaTime * movementForce;
        } else
        {
            LoadUI.currentHealth += 0.01f;
        }

        if (Input.GetKey("d"))
        {
            transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
        }
        else if (Input.GetKey("a"))
        {
            transform.Rotate(-Vector3.up * rotateSpeed * Time.deltaTime);
        }
        
        if (Input.GetKey("h"))
        {
            LoadUI.currentHealth -= 1.0f;
        }












    }

}
