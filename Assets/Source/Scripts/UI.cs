using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

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
        settingsWindow = FindObjectOfType<SettingsWindow>();
        leaderWindow = FindObjectOfType<LeaderWindow>();
        startButton.onClick.AddListener(StartGame);
        helpButton.onClick.AddListener(ShowHelp);
        settingsButton.onClick.AddListener(ShowSettings);
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
            FirstLanguageSetup();
            SaveExtension.player.firstStart = false;
            SaveExtension.Save();
        }
        else
        {
            SetLanguage();
        }
    }
    private void FirstLanguageSetup()
    {
        if (YandexGame.EnvironmentData.language == "ru")
        {
            SaveExtension.player.language = ELanguages.RU;
        }
        else if (YandexGame.EnvironmentData.language == "en")
        {
            SaveExtension.player.language = ELanguages.EN;
        }
        else if (YandexGame.EnvironmentData.language == "tr")
        {
            SaveExtension.player.language = ELanguages.TR;
        }
        else
        {
            if (YandexGame.EnvironmentData.domain == "be" ||
                YandexGame.EnvironmentData.domain == "kk" ||
                YandexGame.EnvironmentData.domain == "uk" ||
                YandexGame.EnvironmentData.domain == "uz")
            {
                SaveExtension.player.language = ELanguages.RU;
            }
            else
            {
                SaveExtension.player.language = ELanguages.EN;
            }
        }
        SetLanguage();
        SaveExtension.Save();
    }

    private void SetLanguage()
    {
        if (SaveExtension.player.language == ELanguages.RU)
        {
            var button = FindObjectOfType<RUButton>();
            button.transform.GetChild(0).gameObject.SetActive(true);
            YandexGame.SwitchLanguage("ru");
        }
        else if(SaveExtension.player.language == ELanguages.EN)
        {
            var button = FindObjectOfType<ENButton>();
            button.transform.GetChild(0).gameObject.SetActive(true);
            YandexGame.SwitchLanguage("en");
        }
        else if (SaveExtension.player.language == ELanguages.TR)
        {
            var button = FindObjectOfType<TRButton>();
            button.transform.GetChild(0).gameObject.SetActive(true);
            YandexGame.SwitchLanguage("tr");
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
        SaveExtension.game.OnYandexSDKInitialized -= OnSDKInit;
    }
}
