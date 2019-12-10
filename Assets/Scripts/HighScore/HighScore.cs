using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class HighScoreEntry
{
    public float score;
    public string name;
}
public class HighScore : MonoBehaviour
{
    public Transform scoreGrid;
    public Transform entryTemplate;
    private string currentPlayerName;
    private string currentPlayerScore;
    public Text playerName;
    public Text playerScore;
    private List<HighScoreEntry> highScoreEntryList;
 
    [PunRPC]
    public static void UpdateTable(string newName, float newScore)
    {
        HighScoreEntry newHighScoreEntry = new HighScoreEntry {name = newName, score = newScore};
        Debug.Log(newName + " " + newScore);
        Debug.Log("UpdateTable has been called");

        CreateHighScoreEntry(newHighScoreEntry, entryTemplate, scoreGrid);
    }

    private void Awake()
    {
        scoreGrid = transform.Find("ScoreGrid");
        entryTemplate = scoreGrid.Find("highScoreEntryTemplate");
        entryTemplate.gameObject.SetActive(false);
    }

    private static void CreateHighScoreEntry(HighScoreEntry highScoreEntry, Transform entryTemplate, Transform scoreGrid)
    {
        Debug.Log("CreateHighScoreEntry has been called");
        Transform entryTransform = Instantiate(entryTemplate, scoreGrid);
        Debug.Log("inside CreateHighScoreEntry" + highScoreEntry.name + " " + highScoreEntry.score);
        entryTransform.gameObject.SetActive(true);
        entryTransform.Find("Name").GetComponent<Text>().text = /*highScoreEntry.name*/ "test";
        entryTransform.Find("Score").GetComponent<Text>().text = /*highScoreEntry.score.ToString()*/ "20";
    }
}
