/*********************************************************************/
/*         Team: Cannon Corps                                        */
/*       Author: Corey Good                                          */
/* Date Created: September 1, 2019                                   */
/* Date Updated: October 26, 2019                                    */
/*      Purpose: Handles the camera positioning                      */
/*********************************************************************/
using UnityEngine;
using Photon.Pun;

public class CameraMovement : MonoBehaviourPun
{
    // Camera's main position and its offset
    private Vector3 defaultCamera;
    public  Vector3 offsetPosition;

    // Deal with the technicals of camera rotation and position towards player
    private GameObject player;
    private Space offsetPositionSpace = Space.Self;
    private bool lookAt = true;

    private float x_center = (Screen.width / 2.0f);
    private float movementSpeed;



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
            // Handles the camera position in the map
        }
        //else
        //{    
            // Handles the zoom of the camera based on the scroll wheel
            //if (Input.mouseScrollDelta[1] < 0)
            //{
            //    offsetPosition.z -= 0.35f;
            //}
            //if (Input.mouseScrollDelta[1] > 0)
            //{
            //    offsetPosition.z += 0.35f;
            //}
        //}
        
    }

    public void LateUpdate()
    {
        Debug.Log(Input.mousePosition.x);
        if(player != null)
        { 
            // Resets the camera to the default position
            if (Input.GetKey("c"))
            {
                offsetPosition = defaultCamera;
            }

            ////Computes the camera rotation in order to always look at the player
            if (lookAt)
            {
                transform.LookAt(player.transform);
            }

            if (Input.mousePosition.x < x_center)
            {
                movementSpeed = (x_center - Input.mousePosition.x) / x_center * 100.0f;
                transform.RotateAround(player.transform.position, -Vector3.up, movementSpeed * Time.deltaTime);
            }
            if (Input.mousePosition.x > x_center)
            {
                movementSpeed = (x_center - Input.mousePosition.x) / x_center * 100.0f;
                transform.RotateAround(player.transform.position, Vector3.up, -(movementSpeed) * Time.deltaTime);
            }

        }
    }
}

