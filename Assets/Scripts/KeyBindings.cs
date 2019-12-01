/************************************************************************/
/* Author:  */
/* Date Created: */
/* Last Modified Date: */
/* Modified By: */
/************************************************************************/

using UnityEngine;
using UnityEngine.UI;

public class KeyBindings : MonoBehaviour
{
    public static string backwardKey = "S";
    public static int clickIndex = 0;
    public static string forwardKey = "W";
    public static string leftKey = "A";
    public static string rightKey = "D";
    public Text backwardButton;
    public Text fireButton;
    public Text forwardButton;
    public Text leftButton;
    public bool lookingForKey = false;
    public Text rightButton;
    private string keyHit;
    private string objectName;
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

        if (e.isMouse && lookingForKey)
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

    public void OnMouseClick(Text text)
    {
        text.text = "Click Mouse";
        lookingForKey = true;
        objectName = text.name;
    }

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
}