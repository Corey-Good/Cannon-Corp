/************************************************************************/
/* Author:  */
/* Date Created: */
/* Last Modified Date: */
/* Modified By: */
/************************************************************************/

using Photon.Pun;
using UnityEngine;

public class CameraMovement : MonoBehaviourPun
{
    public static float cameraRotateSpeed = 500.0f;
    public Transform cameraTransform;
    public GameObject player;

    //same as tankCameraC, needed for transform.LookAt and transform.RotateAround
    public Camera tankCamera;

    public Transform target;
    //same as tankCameraT, needed for camera.fieldOfView
    public void Awake()
    {
        if (!photonView.IsMine)
        {
            tankCamera.enabled = false;
            return;
        }
    }

    public void FixedUpdate()
    {
        if (!photonView.IsMine) { return; }
        if (!PauseMenu.GameIsPaused) // might need (photonView.IsMine && !PauseMenu.GameIsPaused) for network
        {
            ZoomCamera();
        }
    }

    public void LateUpdate()
    {
        if (!photonView.IsMine) return;
        SetCameraTarget();

        if (!PauseMenu.GameIsPaused)
        {
            OrbitCamera();
        }

        LookAtCameraTarget();
    }

    public void LookAtCameraTarget()
    {
        cameraTransform.transform.LookAt(target);
    }

    public void OrbitCamera()
    {
        float orbitSpeed = Input.GetAxis("Mouse X") * cameraRotateSpeed * Time.deltaTime;

        cameraTransform.transform.RotateAround(player.transform.position, Vector3.up, orbitSpeed);
    }

    public void SetCameraTarget()
    {
        float cameraTargetOffset = 1.0f;

        target.position = new Vector3(player.transform.position.x, (player.transform.position.y + cameraTargetOffset), player.transform.position.z);
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