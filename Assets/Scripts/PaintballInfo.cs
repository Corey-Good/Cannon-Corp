using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Photon.Pun;

public class PaintballInfo : MonoBehaviourPun
{ 
    public GameObject objectName; // Name of the object using the PaintballInfo script
   
    public static  Hashtable paintballInfo = new Hashtable()
    {
        { "playerName",      NameGenerator.UserName},
        { "paintballDamage", CharacterInfo.info[CharacterMenu.currentModelIndex]["bulletDamage"]}
    };
    

    public string GetName()
    {
        return (string)paintballInfo["playerName"];
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (photonView.IsMine)
        {
            GameObject splatter = PhotonNetwork.Instantiate("Splatter", objectName.transform.position, Quaternion.Euler(0, 0, 0));
            Renderer rend = splatter.GetComponent<Renderer>();
            rend.material.color = PlayerMovement.bulletColor;
            PhotonNetwork.Destroy(objectName);

        }
}
}
