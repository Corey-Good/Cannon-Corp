using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class HighScoreEntry
{
    public int score;
    public string name;
}
public class HighScore : MonoBehaviour
{
    private Transform scoreGrid;
    private Transform entryTemplate;
    private string currentPlayerName;
    private string currentPlayerScore;
    public Text playerName;
    public Text playerScore;
    private List<HighScoreEntry> highScoreEntryList;
    private List<Transform> table;
    


    [PunRPC]
    public void UpdateTable(HighScoreEntry newHighScoreEntry)
    {
        CreateHighScoreEntry(newHighScoreEntry, table);
    }

    private void Awake()
    {
        scoreGrid = transform.Find("ScoreGrid");
        entryTemplate = scoreGrid.Find("highScoreEntryTemplate");

        entryTemplate.gameObject.SetActive(false);
    }

    private void CreateHighScoreEntry(HighScoreEntry highScoreEntry, List<Transform> transformList)
    {
        Transform entryTransform = Instantiate(entryTemplate, scoreGrid);

        entryTransform.gameObject.SetActive(true);

        entryTransform.Find("Name").GetComponent<Text>().text = highScoreEntry.name;
        entryTransform.Find("Score").GetComponent<Text>().text = highScoreEntry.score.ToString();

        transformList.Add(entryTransform);
    }
}
