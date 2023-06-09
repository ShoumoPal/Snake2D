using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerWinScreenController : MonoBehaviour
{
    [SerializeField]
    private Button playAgainButton;
    [SerializeField]
    private Button mainMenuButton;
    [SerializeField]
    private TextMeshProUGUI playerWinText;

    private void Awake()
    {
        Time.timeScale = 0f;
        playAgainButton.onClick.AddListener(PlayAgain);
        mainMenuButton.onClick.AddListener(MainMenu);
    }
    public void ShowPlayerWinScreen(Player player)
    {
        gameObject.SetActive(true);

        if(player == Player.Player1)
        {
            ChangeText("Player 1 Wins!!");
        }
        else
        {
            ChangeText("Player 2 Wins!!");
        }
    }
    public void ChangeText(string text)
    {
        playerWinText.text = text;
    }
    private void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    private void PlayAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
