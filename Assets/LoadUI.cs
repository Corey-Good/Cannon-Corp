﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadUI : MonoBehaviour
{
    // Start is called before the first frame update
    public Text playerName;
    void Start()
    {
        playerName.text = NameGenerator.UserName;
    }


}
