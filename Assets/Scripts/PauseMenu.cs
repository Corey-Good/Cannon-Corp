using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject settingsUI;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
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
        GameIsPaused = false;

        pauseMenuUI.SetActive(false);
        settingsUI.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Pause()
    {
        GameIsPaused = true;

        pauseMenuUI.SetActive(true);   //Opposite of Settings()
        settingsUI.SetActive(false);  //Opposite of Settings()

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Settings()
    {
        GameIsPaused = true;

        pauseMenuUI.SetActive(false);  //Opposite of Pause()
        settingsUI.SetActive(true);   //Opposite of Pause()

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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

    IEnumerator DisconnectAndLoad()
    {
        PhotonNetwork.LeaveRoom();
        while (PhotonNetwork.InRoom)
            yield return null;
        SceneManager.LoadScene(0);
    }
}