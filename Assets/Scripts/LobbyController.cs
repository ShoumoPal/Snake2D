using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyController : MonoBehaviour
{
    [SerializeField]
    private Button player1Button;
    [SerializeField]
    private Button player2Button;

    private void Awake()
    {
        player1Button.onClick.AddListener(PlaySinglePlayer);
        player2Button.onClick.AddListener(PlayDoublePlayer);
        SoundManager.Instance.PlayBG(SoundManager.Sounds.LobbyMusic);
    }

    private void PlaySinglePlayer()
    {
        SceneManager.LoadScene(1);
    }

    private void PlayDoublePlayer()
    {
        SceneManager.LoadScene(2);
    }
    public void PlayHoverSound()
    {
        SoundManager.Instance.Play(SoundManager.Sounds.ButtonHover);
    }

    public void PlayButtonPressSound()
    {
        SoundManager.Instance.Play(SoundManager.Sounds.ButtonPressed);
    }
}
