using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PaintballInfo : MonoBehaviour
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
}
