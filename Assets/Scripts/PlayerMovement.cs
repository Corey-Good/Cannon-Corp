using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviourPun
{
    private float movementForce = (float)CharacterInfo.info[CharacterMenu.currentModelIndex]["movementForce"];
    private float rotateSpeed = (float)CharacterInfo.info[CharacterMenu.currentModelIndex]["rotationSpeed"];

    void FixedUpdate()
    {

        if (photonView.IsMine)
        {

            // Move play forwards and backwards, regenerate health when no movement is detected
            if (Input.GetKey("w"))
            {
                transform.position += transform.forward * Time.deltaTime * movementForce;
            }
            else if (Input.GetKey("s"))
            {
                transform.position += -transform.forward * Time.deltaTime * movementForce;
            }
            else
            {
                LoadUI.currentHealth += 0.01f;
            }

            // Rotate model left and right
            if (Input.GetKey("d"))
            {
                transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
            }
            else if (Input.GetKey("a"))
            {
                transform.Rotate(-Vector3.up * rotateSpeed * Time.deltaTime);
            }

            // Decrease health, for testing purposes
            if (Input.GetKey("h"))
            {
                LoadUI.currentHealth -= 1.0f;
            }
        }
    }

}
