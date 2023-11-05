using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button helpButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button leaderButton;

    private GameWindow helpWindow;
    private GameWindow settingsWindow;
    private GameWindow leaderWindow;

    private void Awake()
    {
        helpWindow = FindObjectOfType<HelpWindow>();
        startButton.onClick.AddListener(StartGame);
        helpButton.onClick.AddListener(ShowHelp);
        settingsButton.onClick.AddListener(ShowSettings);
        leaderButton.onClick.AddListener(ShowLeader);
    }
    private void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    private void ShowHelp()
    {
        helpWindow.ShowWindow();
    }
    private void ShowSettings()
    {
        settingsWindow.ShowWindow();
    }
    private void ShowLeader()
    {
        leaderWindow.ShowWindow();
    }

    private void OnDestroy()
    {
        startButton.onClick.RemoveAllListeners();
        helpButton.onClick.RemoveAllListeners();
        settingsButton.onClick.RemoveAllListeners();
        leaderButton.onClick.RemoveAllListeners();
    }
}
