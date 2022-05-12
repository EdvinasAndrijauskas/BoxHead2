using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighscoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> highscoreEntryTransformList;


    private void Awake()
    {
        entryContainer = transform.Find("highscoreEntryContainer");
        entryTemplate = entryContainer.Find("highscoreEntryTemplate");

        entryTemplate.gameObject.SetActive(false);

        AddHighscoreEntry(Score.points);
        var jsonString = PlayerPrefs.GetString("highscoreTable");
        var highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if (highscores != null)
        {
            // Sort entry list by Score and keep max 12 highscores
            highscores.highscoreEntryList.Sort((x, y) => y.score.CompareTo(x.score));

            highscoreEntryTransformList = new List<Transform>();
            foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList)
            {
                CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
            }
        }
    }

    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container,
        List<Transform> transformList)
    {
        float templateHeight = 31f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        switch (rank)
        {
            default:
                rankString = rank + "TH";
                break;

            case 1:
                rankString = "1ST";
                break;
            case 2:
                rankString = "2ND";
                break;
            case 3:
                rankString = "3RD";
                break;
        }

        entryTransform.Find("posText").GetComponent<Text>().text = rankString;

        int score = highscoreEntry.score;

        entryTransform.Find("scoreText").GetComponent<Text>().text = score.ToString();

        // Set background visible odds and evens, easier to read
        entryTransform.Find("background").gameObject.SetActive(rank % 2 == 1);

        transformList.Add(entryTransform);
    }

    public void AddHighscoreEntry(int score)
    {
        // Create HighscoreEntry
        HighscoreEntry highscoreEntry = new HighscoreEntry {score = score};

        // Load saved Highscores
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if (highscores == null)
        {
            // There's no stored table, initialize
            highscores = new Highscores()
            {
                highscoreEntryList = new List<HighscoreEntry>()
            };
        }

        // Add new entry to Highscores
        highscores.highscoreEntryList.Add(highscoreEntry);

        // Sort entry list by Score
        SortList(highscores.highscoreEntryList);

        // Save updated Highscores
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }

    public void Back()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 4);
    }

    private void SortList(List<HighscoreEntry> highscores)
    {
        highscores.Sort((x, y) => y.score.CompareTo(x.score));
        if (highscores.Count > 12)
        {
            for (int h = highscores.Count; h > 12; h--)
            {
                highscores.RemoveAt(12);
            }
        }
    }

    [Serializable]
    private class HighscoreEntry
    {
        public int score;
    }

    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }
}