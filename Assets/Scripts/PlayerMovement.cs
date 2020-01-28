/************************************************************************/
/* Author:  */
/* Date Created: */
/* Last Modified Date: */
/* Modified By: */
/************************************************************************/

using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviourPun, IPunObservable
{
    public static Color bulletColor = Color.red;
    public static float movementMultiplier = 1.0f;
    public static int playerViewId;
    public static float reloadMultiplier = 1.0f;
    public static float reloadProgress;
    public static float rotateMultiplier = 8.0f;
    public GameObject baseObject;
    public GameObject bulletSpawnLocation;
    public GameObject headObject;
    // Where the bullet is instantiated
    public Camera tankCamera;

    public Transform turretObject;
    private string backwardbutton;
    private bool bulletActive = false;
    private float bulletArch = (float)CharacterInfo.info[CharacterMenu.currentModelIndex]["bulletArch"];
    private GameObject bulletCopy;
    private float bulletSpeed = (float)CharacterInfo.info[CharacterMenu.currentModelIndex]["bulletSpeed"];
    private Vector3 cursorPosition;
    private string forwardbutton;
    private string leftbutton;
    private float movementForce = (float)CharacterInfo.info[CharacterMenu.currentModelIndex]["movementForce"];
    private List<string> paintballs = new List<string>() { "bullet1", "bullet2", "bullet3", "bullet4", "bullet5" };
    private float reloadSpeed = (float)CharacterInfo.info[CharacterMenu.currentModelIndex]["reloadSpeed"];
    private string rightbutton;
    private float rotateSpeed = (float)CharacterInfo.info[CharacterMenu.currentModelIndex]["rotationSpeed"];
    private float timeElapsed = 0f;

    private Vector3 turretFinalLookDirection;

    // Copy of the bullet that gets launched
    private float turretLagSpeed = 0.7f;
    public void FireMechanism()
    {
        Renderer bulletRender = null;

        if (Input.GetMouseButtonDown(KeyBindings.clickIndex) && bulletActive == false && !PauseMenu.GameIsPaused)
        {
            bulletColor = GetRandomColor();
            // Creates a copy of the bullet, and captures its Rigibody (into bulletRB)
            bulletCopy = PhotonNetwork.Instantiate(paintballs[CharacterMenu.currentModelIndex], bulletSpawnLocation.transform.position, Quaternion.Euler(0, 0, 0));
            bulletRender = bulletCopy.GetComponent<Renderer>();

            bulletRender.material.color = bulletColor;

            // Applies the launching force to the bullet and sets its status to active (true)
            bulletCopy.GetComponent<Rigidbody>().AddRelativeForce(((bulletSpawnLocation.transform.forward * Vector3.Distance(this.transform.position, cursorPosition)) + (bulletSpawnLocation.transform.up * bulletArch)), ForceMode.Impulse);
            bulletActive = true;
        }

        if (bulletActive)
        {
            // Increase time and update the reloadBar progress
            timeElapsed += Time.deltaTime;
            reloadProgress = timeElapsed / (reloadSpeed * reloadMultiplier);

            // When a bullet is reloaded, delete the previouis copy and reset timer
            if (timeElapsed >= (reloadSpeed * reloadMultiplier))
            {
                timeElapsed = 0f;
                bulletActive = false;
            }
        }
    }

    public void MovePlayer()
    {
        // Move play forwards and backwards, regenerate health when no movement is detected
        if (Input.GetKey(forwardbutton))
        {
            baseObject.transform.position += transform.forward * Time.deltaTime * movementForce * movementMultiplier;
            // Rotate model left and right
            if (Input.GetKey(rightbutton))
            {
                baseObject.transform.Rotate(Vector3.up * rotateSpeed * rotateMultiplier * Time.deltaTime);
            }
            else if (Input.GetKey(leftbutton))
            {
                baseObject.transform.Rotate(-Vector3.up * rotateSpeed * rotateMultiplier * Time.deltaTime);
            }
        }
        else if (Input.GetKey(backwardbutton))
        {
            baseObject.transform.position += -transform.forward * Time.deltaTime * movementForce * movementMultiplier;
            // Rotate model left and right
            if (Input.GetKey(rightbutton))
            {
                baseObject.transform.Rotate(Vector3.up * rotateSpeed * rotateMultiplier * Time.deltaTime);
            }
            else if (Input.GetKey(leftbutton))
            {
                baseObject.transform.Rotate(-Vector3.up * rotateSpeed * rotateMultiplier * Time.deltaTime);
            }
        }
        else
        {
            if (LoadUI.currentHealth < LoadUI.totalHealth)
            {
                LoadUI.currentHealth += 0.01f;
            }
        }



        // Decrease health, for testing purposes
        if (Input.GetKey("h"))
        {
            LoadUI.currentHealth -= 1.0f;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(baseObject.transform.position);
            stream.SendNext(headObject.transform.position);
        }
        else
        {
            baseObject.transform.position = (Vector3)stream.ReceiveNext();
            headObject.transform.position = (Vector3)stream.ReceiveNext();
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

    public void TurretRotation()
    {
        Ray screenRay = tankCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(screenRay, out hit))
        {
            cursorPosition = hit.point;
        }

        Vector3 turretLookDirection = cursorPosition - turretObject.position;
        turretLookDirection.y = 0.0f;

        turretFinalLookDirection = Vector3.RotateTowards(turretFinalLookDirection, turretLookDirection, turretLagSpeed * Time.deltaTime, 10.0f);
        turretObject.rotation = Quaternion.LookRotation(turretFinalLookDirection);
    }

    private void Awake()
    {
        reloadProgress = 1.0f;
        //PhotonView view = baseObject.GetPhotonView();
        //playerViewId =  view.ViewID;
        // Debug.Log(playerViewId);
    }

    private void FixedUpdate()
    {
        SetKeyBindings();

        //if (photonView.IsMine && !PauseMenu.GameIsPaused)
        //{
            PhotonView view = baseObject.GetPhotonView();
            playerViewId = view.ViewID;
            MovePlayer();
            FireMechanism();

            TurretRotation();
        //}
    }
    private Color GetRandomColor()
    {
        List<Color> colors = new List<Color>() { Color.blue, Color.green, Color.red, Color.yellow, Color.cyan, Color.gray, Color.magenta, Color.white };

        return colors[Random.Range(0, colors.Count)];
    }
}