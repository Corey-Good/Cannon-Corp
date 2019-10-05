using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PauseMenu : MonoBehaviourPunCallbacks
{
    public static bool IsPaused = false;
    public GameObject pauseMenuUI;
    // Update is called once per frame

    private void Awake()
    {

       Cursor.visible = false;

    }

    public void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (IsPaused)
            {
                Cursor.visible = false;
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
        IsPaused = false;
    }

    public void Pause()
    {
        Cursor.visible = true;
        pauseMenuUI.SetActive(true);
        IsPaused = true;
    }

    public void LeaveGame()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel(0);
    }
}
