
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private float timeElapsed = 0.0f;
    private float waitTime = 3.0f; // 3 seconds
    public void FixedUpdate()
    {
        // Give a short delay before loading the Main Menu
        timeElapsed += Time.deltaTime;
        if (timeElapsed > waitTime)
        { 
            SceneManager.LoadScene(0);
            Cursor.visible = true;
        }
    }
}
