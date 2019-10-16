using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public GameObject objectName; // Name of the object using the CollisionDetection script

    private float killPoints = 10.0f;

    void OnCollisionEnter (Collision collisionInfo) // Called when this collider/rigidbody has begun touching another rigidbody/collider
    {
        if (PaintballLauncher.bulletActive == true)
        {
            if (collisionInfo.collider.tag == "Paintball")
            {
                UnityEngine.Debug.Log("Bullet Damange: " + PaintballInfo.paintballInfo["paintballDamage"]);

                LoadUI.currentHealth = ((float)LoadUI.currentHealth - (float)PaintballInfo.paintballInfo["paintballDamage"]);

                UnityEngine.Debug.Log("Remaining Tank Health: " + LoadUI.currentHealth);

                LoadUI.score += killPoints;
            }
        }
        else 
        {
            UnityEngine.Debug.Log(objectName + " collided with " + collisionInfo.collider.name);
        }
    }
}
