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
    public GameObject player;

    public Transform target;
    public Transform cameraTransform;  //same as tankCameraC, needed for transform.LookAt and transform.RotateAround
    public Camera tankCamera;       //same as tankCameraT, needed for camera.fieldOfView

    public static float cameraRotateSpeed = 500.0f;

    public void FixedUpdate()
    {
        if (!PauseMenu.GameIsPaused) // might need (photonView.IsMine && !PauseMenu.GameIsPaused) for network
        {
            ZoomCamera();
        }
    }

    public void LateUpdate()
    {
        SetCameraTarget();

        if (!PauseMenu.GameIsPaused)
        {
            OrbitCamera();
        }

        LookAtCameraTarget();
    }

    public void SetCameraTarget()
    {
        float cameraTargetOffset = 1.0f;

        target.position = new Vector3(player.transform.position.x, (player.transform.position.y + cameraTargetOffset), player.transform.position.z);
    }

    public void OrbitCamera()
    {
        float orbitSpeed = Input.GetAxis("Mouse X") * cameraRotateSpeed * Time.deltaTime;

        cameraTransform.transform.RotateAround(player.transform.position, Vector3.up, orbitSpeed);
    }

    public void LookAtCameraTarget()
    {
        cameraTransform.transform.LookAt(target);
    }

    public void ZoomCamera()
    {
        float zoomFOV = tankCamera.fieldOfView;
        float zoomDistance = 7.0f;
        float zoomMin = 15.0f;
        float zoomMax = 75.0f;

        zoomFOV -= Input.GetAxis("Mouse ScrollWheel") * zoomDistance;
        zoomFOV = Mathf.Clamp(zoomFOV, zoomMin, zoomMax);

        tankCamera.fieldOfView = zoomFOV;
    }
}
///*********************************************************************/
///*         Team: Cannon Corps                                        */
///*       Author: Corey Good                                          */
///* Date Created: September 1, 2019                                   */
///* Date Updated: October 26, 2019                                    */
///*      Purpose: Handles the camera positioning                      */
///*********************************************************************/

//using Photon.Pun;
//using UnityEngine;

//public class CameraMovement : MonoBehaviourPun
//{
//    // Camera's main position and its offset
//    private Vector3 defaultCamera;

//    public Vector3 offsetPosition;

//    // Deal with the technicals of camera rotation and position towards player
//    private GameObject player;

//    private Space offsetPositionSpace = Space.Self;
//    private bool lookAt = true;

//    public void Start()
//    {
//        offsetPosition = defaultCamera = new Vector3(0.0f, 3.0f, -11.0f);
//    }

//    public void FixedUpdate()
//    {
//        // Assigns the camera to the player that spawns. Change to player ID in order to deal with multiple player later on
//        if (player == null)
//        {
//            player = GameObject.FindWithTag("CharacterModel");
//            //Debug.Log(player);
//        }
//        else
//        {
//            // Handles the zoom of the camera based on the scroll wheel
//            if (Input.mouseScrollDelta[1] < 0)
//            {
//                offsetPosition.z -= 0.35f;
//            }
//            if (Input.mouseScrollDelta[1] > 0)
//            {
//                offsetPosition.z += 0.35f;
//            }
//        }
//    }

//    public void LateUpdate()
//    {
//        if (player != null)
//        {
//            // Resets the camera to the default position
//            if (Input.GetKey("c"))
//            {
//                offsetPosition = defaultCamera;
//            }

//            // Handles the camera position in the map
//            if (offsetPositionSpace == Space.Self)
//            {
//                transform.position = player.transform.TransformPoint(offsetPosition);
//            }
//            else
//            {
//                transform.position = player.transform.position + offsetPosition;
//            }

//            //Computes the camera rotation in order to always look at the player
//            if (lookAt)
//            {
//                transform.LookAt(player.transform);
//            }
//            else
//            {
//                transform.rotation = player.transform.rotation;
//            }
//        }
//    }
//}