using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviourPun, IPunObservable
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



    private GameObject bulletCopy;          // Copy of the bullet that gets launched
    public GameObject bulletSpawnLocation; // Where the bull is instantiated
    public static Color bulletColor = Color.red;


    private float timeElapsed = 0f;
    private bool bulletActive = false;
    private float reloadSpeed = (float)CharacterInfo.info[CharacterMenu.currentModelIndex]["reloadSpeed"];
    private float bulletSpeed = (float)CharacterInfo.info[CharacterMenu.currentModelIndex]["bulletSpeed"];
    private float bulletArch = (float)CharacterInfo.info[CharacterMenu.currentModelIndex]["bulletArch"];
    public static float reloadProgress;
    public static float reloadMultiplier = 1.0f;
    List<string> paintballs = new List<string>() { "bullet1", "bullet2", "bullet3", "bullet4", "bullet5"};

    private void Awake()
    {
        reloadProgress = 1.0f;
    }
    void FixedUpdate()
    {
        SetKeyBindings();

        if (/*photonView.IsMine &&*/ !PauseMenu.GameIsPaused)
        {
            MovePlayer();
            FireMechanism();

            //if (GameLoad.isXInverted)
            //{
            //    MoveXInverted();
            //}
            //else
            //{
            //    MoveXNormal();
            //}
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

        if (Input.mousePosition.x < x_left)
        {
            headObject.transform.Rotate(-Vector3.up * 30.0f * Time.deltaTime);
        }
        if (Input.mousePosition.x > x_right)
        {

            headObject.transform.Rotate(Vector3.up * 30.0f * Time.deltaTime);
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
            bulletCopy.GetComponent<Rigidbody>().AddRelativeForce(((bulletSpawnLocation.transform.forward * bulletSpeed) + (bulletSpawnLocation.transform.up * bulletArch)), ForceMode.Impulse);
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
                //GameObject splatter = PhotonNetwork.Instantiate("Splatter", bulletCopy.transform.position, Quaternion.Euler(0, 0, 0));
                //Renderer rend = splatter.GetComponent<Renderer>();
                //rend.material.color = bulletColor;
                //PhotonNetwork.Destroy(bulletCopy);
                timeElapsed = 0f;
                bulletActive = false;
            }
        }
    }

    private Color GetRandomColor()
    {
        List<Color> colors = new List<Color>() { Color.blue, Color.green, Color.red, Color.yellow, Color.cyan, Color.gray, Color.magenta, Color.white};

        return colors[Random.Range(0, colors.Count)];
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
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
}
