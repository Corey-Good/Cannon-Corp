using Photon.Pun;
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



    public GameObject bullet;              // Contains the bullet model
    private GameObject bulletCopy;          // Copy of the bullet that gets launched
    public GameObject bulletSpawnLocation; // Where the bull is instantiated
    private Rigidbody bulletRB;            // Rigibody of the bullet to be launched

    private float timeElapsed = 0f;
    private bool bulletActive = false;
    private float reloadSpeed = (float)CharacterInfo.info[CharacterMenu.currentModelIndex]["reloadSpeed"];
    private float bulletSpeed = (float)CharacterInfo.info[CharacterMenu.currentModelIndex]["bulletSpeed"];
    private float bulletArch = (float)CharacterInfo.info[CharacterMenu.currentModelIndex]["bulletArch"];
    public static float reloadProgress;


    private void Awake()
    {
        // Ensures the bar is full when starting the game
        reloadProgress = 1.0f;
    }







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

        if (Input.GetMouseButtonDown(KeyBindings.clickIndex) && bulletActive == false && !PauseMenu.GameIsPaused)
        {
            // Creates a copy of the bullet, and captures its Rigibody (into bulletRB)
            bulletCopy = PhotonNetwork.Instantiate("Bullet", bulletSpawnLocation.transform.position, Quaternion.Euler(0, 0, 0));
            bulletRB = bulletCopy.GetComponent<Rigidbody>();

            // Applies the launching force to the bullet and sets its status to active (true)
            bulletRB.AddRelativeForce(((transform.forward * bulletSpeed) + (transform.up * bulletArch)), ForceMode.Impulse);
            bulletActive = true;
        }

        if (bulletActive)
        {
            // Increase time and update the reloadBar progress
            timeElapsed += Time.deltaTime;
            reloadProgress = timeElapsed / reloadSpeed;

            // When a bullet is reloaded, delete the previouis copy and reset timer
            if (timeElapsed >= reloadSpeed)
            {
                PhotonNetwork.Destroy(bulletCopy);
                timeElapsed = 0f;
                bulletActive = false;
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
