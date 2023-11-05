﻿using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        startButton.onClick.AddListener(StartGame);
        helpButton.onClick.AddListener(ShowHelp);
        settingsButton.onClick.AddListener(ShowSettings);
        leaderButton.onClick.AddListener(ShowLeader);

        SaveExtension.Override();

        if (SaveExtension.player.firstStart)
        {
            FirstLanguageSetup();
            SaveExtension.player.firstStart = false;
            SaveExtension.Save();
        }
        SetLanguage();
    }

    private async void FirstLanguageSetup()
    {
        await Task.Delay(10);
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

    private async void SetLanguage()
    {
        await Task.Delay(10);
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
    }
}
