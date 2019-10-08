using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBindings : MonoBehaviour
{
    public static string forwardKey = "W";
    public static string backwardKey = "S";
    public static string leftKey = "A";
    public static string rightKey = "D";
    public Text forwardButton;
    public Text backwardButton;
    public Text leftButton;
    public Text rightButton;
    private string keyHit;
    public bool lookingForKey = false;
    private string objectName;

    private void Start()
    {
        forwardButton.text = forwardKey;
        backwardButton.text = backwardKey;
        leftButton.text = leftKey;
        rightButton.text = rightKey;
    }

    public void OnClick(Text text)
    {
        text.text = "Hit Key";
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
    }

}
