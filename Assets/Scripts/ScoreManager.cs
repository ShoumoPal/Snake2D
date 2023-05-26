using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private SnakeController snakeController;

    private int score = 0;

    public void AddScore(int _score)
    {
        if (snakeController.hasScoreBuff)
        {
            score += _score * 2;
        }
        else
        {
            score += _score;
        }
        scoreText.text = "Score: " + score;
    }
    public void RemoveScore(int _score)
    {
        score -= _score;
        scoreText.text = "Score: " + score;
    }
}
