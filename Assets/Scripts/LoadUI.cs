using Photon.Pun;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadUI : MonoBehaviour
{
    public Text playerName;
    public Text playerScore;
    public Slider healthBar;
    public Slider reloadBar;

    public static float score;
    public static float totalHealth = (float)CharacterInfo.info[CharacterMenu.currentModelIndex]["healthPoints"];
    public static float currentHealth;

    public GameObject gameOverPanel;
    private bool isGameOver = false;
    private Image panel;
    private float alpha = 0.0f;

    public Texture2D cursorImage;
    public GameObject dangerPanel;

    private void Awake()
    {
        // Set the player's health to full on load
        currentHealth = totalHealth;
        healthBar.value = currentHealth / totalHealth;

        Cursor.SetCursor(cursorImage, new Vector2(0, 0), CursorMode.Auto);

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

    public void FixedUpdate()
    {
        // Currently, the score is based on the amount of time alive
        //score += Time.deltaTime;
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

    private IEnumerator DisconnectAndLoad()
    {
        PhotonNetwork.LeaveRoom();
        while (PhotonNetwork.InRoom)
            yield return null;
        SceneManager.LoadScene(0);
    }
}