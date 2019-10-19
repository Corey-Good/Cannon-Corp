﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeScreenModel : MonoBehaviour
{
    public List<GameObject> allModels = new List<GameObject>(5);
    static public int currentModelIndex = 0;

    public void Awake()
    {
        currentModelIndex = 0;
        allModels[0].SetActive(true);
        allModels[1].SetActive(false);
        allModels[2].SetActive(false);
        allModels[3].SetActive(false);
        allModels[4].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        allModels[0].SetActive(false);
        allModels[1].SetActive(false);
        allModels[2].SetActive(false);
        allModels[3].SetActive(false);
        allModels[4].SetActive(false);

        allModels[CharacterMenu.currentModelIndex].SetActive(true);
    }
}