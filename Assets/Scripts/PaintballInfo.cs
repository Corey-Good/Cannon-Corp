using Photon.Pun;
using System.Collections;
using UnityEngine;

public class PaintballInfo : MonoBehaviourPun
{
    public GameObject objectName; // Name of the object using the PaintballInfo script
    

    public static Hashtable paintballInfo = new Hashtable()
    {
        { "playerName",      NameGenerator.UserName},
        { "paintballDamage", CharacterInfo.info[CharacterMenu.currentModelIndex]["bulletDamage"]}
    };

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
                photonView.RPC("UpdateTable", RpcTarget.All, 10, "hello");
            }
            GameObject splatter = PhotonNetwork.Instantiate("Splatter", objectName.transform.position, Quaternion.Euler(-90, 0, 0));
            Renderer rend = splatter.GetComponent<Renderer>();
            rend.material.color = PlayerMovement.bulletColor;
            if(collisionInfo.collider.tag != "Player")
                PhotonNetwork.Destroy(objectName);
        }
    }
}