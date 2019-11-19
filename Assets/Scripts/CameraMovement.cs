/*********************************************************************/
/*         Team: Cannon Corps                                        */
/*       Author: Corey Good                                          */
/* Date Created: September 1, 2019                                   */
/* Date Updated: October 26, 2019                                    */
/*      Purpose: Handles the camera positioning                      */
/*********************************************************************/

using Photon.Pun;
using UnityEngine;

public class CameraMovement : MonoBehaviourPun
{
    // Camera's main position and its offset
    private Vector3 defaultCamera;

    public Vector3 offsetPosition;

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
        if (player != null)
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
}