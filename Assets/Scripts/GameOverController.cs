using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    [SerializeField]
    private Button restartButton;
    [SerializeField]
    private Button menuButton;


    private void Awake()
    {
        restartButton.onClick.AddListener(RestartLevel);
        menuButton.onClick.AddListener(BackToMenu);
    }

    private void BackToMenu()
    {
        //Go to lobby
        SceneManager.LoadScene(0);
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void ShowGameOverPanel()
    {
        gameObject.SetActive(true);
    }

    public void ButtonPressSound()
    {
        SoundManager.Instance.Play(SoundManager.Sounds.ButtonPressed);
    }

    public void ButtonHoverSound()
    {
        SoundManager.Instance.Play(SoundManager.Sounds.ButtonHover);
    }
}
