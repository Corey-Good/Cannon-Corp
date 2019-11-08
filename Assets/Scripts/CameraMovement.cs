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
    //private Space offsetPositionSpace = Space.Self;
    //private bool lookAt = true;

    public static float centerOfScreen = (Screen.width / 2.0f);
    private float movementSpeed;
    public static float cameraRotateSpeed = 100.0f;

    public float rotationSpeed = 1.0f;

    private float mouseX, mouseY;
    float camZoom = 0.5f;

    public Transform tankCamera;
    public Transform cameraTarget;
    private Vector3 cameraOffset;
    [Range(0.1f, 1.0f)]
    private float cameraSmoothness = 0.5f;
    public bool lookAtTank = false;
    public bool rotateAroundTank = true;
    public float cameraRotationSpeed = 5.0f;

    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        cameraOffset = tankCamera.position - cameraTarget.position;
    }

    public void FixedUpdate()
    {
        // Assigns the camera to the player that spawns. Change to player ID in order to deal with multiple player later on
        //if (player == null)
        //{
        //    // Handles the camera position in the map
        //    player = GameObject.FindWithTag("CharacterModel");
        //}

        CursorLock();

        ScrollZoom();
    }

    public void LateUpdate()
    {
        CameraControl();

        CameraFollow();

        //CameraRotation();

        //ClampCamera();
    }

    public void CameraControl()
    {
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;
        mouseY = Mathf.Clamp(mouseY, -30, 30);

        tankCamera.rotation = Quaternion.Euler(mouseY, mouseX, 0);

        ////Rotates camera to the left
        //if (Input.mousePosition.x < centerOfScreen)
        //{
        //    movementSpeed = (centerOfScreen - Input.mousePosition.x) / centerOfScreen * cameraRotateSpeed;

        //    tankCamera.transform.RotateAround(player.transform.position, Vector3.down, movementSpeed * Time.deltaTime);
        //}

        ////Rotates camera to the right
        //if (Input.mousePosition.x > centerOfScreen)
        //{

        //    movementSpeed = (centerOfScreen - Input.mousePosition.x) / centerOfScreen * cameraRotateSpeed;

        //    tankCamera.transform.RotateAround(player.transform.position, Vector3.up, -(movementSpeed) * Time.deltaTime);
        //}
    }

    public void CameraFollow()
    {
        Vector3 cameraPosition = cameraTarget.position + cameraOffset;

        tankCamera.position = Vector3.Slerp(tankCamera.position, cameraPosition, cameraSmoothness);

        if (lookAtTank)
        {
            transform.LookAt(cameraTarget);
        }
    }

    public void CameraRotation()
    {
        if (rotateAroundTank)
        {
            Quaternion cameraTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * cameraRotationSpeed, Vector3.up);

            cameraOffset = cameraTurnAngle * cameraOffset;
        }

        if (lookAtTank || rotateAroundTank)
        {
            transform.LookAt(cameraTarget);
        }
    }

    public void CursorLock()
    {
        // Locks the cursor to the center of the screen
        if (Input.GetKey("1"))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;
        }

        // Unlocks the cursor
        if (Input.GetKey("2"))
        {
            Cursor.lockState = CursorLockMode.None;
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
    
    private void ClampCamera()
    {
        Vector3 clampMovement = tankCamera.position;


        //clampMovement.x = Mathf.Clamp(clampMovement.x, -1.0f, 10.0f);
        //clampMovement.y = Mathf.Clamp(clampMovement.y, 0.0f, 17.0f);
        //clampMovement.z = Mathf.Clamp(clampMovement.z, 0.0f, 50.0f);

        tankCamera.position = clampMovement;
    }
}