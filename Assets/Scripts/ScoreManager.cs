using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private SnakeController snakeController;
    [SerializeField]
    PauseMenuController pauseMenuController;

    private int score = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenuController.ShowPauseMenu();
        }
    }
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
        if(snakeController.player == Player.Player1)
            scoreText.text = "Player 1\n\nScore: " + score;
        else
            scoreText.text = "Player 2\n\nScore: " + score;
    }
    public void RemoveScore(int _score)
    {
        score -= _score;
        if (snakeController.player == Player.Player1)
            scoreText.text = "Player 1\n\nScore: " + score;
        else
            scoreText.text = "Player 2\n\nScore: " + score;
    }
}
