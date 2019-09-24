﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMenu : MonoBehaviour
{

    public List<GameObject> allModels = new List<GameObject>(3);
    static public int currentModelIndex = 0;

    public void Awake()
    {
        currentModelIndex = 0;
        allModels[0].SetActive(true);
        allModels[1].SetActive(false);
        allModels[2].SetActive(false);

    }
    public void ChangeCharacterRight()
    {
        if(currentModelIndex + 1 < allModels.Count)
        {
            allModels[currentModelIndex++].SetActive(false);
            allModels[currentModelIndex].SetActive(true);
        }
    }

    public void ChangeCharacterLeft()
    {
        if (currentModelIndex - 1 > -1)
        {
            allModels[currentModelIndex--].SetActive(false);
            allModels[currentModelIndex].SetActive(true);
        }
    }


}

