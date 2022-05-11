using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static int points;

    [SerializeField] private Text scoreText;
    
    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + points;
    }
}
