using Photon.Pun;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject settingsUI;

    private void Awake()
    {
        //Cursor.visible = false;
    }

    public void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        //Cursor.visible = false;
        pauseMenuUI.SetActive(false);
        GameIsPaused = false;
        settingsUI.SetActive(false);
    }

    public void Pause()
    {
        Cursor.visible = true;
        pauseMenuUI.SetActive(true);   //Opposite of Settings()
        GameIsPaused = true;
        settingsUI.SetActive(false);   //Opposite of Settings()
    }

    public void Settings()
    {
        Cursor.visible = true;
        pauseMenuUI.SetActive(false);  //Opposite of Pause()
        GameIsPaused = true;
        settingsUI.SetActive(true);    //Opposite of Pause()
    }

    public void Back()
    {
        Pause();
    }

    public void QuitGame()
    {
        GameIsPaused = false;
        StartCoroutine(DisconnectAndLoad());
    }

    private IEnumerator DisconnectAndLoad()
    {
        Cursor.SetCursor(null, new Vector2(0, 0), CursorMode.Auto);
        PhotonNetwork.LeaveRoom();
        while (PhotonNetwork.InRoom)
            yield return null;
        SceneManager.LoadScene(0);
    }
}