using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // private List<HighScore> scoreData = new List<HighScore>(5);
    // private string key = "";
    private List<HighScore> scoreData;
    // void Start()
    // {
    //     for (int i = 0; i < scoreData.Count; i++)
    //     {
    //         key = "score" + (i + 1);
    //         scoreData[i] = new HighScore(PlayerPrefs.GetInt(key, 0));
    //     }
    // }
    
    void Awake()
    {
        scoreData = new List<HighScore>();
      
    }
    
    public IEnumerable<HighScore> GetHighScores()
    {
        return scoreData.OrderByDescending(x => x.score);
    }
    
    public void getList()
    {
        for (int i = 0; i < PlayerPrefs.GetInt("Index"); i++)
        {
            scoreData.Add(new HighScore(PlayerPrefs.GetInt("scores" + i)));
            Debug.Log(PlayerPrefs.GetInt("scores" + i)  + "OPDAVAIDADAVAI");
        }
    }
}