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
            }
            else
            {                
                GameObject splatter = PhotonNetwork.Instantiate("Splatter", objectName.transform.position, Quaternion.Euler(-90, 0, 0));
                Renderer rend = splatter.GetComponent<Renderer>();
                rend.material.color = PlayerMovement.bulletColor;
            }

            for (float i = 0; i < 10; i+= 0.1f)
            {

            }
            //Debug.Log("Bullet destroyed at: " + PhotonNetwork.Time);
            PhotonNetwork.Destroy(objectName);

        }
    }


}