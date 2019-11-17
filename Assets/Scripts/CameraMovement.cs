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
    public Transform cameraTarget;
    //private Transform cameraDefault;
    public Transform tankCamera;
    private Vector3 cameraOffset;

    private float cameraDistance;
    private float cameraMin = 0.2f;
    private float cameraMax = 2.2f;

    public GameObject player;

    public static float cameraRotateSpeed = 500.0f;
    public float cameraRotationSpeed = 5.0f;
    private float cameraZoom = 0.1f;
    private float movementSpeed;
    private float targetUp = 1.2f;

    public bool lookAtTank = false;
    public bool rotateAroundTank = true;

    public void Start()
    {
        //cameraDefault = cameraTarget;
        cameraOffset  = tankCamera.position - cameraTarget.position;
    }

    public void FixedUpdate()
    {
        if (!PauseMenu.GameIsPaused) //       if (photonView.IsMine && !PauseMenu.GameIsPaused)
        {
            ScrollZoom();
            ScrollLimiter();
        }
    }

    public void LateUpdate()
    {
        if (!PauseMenu.GameIsPaused) //       if (photonView.IsMine && !PauseMenu.GameIsPaused)
        {
            CameraControl();
        }

        cameraTarget.position = new Vector3(player.transform.position.x, player.transform.position.y + targetUp, player.transform.position.z);

        if (lookAtTank)
        {
            tankCamera.transform.LookAt(cameraTarget);
        }
    }

    public void CameraControl()
    {
        if (Input.GetAxis("Mouse X") > 0.0f) //Debug.Log("X value: " + Input.GetAxis("Mouse X"));
        {
            movementSpeed = Input.GetAxis("Mouse X") * cameraRotateSpeed;

            tankCamera.transform.RotateAround(player.transform.position, Vector3.up, movementSpeed * Time.deltaTime);
        }

        //Rotates camera right
        if (Input.GetAxis("Mouse X") < 0.0f)
        {
            movementSpeed = Input.GetAxis("Mouse X") * cameraRotateSpeed;

            tankCamera.transform.RotateAround(player.transform.position, Vector3.up, movementSpeed * Time.deltaTime);
        }

        ////Rotates camera up        
        //if (Input.GetAxis("Mouse Y") > 0.0f) //Debug.Log("Y value: " + Input.GetAxis("Mouse Y"));
        //{
        //    movementSpeed = Input.GetAxis("Mouse Y") * cameraRotateSpeed;

        //    tankCamera.transform.RotateAround(player.transform.position, Vector3.left, movementSpeed * Time.deltaTime);
        //}

        ////Rotates camera down
        //if (Input.GetAxis("Mouse Y") < 0.0f)
        //{
        //    movementSpeed = Input.GetAxis("Mouse Y") * cameraRotateSpeed;

        //    tankCamera.transform.RotateAround(player.transform.position, Vector3.left, movementSpeed * Time.deltaTime);
        //}
    }

    public void ScrollZoom()
    {
        if (cameraDistance > cameraMin && cameraDistance < cameraMax)
        {
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                tankCamera.Translate(0, 0, -cameraZoom);
            }

            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                tankCamera.Translate(0, 0, cameraZoom);
            }
        }

        cameraOffset = tankCamera.position - cameraTarget.position;
    }

    public void ScrollLimiter()
    {
        //Debug.Log("distance: " + cameraDistance);
        //Debug.Log("min: " + cameraMin);
        //Debug.Log("max: " + cameraMax);

        if (cameraDistance < cameraMin)
        {
            cameraOffset.y = 0.21f;
        }

        if (cameraDistance > cameraMax)
        {
            cameraOffset.y = 2.19f;
        }

        cameraDistance = cameraOffset.y;
    }
}