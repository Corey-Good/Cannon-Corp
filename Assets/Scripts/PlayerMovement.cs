﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviourPun
{
    private float movementForce = (float)CharacterInfo.info[CharacterMenu.currentModelIndex]["movementForce"];
    private float rotateSpeed   = (float)CharacterInfo.info[CharacterMenu.currentModelIndex]["rotationSpeed"];
    private float x_left = (Screen.width / 2.0f) + (Screen.width * 0.12f);
    private float x_right = (Screen.width / 2.0f) - (Screen.width * 0.12f);

    public GameObject baseObject;
    public GameObject headObject;

    private string forwardbutton;
    private string backwardbutton;
    private string leftbutton;
    private string rightbutton;


    void FixedUpdate()
    {
        SetKeyBindings();

        if (photonView.IsMine && !PauseMenu.GameIsPaused) 
        {
            MovePlayer();
            if (GameLoad.isXInverted)
            {
                MoveXInverted();
            }
            else
            {
                MoveXNormal();
            }
        }
    }

    public void SetKeyBindings()
    {
        if (Equals(KeyBindings.forwardKey, "UpArrow"))
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

    public void MovePlayer()
    {
        // Move play forwards and backwards, regenerate health when no movement is detected
        if (Input.GetKey(forwardbutton))
        {
            baseObject.transform.position += transform.forward * Time.deltaTime * movementForce;
        }
        else if (Input.GetKey(backwardbutton))
        {
            baseObject.transform.position += -transform.forward * Time.deltaTime * movementForce;
        }
        else
        {
            if (LoadUI.currentHealth < LoadUI.totalHealth)
            {
                LoadUI.currentHealth += 0.01f;
            }
        }

        // Rotate model left and right
        if (Input.GetKey(rightbutton))
        {
            baseObject.transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(leftbutton))
        {
            baseObject.transform.Rotate(-Vector3.up * rotateSpeed * Time.deltaTime);
        }

        // Decrease health, for testing purposes
        if (Input.GetKey("h"))
        {
            LoadUI.currentHealth -= 1.0f;
        }
    }

    public void MoveXNormal()
    {
        if (photonView.IsMine && !PauseMenu.GameIsPaused)
        {
            if (Input.mousePosition.x < x_left)
            {
                headObject.transform.Rotate(-Vector3.up * 30.0f * Time.deltaTime);
            }
            if (Input.mousePosition.x > x_right)
            {

                headObject.transform.Rotate(Vector3.up * 30.0f * Time.deltaTime);
            }
        }
    }

    public void MoveXInverted()
    {
        if (Input.mousePosition.x < x_left)
        {
            headObject.transform.Rotate(Vector3.up * 30.0f * Time.deltaTime);
        }
        if (Input.mousePosition.x > x_right)
        {
            headObject.transform.Rotate(-Vector3.up * 30.0f * Time.deltaTime);
        }

    }
}
