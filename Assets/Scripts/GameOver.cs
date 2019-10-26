
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GameOver : MonoBehaviour
{
    private float timeElapsed = 0.0f;
    private float waitTime = 3.0f;

    public void GameOverScreen()
    {
        // Give a short delay before loading the Main Menu
        timeElapsed += Time.deltaTime;
        if (timeElapsed > waitTime)
        {
            Cursor.visible = true;
            PhotonNetwork.LoadLevel(0);
            
        }
    }
}
