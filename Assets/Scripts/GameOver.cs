
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GameOver : MonoBehaviour
{

    public static void GameOverScreen()
    {
        // Give a short delay before loading the Main Menu
        timeElapsed += Time.deltaTime;
        if (timeElapsed > waitTime)
        {
            Cursor.visible = true;
            PhotonNetwork.LoadLevel(0);
            ;
        }
    }
}
