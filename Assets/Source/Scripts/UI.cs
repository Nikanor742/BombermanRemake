using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class UI : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button helpButton;
    [SerializeField] private Button leaderButton;

    private GameWindow helpWindow;
    private GameWindow leaderWindow;

    private void Awake()
    {
        helpWindow = FindObjectOfType<HelpWindow>();
        leaderWindow = FindObjectOfType<LeaderWindow>();
        startButton.onClick.AddListener(StartGame);
        helpButton.onClick.AddListener(ShowHelp);
        leaderButton.onClick.AddListener(ShowLeader);
    }
    private void Start()
    {
        SaveExtension.game.OnYandexSDKInitialized += OnSDKInit;
    }
    private void OnSDKInit()
    {
        if (SaveExtension.player.firstStart)
        {
            SaveExtension.player.firstStart = false;
            SaveExtension.Save();
        }
    }

    private void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    private void ShowHelp()
    {
        helpWindow.ShowWindow();
    }
    private void ShowLeader()
    {
        leaderWindow.ShowWindow();
    }

    private void OnDestroy()
    {
        startButton.onClick.RemoveAllListeners();
        helpButton.onClick.RemoveAllListeners();
        leaderButton.onClick.RemoveAllListeners();
        SaveExtension.game.OnYandexSDKInitialized -= OnSDKInit;
    }
}
