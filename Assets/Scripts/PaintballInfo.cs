/************************************************************************/
/* Author:  */
/* Date Created: */
/* Last Modified Date: */
/* Modified By: */
/************************************************************************/

using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintballInfo : MonoBehaviourPun
{
    public static Hashtable paintballInfo = new Hashtable()
    {
        { "playerName",      NameGenerator.UserName},
        { "paintballDamage", CharacterInfo.info[CharacterMenu.currentModelIndex]["bulletDamage"]}
    };

    //List<GameObject> splatters = new List<GameObject>();

    public GameObject objectName; // Name of the object using the PaintballInfo script
    public string GetName()
    {
        return (string)paintballInfo["playerName"];
    }

    private void OnCollisionEnter(Collision collisionInfo)
    {
        if (photonView.IsMine)
        {
            if (collisionInfo.collider.tag == "Player")
            {
                LoadUI.score += 10;
            }
            GameObject splatter = PhotonNetwork.Instantiate("Splatter", objectName.transform.position, Quaternion.Euler(-90, 0, 0));
            //splatters.Add(splatter);
            Renderer rend = splatter.GetComponent<Renderer>();
            rend.material.color = PlayerMovement.bulletColor;
            if (collisionInfo.collider.tag != "Player")
                PhotonNetwork.Destroy(objectName);
        }
    }
}