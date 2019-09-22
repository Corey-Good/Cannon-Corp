
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{


    private GameObject player;

    [SerializeField]
    private Vector3 offsetPosition;

    [SerializeField]
    private Space offsetPositionSpace = Space.Self;

    [SerializeField]
    private bool lookAt = true;



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

    public void Refresh()
    {

        // compute position
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

