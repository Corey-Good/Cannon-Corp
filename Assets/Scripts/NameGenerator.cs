/************************************************************************/
/* Author:  */
/* Date Created: */
/* Last Modified Date: */
/* Modified By: */
/************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameGenerator : MonoBehaviour
{
    static public string UserName;
    public Text PlayerName;

    #region list of names
    string[] 
        nameAdj = new string[] 
        {
            "Great ", "Sneaky ", "Tenacious ", "Notorious ", "Slippery ", "Bodacious ", "THE ", "The Only ", "Ambitious ", "Courageous ", "Gregarious ", "Practical ", "Witty ", ""
        },
        nameTitle = new string[] 
        {
            "Sir ", "Knight ", "Honourable ", "Admiral ", "Master ", "Private ", "Specialist ", "Corporal ", "Sergeant ", "Major ", "Captain ", "" 
        },
        nameBody = new string[] 
        {
            "Lee", "Grant", "Sherman", "Abrams", "Bradley", "Patton", "Chaffee", "Jackson", "Pershing" 
        };
    List<string> allNames = new List<string>();
    #endregion list of names

    private void Awake()
    {
        GenerateAllNames();
    }

    public void randomName()
    {       

        var random_number = new System.Random();
        string random_name =  allNames[random_number.Next(0, allNames.Count)];
        allNames.Remove(random_name);

        PlayerName.text = random_name;
        UserName = PlayerName.text;
    }

    public void GenerateAllNames()
    {
        for (int i = 0; i <= nameAdj.Length - 1; i++)
        {
            for (int j = 0; j <= nameTitle.Length - 1; j++)
            {
                for (int k = 0; k <= nameBody.Length - 1; k++)
                {
                    allNames.Add(nameAdj[i] + nameTitle[j] + nameBody[k]);
                }
            }
        }
    }
}