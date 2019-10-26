using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
<<<<<<< HEAD

    private void Awake()
    {
        Cursor.visible = false;
=======
  

    private void Awake()
    {
       Cursor.visible = false;
>>>>>>> ProdWithMenu
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
        Cursor.visible = false;
        pauseMenuUI.SetActive(false);
        GameIsPaused = false;
    }

    public void Pause()
    {
        Cursor.visible = true;
        pauseMenuUI.SetActive(true);
        GameIsPaused = true;
    }

    public void Settings()
    {
        SceneManager.LoadScene("InGameSettings");
    }

    public void Back()
    {
        Pause();
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
    }

    public void QuitGame()
    {
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
