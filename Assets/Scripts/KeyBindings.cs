﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBindings : MonoBehaviour
{
    static public bool isXInverted = false;
    static public bool isYInverted = false;

    public static string forwardKey = "W";
    public static string backwardKey = "S";
    public static string leftKey = "A";
    public static string rightKey = "D";
    public static int clickIndex = 0;

    public Text forwardButton;
    public Text backwardButton;
    public Text leftButton;
    public Text rightButton;
    public Text fireButton;

    private string keyHit;
    private string objectName;

    public bool lookingForKey = false;


    private void Start()
    {
        forwardButton.text = forwardKey;
        backwardButton.text = backwardKey;
        leftButton.text = leftKey;
        rightButton.text = rightKey;
        if (clickIndex == 0)
            fireButton.text = "LeftClick";
        else if (clickIndex == 1)
            fireButton.text = "RightClick";
    }

    public void FlipXAxis()
    {
        isXInverted = (isXInverted) ? false : true;
    }

    public void FlipYAxis()
    {
        isYInverted = (isYInverted) ? false : true;
    }

    public void OnClick(Text text)
    {
        text.text = "HIT KEY";
        lookingForKey = true;
        objectName = text.name;           
    }

    public void OnMouseClick(Text text)
    {
        text.text = "Click Mouse";
        lookingForKey = true;
        objectName = text.name;
    }

    public void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey && lookingForKey)
        {
            keyHit = e.keyCode.ToString();
            switch (objectName)
            {
                case "ForwardText":
                    forwardButton.text = keyHit;
                    forwardKey = forwardButton.text;
                    break;
                case "BackwardText":
                    backwardButton.text = keyHit;
                    backwardKey = backwardButton.text;
                    break;
                case "LeftText":
                    leftButton.text = keyHit;
                    leftKey = leftButton.text;
                    break;
                case "RightText":
                    rightButton.text = keyHit;
                    rightKey = rightButton.text;
                    break;
            }
            lookingForKey = false;
        }

        if(e.isMouse && lookingForKey)
        {
            if (Input.GetMouseButtonDown(0))
            {
                clickIndex = 0;
                fireButton.text = "LeftClick";
            }
            else if (Input.GetMouseButtonDown(1))
            {
                clickIndex = 1;
                fireButton.text = "RightClick";
            }
            lookingForKey = false;
        }
    }

}
