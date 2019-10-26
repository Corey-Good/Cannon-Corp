﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using Photon.Pun;

public class LoadUI : MonoBehaviour
{
    public Text   playerName;
    public Text   playerScore;
    public Slider healthBar;
    public Slider reloadBar;

    public static float score;
    public static       float totalHealth = (float)CharacterInfo.info[CharacterMenu.currentModelIndex]["healthPoints"];
    public static float currentHealth;

    void Awake()
    {
        // Set the player's health to full on load
        currentHealth = totalHealth;
        healthBar.value = currentHealth / totalHealth;

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

    public void FixedUpdate()
    {
        // Currently, the score is based on the amount of time alive
        score += Time.deltaTime;
        playerScore.text = score.ToString("0"); // 0 converts the float to a string with no decimal value

        // Update the player health bar and reload bars
        reloadBar.value = PaintballLauncher.reloadProgress;
        healthBar.value = currentHealth / totalHealth;

        // Send the player to the GameOver screen when killed
        if(currentHealth <= 0.0f)
        {
            //StartCoroutine(DisconnectAndLoad());
            GameOver.GameOverScreen();

        }
    }


    //IEnumerator DisconnectAndLoad()
    //{
    //    PhotonNetwork.LeaveRoom();
    //    while (PhotonNetwork.InRoom)
    //        yield return null;
    //    SceneManager.LoadScene(3);

    //}
}
