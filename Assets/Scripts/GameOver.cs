
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private float timeElapsed = 0.0f;
    private float waitTime = 5.0f;
    public void FixedUpdate()
    {


        timeElapsed += Time.deltaTime;

        if (timeElapsed > waitTime)
        { 
            SceneManager.LoadScene(0);
        }
    }
}
