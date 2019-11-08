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

    // Deal with the technicals of camera rotation and position towards player
    public GameObject player;

    public static float centerOfScreen = (Screen.width / 2.0f);
    private float movementSpeed;
    public static float cameraRotateSpeed = 200.0f;

    public float rotationSpeed = 1.0f;


    private float camZoom = 0.5f;
    public Transform tankCamera;
    public Transform cameraTarget;
    private Vector3 cameraOffset;

    public bool lookAtTank = false;
    public bool rotateAroundTank = true;
    public float cameraRotationSpeed = 5.0f;


    private float targetUp = 1.2f;


    public void Start()
    {
        cameraOffset = tankCamera.position - cameraTarget.position;
    }

    public void FixedUpdate()
    {
        ScrollZoom();

    }

    public void LateUpdate()
    {
        CameraControl();

        cameraTarget.position = new Vector3(player.transform.position.x, player.transform.position.y + targetUp, player.transform.position.z);

        if (lookAtTank)
        {
            tankCamera.transform.LookAt(cameraTarget);
        }


    }

    public void CameraControl()
    {
        //Debug.Log("X value: " + Input.GetAxis("Mouse X"));
        //Debug.Log("Y value: " + Input.GetAxis("Mouse Y"));
        //Rotates camera to the left
        if (Input.GetAxis("Mouse X") > 0.0f)
        {
            movementSpeed =Input.GetAxis("Mouse X") * cameraRotateSpeed; /*(centerOfScreen - Input.mousePosition.x) / centerOfScreen*/

            tankCamera.transform.RotateAround(player.transform.position, Vector3.down, movementSpeed * Time.deltaTime);
        }

        //Rotates camera to the right
        if (Input.GetAxis("Mouse X") < 0.0f)
        {

            movementSpeed = Input.GetAxis("Mouse X") * cameraRotateSpeed;

            tankCamera.transform.RotateAround(player.transform.position, Vector3.up, -(movementSpeed) * Time.deltaTime);
        }
    }

    public void ScrollZoom()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            tankCamera.Translate(0, 0, -camZoom);
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            tankCamera.Translate(0, 0, camZoom);
        }

        cameraOffset = tankCamera.position - cameraTarget.position;
    }
    

}