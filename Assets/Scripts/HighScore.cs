using System;
using UnityEngine;

[Serializable]
public class HighScore : MonoBehaviour
{
    public int score { get; set; }

    public HighScore(int score)
    {
        this.score = score;
    }
}