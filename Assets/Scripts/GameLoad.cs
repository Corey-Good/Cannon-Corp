/************************************************************************/
/* Author:  */
/* Date Created: */
/* Last Modified Date: */
/* Modified By: */
/************************************************************************/

using Photon.Pun;
using UnityEngine.SceneManagement;

public class GameLoad : MonoBehaviourPun
{
    static public bool isXInverted = false;
    static public bool isYInverted = false;

    public void FlipXAxis()
    {
        isXInverted = (isXInverted) ? false : true;
    }

    public void FlipYAxis()
    {
        isYInverted = (isYInverted) ? false : true;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void PracticeMode()
    {
        SceneManager.LoadScene(2);
    }

    // Start is called before the first frame update
    public void StartGame()
    {
        SceneManager.LoadScene(4);
    }
}