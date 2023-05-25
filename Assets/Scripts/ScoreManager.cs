using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;

    private int score = 0;

    public void AddScore(int _score)
    {
        score += _score;
        scoreText.text = "Score: " + score;
    }
    public void RemoveScore(int _score)
    {
        score -= _score;
        scoreText.text = "Score: " + score;
    }
}
