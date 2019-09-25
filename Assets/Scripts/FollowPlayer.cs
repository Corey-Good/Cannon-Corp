
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public Vector3 gameCamera;
    private Vector3 defaultCamera;


    private float Y_MIN = 2.00f;
    private float Y_MAX = 16.00f;
    private float X_MIN = -10.0f;
    private float X_MAX = 10.0f;

    private float x_left = (Screen.width / 2.0f) + (Screen.width * 0.12f);
    private float x_right = (Screen.width / 2.0f) - (Screen.width * 0.12f);
    private float y_up = (Screen.height / 2.0f) + (Screen.height * 0.3f);
    private float y_down = (Screen.height / 2.0f) - (Screen.height * 0.07f);


    private GameObject player;

    [SerializeField]
    private Vector3 offsetPosition;

    [SerializeField]
    private Space offsetPositionSpace = Space.Self;

    [SerializeField]
    private bool lookAt = true;


    public void Start()
    {
        defaultCamera = gameCamera;
    }

    public void Update()
    {
        if(player == null) {
            player = GameObject.FindWithTag("CharacterModel");
        }
    }

    private void LateUpdate()
    {
        Refresh();
    }

    public void FixedUpdate()
    {

        if (player == null)
        {
            player = GameObject.FindWithTag("CharacterModel");
        }
        if (Input.mouseScrollDelta[1] < 0)
        {
            offsetPosition.z -= 0.35f;
        }

        if (Input.mouseScrollDelta[1] > 0)
        {
            offsetPosition.z += 0.35f;
        }

    }

    public void Refresh()
    {

        // This part of the script adjust the camera offset based on the mouses pixel position on the screen
        // Instead of hard coding values the pixels LxW needs to be calculated in order to be accurate on all screens!

        if (Input.mousePosition.x < x_left)
        {
            if (offsetPosition.x + 0.18f < X_MAX)
            {
                offsetPosition.x += 0.18f;
            }
        }
        if (Input.mousePosition.x > x_right)
        {
            if (offsetPosition.x - 0.18f > X_MIN)
            {
                offsetPosition.x -= 0.18f;
            }
        }
        if (Input.mousePosition.y < y_down)
        {
            if (offsetPosition.y - 0.19f  > Y_MIN)
            { 
                offsetPosition.y -= 0.19f;
            }
        }
        if (Input.mousePosition.y > y_up)
        {
            if (offsetPosition.y + 0.18f < Y_MAX)
            {
                offsetPosition.y += 0.18f;
            }
            
        }

        if (Input.GetKey("c"))
        {
            offsetPosition = defaultCamera;
        }


        if (offsetPositionSpace == Space.Self)
        {
            transform.position = player.transform.TransformPoint(offsetPosition);
        }
        else
        {
            transform.position = player.transform.position + offsetPosition;
        }

        // compute rotation
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

