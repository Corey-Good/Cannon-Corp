using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float movementForce = (float)CharacterInfo.info[CharacterMenu.currentModelIndex]["movementForce"];
    private float rotateSpeed = (float)CharacterInfo.info[CharacterMenu.currentModelIndex]["rotationSpeed"];

    private string forwardbutton;
    private string backwardbutton;
    private string leftbutton;
    private string rightbutton;

    private void Start()
    {
        if(Equals(KeyBindings.forwardKey, "UpArrow"))
        {
            forwardbutton = "up";
        }
        else
        { 
            forwardbutton = KeyBindings.forwardKey.ToLower();
        }

        if (Equals(KeyBindings.backwardKey, "DownArrow"))
        {
            backwardbutton = "down";
        }
        else
        {
            backwardbutton = KeyBindings.backwardKey.ToLower();
        }

        if (Equals(KeyBindings.leftKey, "LeftArrow"))
        {
            leftbutton = "left";
        }
        else
        {
            leftbutton = KeyBindings.leftKey.ToLower();
        }

        if (Equals(KeyBindings.rightKey, "RightArrow"))
        {
            rightbutton = "right";
        }
        else
        {
            rightbutton = KeyBindings.rightKey.ToLower();
        }




    }
    void FixedUpdate()
    {
        // Move play forwards and backwards, regenerate health when no movement is detected
        if (Input.GetKey(forwardbutton))
        {
            transform.position += transform.forward * Time.deltaTime * movementForce;
        } 
        else if (Input.GetKey(backwardbutton))
        {
            transform.position += -transform.forward * Time.deltaTime * movementForce;
        } 
        else
        {
            LoadUI.currentHealth += 0.01f;
        }

        // Rotate model left and right
        if (Input.GetKey(rightbutton))
        {
            transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(leftbutton))
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
