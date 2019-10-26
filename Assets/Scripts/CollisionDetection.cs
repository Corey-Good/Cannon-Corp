using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Photon.Pun;

public class CollisionDetection : MonoBehaviourPun
{
    public GameObject objectName; // Name of the object using the CollisionDetection script

    private float killPoints = 10.0f;

    private PaintballInfo enemyPlayer;

    void OnCollisionEnter(Collision collisionInfo) // Called when this collider/rigidbody has begun touching another rigidbody/collider
    {
        if (photonView.IsMine)
        {

            if (collisionInfo.collider.name == "Bullet(Clone)")
            {
                //UnityEngine.Debug.Log("Bullet Damange: " + PaintballInfo.paintballInfo["paintballDamage"]);

                LoadUI.currentHealth = ((float)LoadUI.currentHealth - (float)PaintballInfo.paintballInfo["paintballDamage"]);

                //UnityEngine.Debug.Log("Remaining Tank Health: " + LoadUI.currentHealth);

                //LoadUI.score += killPoints;
                enemyPlayer = collisionInfo.gameObject.GetComponent<PaintballInfo>();
                UnityEngine.Debug.Log("You were hit by: " + enemyPlayer.GetName());
                

            }
            else
            {
                // UnityEngine.Debug.Log(objectName + " collided with " + collisionInfo.collider.name);
            }
        }
    }
}
