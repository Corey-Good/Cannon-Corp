﻿/*********************************************************************/
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
    public GameObject   player;

    public Transform    cameraTarget;
    public Transform    tankCameraT;                 //same as tankCameraC, needed for transform.LookAt and transform.RotateAround
    public Camera       tankCameraC;                 //same as tankCameraT, needed for camera.fieldOfView

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

        cameraTarget.position = new Vector3(player.transform.position.x, (player.transform.position.y + cameraTargetOffset), player.transform.position.z);
    }

    public void OrbitCamera()
    {
        float orbitSpeed = Input.GetAxis("Mouse X") * cameraRotateSpeed * Time.deltaTime;

        tankCameraT.transform.RotateAround(player.transform.position, Vector3.up, orbitSpeed);
    }

    public void LookAtCameraTarget()
    {
        tankCameraT.transform.LookAt(cameraTarget);
    }

    public void ZoomCamera()
    {
        float zoomFOV      = tankCameraC.fieldOfView;
        float zoomDistance = 1.0f;
        float zoomMin      = 15.0f;
        float zoomMax      = 75.0f;

        zoomFOV -= Input.GetAxis("Mouse ScrollWheel") * zoomDistance;
        zoomFOV =  Mathf.Clamp(zoomFOV, zoomMin, zoomMax);

        tankCameraC.fieldOfView = zoomFOV;
    }
}