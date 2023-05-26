using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField]
    private Button resumeButton;
    [SerializeField]
    private Button mainMenuButton;

    private void OnEnable()
    {
        Time.timeScale = 0;
    }
    private void Awake()
    {
        resumeButton.onClick.AddListener(Resume);
        mainMenuButton.onClick.AddListener(MainMenu);
    }

    public void ShowPauseMenu()
    {
        gameObject.SetActive(true);
    }

    private void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    private void Resume()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}
