using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Photon.Pun;

public class CollisionDetection : MonoBehaviourPun
{
    public GameObject objectName; // Name of the object using the CollisionDetection script

    //private float killPoints = 10.0f;

    private PaintballInfo enemyPlayer;

    void OnCollisionEnter(Collision collisionInfo) // Called when this collider/rigidbody has begun touching another rigidbody/collider
    {
        if (photonView.IsMine)
        {

            if (collisionInfo.collider.name == "Bullet(Clone)")
            {
                LoadUI.currentHealth = ((float)LoadUI.currentHealth - (float)PaintballInfo.paintballInfo["paintballDamage"]);
                enemyPlayer = collisionInfo.gameObject.GetComponent<PaintballInfo>();
                UnityEngine.Debug.Log("You were hit by: " + enemyPlayer);               

            }

        }
    }
}
