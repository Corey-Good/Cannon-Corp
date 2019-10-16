using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PaintballInfo : MonoBehaviour
{
    public GameObject objectName; // Name of the object using the PaintballInfo script

    static public Hashtable paintballInfo = new Hashtable()
    {
        { "playerName",      NameGenerator.UserName},
        { "paintballDamage", CharacterInfo.info[CharacterMenu.currentModelIndex]["bulletDamage"]}
    };

    public string GetName()
    {
        return (string)paintballInfo["playerName"];
    }
    //void OnCollisionEnter(Collision collisionInfo) // Called when this collider/rigidbody has begun touching another rigidbody/collider
    //{
    //    if (PaintballLauncher.bulletActive == true)
    //    {
    //        if (collisionInfo.collider.tag == "Bullet")
    //        {
    //            UnityEngine.Debug.Log("Bullet Damange: " + CharacterInfo.info[CharacterMenu.currentModelIndex]["bulletDamage"]);

    //            CharacterInfo.info[CharacterMenu.currentModelIndex]["healthPoints"] = ((float)CharacterInfo.info[CharacterMenu.currentModelIndex]["healthPoints"] - (float)CharacterInfo.info[CharacterMenu.currentModelIndex]["bulletDamage"]);

    //            UnityEngine.Debug.Log("Remaining Tank Health: " + CharacterInfo.info[CharacterMenu.currentModelIndex]["healthPoints"]);

    //            LoadUI.score += killPoints;
    //        }
    //    }
    //    else
    //    {
    //        //UnityEngine.Debug.Log(objectName + " collided with " + collisionInfo.collider.name);
    //    }
    //}
}
