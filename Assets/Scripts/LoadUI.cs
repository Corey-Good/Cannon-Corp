using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadUI : MonoBehaviour
{
    // Start is called before the first frame update
    public Text playerName;
    public Slider healthBar;
    public Text playerScore;
    public Slider reloadBar;
    private float score;
    private float totalHealth = (float)CharacterInfo.info[CharacterMenu.currentModelIndex]["healthPoints"];
    public static float currentHealth;
    void Awake()
    {
        currentHealth = (float)CharacterInfo.info[CharacterMenu.currentModelIndex]["healthPoints"];
        playerName.text = NameGenerator.UserName;
        healthBar.value = currentHealth / totalHealth;

    }

    public void FixedUpdate()
    {
        
        score += Time.deltaTime;
        playerScore.text = score.ToString("0");
        reloadBar.value = PaintballLauncher.reloadProgress;
        healthBar.value = currentHealth / totalHealth;

        if(currentHealth <= 0.0f)
        {
            SceneManager.LoadScene(3);
        }
    }


}
