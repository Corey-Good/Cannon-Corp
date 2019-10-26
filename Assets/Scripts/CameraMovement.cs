/*********************************************************************/
/*         Team: Cannon Corps                                        */
/*       Author: Corey Good                                          */
/* Date Created: September 1, 2019                                   */
/* Date Updated: October 14, 2019                                    */
/*      Purpose: Handles the camera positioning                      */
/*********************************************************************/
using UnityEngine;
using Photon.Pun;

public class CameraMovement : MonoBehaviourPun
{
    // Camera's main position and its offset
    private Vector3 defaultCamera;
    public  Vector3 offsetPosition;

    // Max value of movement for the screen
    private float Y_MIN = 2.00f;
    private float Y_MAX = 16.00f;
    private float X_MIN = -10.0f;
    private float X_MAX = 10.0f;

    // Speed the camera will move
    private float cameraMovementSpeedX = 0.18f;
    private float cameraMovementSpeedY = 0.20f;

    // Determines when the camera starts moving
    private float x_left  = (Screen.width / 2.0f) + (Screen.width * 0.12f);
    private float x_right = (Screen.width / 2.0f) - (Screen.width * 0.12f);
    private float y_up    = (Screen.height / 2.0f) + (Screen.height * 0.25f);
    private float y_down  = (Screen.height / 2.0f) - (Screen.height * 0.1f);

    // Deal with the technicals of camera rotation and position towards player
    private GameObject player;
    private Space offsetPositionSpace = Space.Self;
    private bool lookAt = true;

    public void Start()
    {
        offsetPosition = defaultCamera = new Vector3(0.0f, 3.0f, -11.0f);
    }

    public void FixedUpdate()
    {
        
        // Assigns the camera to the player that spawns. Change to player ID in order to deal with multiple player later on
        if (player == null)
        {
            player = GameObject.FindWithTag("CharacterModel");
            //Debug.Log(player);
        }
        else
        {           

            // Handles the zoom of the camera based on the scroll wheel
            if (Input.mouseScrollDelta[1] < 0)
            {
                offsetPosition.z -= 0.35f;
            }
            if (Input.mouseScrollDelta[1] > 0)
            {
                offsetPosition.z += 0.35f;
            }
        }
        
    }

    public void LateUpdate()
    {
        if(player != null)
        { 
            // Resets the camera to the default position
            if (Input.GetKey("c"))
            {
                offsetPosition = defaultCamera;
            }


            // Handles the camera position in the map
            if (offsetPositionSpace == Space.Self)
            {
                transform.position = player.transform.TransformPoint(offsetPosition);
            }
            else
            {
                transform.position = player.transform.position + offsetPosition;
            }

            //Computes the camera rotation in order to always look at the player
            if (lookAt)
            {
                transform.LookAt(player.transform);
            }
            else
            {
                transform.rotation = player.transform.rotation;
            }
        }
    }



    //if (GameLoad.isXInverted) { MoveXInverted();  } else { MoveXNormal(); }
    //if (GameLoad.isYInverted) { MoveYInverted(); } else { MoveYNormal(); }
    public void MoveXNormal()
    {
        if (Input.mousePosition.x < x_left)
        {
            if (offsetPosition.x + cameraMovementSpeedX < X_MAX)
            {
                offsetPosition.x += cameraMovementSpeedX;
            }
        }
        if (Input.mousePosition.x > x_right)
        {
            if (offsetPosition.x - cameraMovementSpeedX > X_MIN)
            {
                offsetPosition.x -= cameraMovementSpeedX;
            }
        }
    }
    public void MoveYNormal()
    {

        if (Input.mousePosition.y < y_down)
        {
            if (offsetPosition.y - cameraMovementSpeedY > Y_MIN)
            {
                offsetPosition.y -= cameraMovementSpeedY;
            }
        }
        if (Input.mousePosition.y > y_up)
        {
            if (offsetPosition.y + cameraMovementSpeedY < Y_MAX)
            {
                offsetPosition.y += cameraMovementSpeedY;
            }
        }
    }
    public void MoveXInverted()
    {
        if (Input.mousePosition.x < x_left)
        {
            if (offsetPosition.x - cameraMovementSpeedX > X_MIN)
            {
                offsetPosition.x -= cameraMovementSpeedX;
            }
        }
        if (Input.mousePosition.x > x_right)
        {

            if (offsetPosition.x + cameraMovementSpeedX < X_MAX)
            {
                offsetPosition.x += cameraMovementSpeedX;
            }
        }

    }
    public void MoveYInverted()
    {
        if (Input.mousePosition.y < y_down)
        {
            if (offsetPosition.y + cameraMovementSpeedY < Y_MAX)
            {
                offsetPosition.y += cameraMovementSpeedY;
            }
        }
        if (Input.mousePosition.y > y_up)
        {

            if (offsetPosition.y - cameraMovementSpeedY > Y_MIN)
            {
                offsetPosition.y -= cameraMovementSpeedY;
            }
        }
    }
}

