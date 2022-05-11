using System.Linq;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    public RowUI Row;
    
    public ScoreManager ScoreManager;
    // public static int index = 0;
    private int index;
    // Start is called before the first frame update
    void Start()
    {
        SaveValues();
        ScoreManager.getList();
        DisplayRows();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveValues()
    {
        // PlayerPrefs.SetInt("index",0);
        // PlayerPrefs.Save();
        // index = GetIndex();
        PlayerPrefs.SetInt("scores" + index, Score.points);
        PlayerPrefs.Save();
        // index++;



        var shownHighscores = ScoreManager.GetHighScores().ToArray();
        Debug.Log(shownHighscores.Length);
        if (shownHighscores.Length < 10)
        {
            for (int i = 0; i < shownHighscores.Length - 1; i++)
            {
                var row = Instantiate(Row, transform).GetComponent<RowUI>();
                row.rank.text = (i + 1).ToString();
                row.score.text = shownHighscores[i].score.ToString();
            }
        }
        
        for (int i = 0; i <= 10; i++)
        {
            var row = Instantiate(Row, transform).GetComponent<RowUI>();
            row.rank.text = (i + 1).ToString();
            row.score.text = shownHighscores[i].score.ToString();
        }
    }
    
    public void DisplayRows()
    {
        var shownHighscores = ScoreManager.GetHighScores().ToArray();
        Debug.Log(shownHighscores.Length);
        if (shownHighscores.Length < 10)
        {
            
            Debug.Log("WORK BLEA");
            for (int i = 0; i < shownHighscores.Length ; i++)
            {
                Debug.Log("AICI");
                var row = Instantiate(Row, transform).GetComponent<RowUI>();
                row.rank.text = (i + 1).ToString();
                row.score.text = shownHighscores[i].score.ToString();
            }
        }
        else
        {
            for (int i = 0; i <= 10; i++)
            {
                Debug.Log("Here lol u stupid!");
                var row = Instantiate(Row, transform).GetComponent<RowUI>();
                row.rank.text = (i + 1).ToString();
                row.score.text = shownHighscores[i].score.ToString();
            } 
        }
       
    }

    // private int GetIndex()
    // {
    //     return PlayerPrefs.GetInt("index");
    // }
}