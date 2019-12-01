/************************************************************************/
/* Author:  */
/* Date Created: */
/* Last Modified Date: */
/* Modified By: */
/************************************************************************/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameGenerator : MonoBehaviour
{
    static public string UserName;
    public Text PlayerName;

    private List<string> allNames = new List<string> { "Oswald_13",
        "Dr_Radkins",
        "Panda_King",
        "MinecraftBob1",
        "Big Kahuna",
        "Phantom Mystery",
        "Whispering Life",
        "Ships of Justice",
        "Demons of Blasphemy",
        "Mystery and Tombs",
        "Clans and History",
        "Crystalpiece",
        "Evoville",
        "Dataheart",
        "Farrealm",
        "Storm Siren",
        "Bullet Crossing",
        "Maze and Force",
        "Valor and Misery",
        "Crossborne",
        "Bladespace",
        "Maze and Force",
        "Alphastar",
        "Attack of Logic"};

    public void randomName()
    {
        int randomNumber = Random.Range(0, 23);
        PlayerName.text = allNames[randomNumber];
        UserName = PlayerName.text;
    }
}