using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class HeadRoation : MonoBehaviourPun
{

    private float x_left = (Screen.width / 2.0f) + (Screen.width * 0.12f);
    private float x_right = (Screen.width / 2.0f) - (Screen.width * 0.12f);

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveXNormal();
    }

    public void MoveXNormal()
    {
        if (photonView.IsMine)
        {        
            if (Input.mousePosition.x < x_left)
            {
                transform.Rotate(-Vector3.up * 30.0f * Time.deltaTime);
            }
            if (Input.mousePosition.x > x_right)
            {
            
                transform.Rotate(Vector3.up * 30.0f * Time.deltaTime);
            }
        }
    }
}
