/************************************************************************/
/* Author:  */
/* Date Created: */
/* Last Modified Date: */
/* Modified By: */
/************************************************************************/

using Photon.Pun;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadUI : MonoBehaviour
{
    public static float currentHealth;
    public static float score;
    public static float totalHealth = (float)CharacterInfo.info[CharacterMenu.currentModelIndex]["healthPoints"];
    public Texture2D cursorImage;
    public GameObject dangerPanel;
    public GameObject gameOverPanel;
    public Slider healthBar;
    public Text playerName;
    public Text playerScore;
    public Slider reloadBar;
    public Text timer;
    private float alpha = 0.0f;
    public  bool isGameOver = false;
    private int minute;
    private Image panel;
    private int second;
    private float stopwatchTime = 0.0f;
    private float time = 360.0f;

    public void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().name == "SM")
        {
            UpdateTimer();
        }
        else if (SceneManager.GetActiveScene().name == "Obstacle")
        {
            stopwatchTime += Time.deltaTime;
            timer.text = stopwatchTime.ToString("0.00s");
        }

        playerScore.text = score.ToString("0"); // 0 converts the float to a string with no decimal value

        // Update the player health bar and reload bars
        reloadBar.value = PlayerMovement.reloadProgress;
        healthBar.value = currentHealth / totalHealth;

        // Send the player to the GameOver screen when killed
        if (currentHealth <= 0.0f)
        {
            if (SceneManager.GetActiveScene().name == "FFA")
                isGameOver = true;
        }

        if (DangerZone.isInDanger)
        {
            currentHealth -= 0.05f;
        }

        if (isGameOver)
        {
            LoadGameOver();
            isGameOver = false;
        }
    }

    public void LoadGameOver()
    {
        gameOverPanel.SetActive(true);
        dangerPanel.SetActive(false);
        panel = gameOverPanel.GetComponent<Image>();
        panel.color = new Color(0.0f, 0.0f, 0.0f, alpha);
        alpha += 0.007f;
        if (alpha > 1.9f)
        {
            Cursor.visible = true;
            StartCoroutine(DisconnectAndLoad());
        }
    }

    public void UpdateTimer()
    {
        if (time <= 0.0f)
        {
            StartCoroutine(DisconnectAndLoad());
        }
        time -= Time.deltaTime;
        second = (int)(time % 60.0f);
        minute = (int)(time / 60.0f);
        timer.text = "";
        timer.text = minute.ToString() + ":" + second.ToString("00");
        // Note for next semester:
        // SM will have a lobby, once the lobby has enough players then the game will start
        // Have the master client start the timer over the network and have evryone else get the value from the master client
        // that way the round will end at the same time for everyone
    }

    private void Awake()
    {
        // Set the player's health to full on load
        currentHealth = totalHealth;
        healthBar.value = currentHealth / totalHealth;

        Cursor.SetCursor(cursorImage, new Vector2(cursorImage.width / 2.0f, cursorImage.height / 2.0f), CursorMode.Auto);

        score = 0.0f;

        // Give the user a deafult name if no name was chosen
        if (NameGenerator.UserName == null)
        {
            playerName.text = "NoNameNoWin";
        }
        else
        {
            playerName.text = NameGenerator.UserName;
        }
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