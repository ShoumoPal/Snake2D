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
        SoundManager.Instance.PlayBG(SoundManager.Sounds.LobbyMusic);
    }

    private void PlaySinglePlayer()
    {
        SceneManager.LoadScene(1);
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
