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

    private float centerOfScreen = (Screen.width / 2.0f);

    public GameObject baseObject;
    public GameObject headObject;
    public GameObject tankCamera;
    //private float turretSpeed = 0.4f;

    private string forwardbutton;
    private string backwardbutton;
    private string leftbutton;
    private string rightbutton;

    private GameObject bulletCopy;         // Copy of the bullet that gets launched
    public GameObject bulletSpawnLocation; // Where the bullet is instantiated

    private float timeElapsed = 0f;
    private bool bulletActive = false;
    private float reloadSpeed = (float)CharacterInfo.info[CharacterMenu.currentModelIndex]["reloadSpeed"];
    private float bulletSpeed = (float)CharacterInfo.info[CharacterMenu.currentModelIndex]["bulletSpeed"];
    private float bulletArch = (float)CharacterInfo.info[CharacterMenu.currentModelIndex]["bulletArch"];
    public static float reloadProgress;


    //[Header("Turret Properties")]
    //public Transform turretTransform;
    //public float turretLagSpeed = 0.5f;
    //private Vector3 turretFinalLookDirection;


    Vector3 targetPosition;
    Vector3 lookAtTarget;
    Quaternion playerRot;
    float rotSpeed = 5;


    private void Awake()
    {
        reloadProgress = 1.0f;
    }

    public void FixedUpdate()
    {
        SetKeyBindings();

        if (!PauseMenu.GameIsPaused)
        {
            MovePlayer();
            FireMechanism();
        }

        //SetTargetPosition();
        //RotateHead();

        //if (GameLoad.isXInverted)
        //{
        //    MoveXInverted();
        //}
        //else
        //{
        //    MoveXNormal();
        //}
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


    //void SetTargetPosition()
    //{
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    RaycastHit hit;

    //    if (Physics.Raycast(ray, out hit, 1000))
    //    {
    //        targetPosition = hit.point;
    //        //this.transform.LookAt(targetPosition);
    //        lookAtTarget = new Vector3(targetPosition.x - transform.position.x, transform.position.y, targetPosition.z - transform.position.z);
    //        playerRot = Quaternion.LookRotation(lookAtTarget);
    //    }
    //}

    //void RotateHead()
    //{
    //    transform.rotation = Quaternion.Slerp(transform.rotation, playerRot, rotSpeed * Time.deltaTime);
    //}

    // Used for turret rotation
    public void MoveXNormal()
    {
        //if (Input.GetKey("e"))
        //{
        //    headObject.transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
        //}
        //else if (Input.GetKey("q"))
        //{
        //    headObject.transform.Rotate(-Vector3.up * rotateSpeed * Time.deltaTime);
        //}
        //else
        //{

        //}

        //if (turretTransform)
        //{
        //    Vector3 turretLookDirection = Input.
        //}
    }

    public void MoveXInverted()
    {
        {
            // Move turret to the left
            if (Input.mousePosition.x > centerOfScreen)
            {
                headObject.transform.Rotate(Vector3.down * (CameraMovement.cameraRotateSpeed) / 2 * Time.deltaTime);
            }

            // Move turret to the right
            if (Input.mousePosition.x < centerOfScreen)
            {
                headObject.transform.Rotate(Vector3.up * (CameraMovement.cameraRotateSpeed) / 2 * Time.deltaTime);
            }
        }
    }

    public void FireMechanism()
    {
        if (Input.GetMouseButtonDown(KeyBindings.clickIndex) && bulletActive == false && !PauseMenu.GameIsPaused)
        {
            // Creates a copy of the bullet, and captures its Rigibody (into bulletRB)
            bulletCopy = PhotonNetwork.Instantiate("Bullet", bulletSpawnLocation.transform.position, Quaternion.Euler(0, 0, 0));


            // Applies the launching force to the bullet and sets its status to active (true)
            bulletCopy.GetComponent<Rigidbody>().AddRelativeForce(((bulletSpawnLocation.transform.forward * bulletSpeed) + (bulletSpawnLocation.transform.up * bulletArch)), ForceMode.Impulse);
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
}
