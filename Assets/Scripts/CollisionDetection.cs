using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Photon.Pun;

public class CollisionDetection : MonoBehaviourPun
{
    public GameObject objectName; // Name of the object using the CollisionDetection script

    private float damage = 0f;

    private PaintballInfo enemyPlayer;

    void OnCollisionEnter(Collision collisionInfo) // Called when this collider/rigidbody has begun touching another rigidbody/collider
    {
        if (photonView.IsMine)
        {
            switch(collisionInfo.collider.name)
            {

                case "bullet1":
                    damage = 10.0f;
                    break;
                case "bullet2":
                    damage = 7.0f;
                    break;
                case "bullet3":
                    damage = 25.0f;
                    break;
                case "bullet4":
                    damage = 12.0f;
                    break;
                case "bullet5":
                    damage = 10.0f;
                    break;
            }

            LoadUI.currentHealth -= damage;

            PhotonNetwork.Destroy(collisionInfo.collider.gameObject);

        }
    }
}
