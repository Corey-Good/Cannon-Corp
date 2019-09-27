using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoad : MonoBehaviour
{
    static public bool isXInverted = false;
    static public bool isYInverted = false;

    // Start is called before the first frame update
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void PracticeMode()
    {
        SceneManager.LoadScene(2);
    }

    public void QuitGame()
    {
        SceneManager.LoadScene(0);
    }

    public void FlipXAxis()
    {
        isXInverted = (isXInverted) ? false : true;
    }

    public void FlipYAxis()
    {
        isYInverted = (isYInverted) ? false : true;
    }
}
