using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private ScoreDrawComponent scoreDraw;
    [SerializeField] private ScoreConfig scoreConfig;

    public static ScoreSystem Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SaveExtension.game.OnYandexSDKInitialized += OnSDKInit;
        
    }

    private void OnSDKInit()
    {
        SetScoreText();
    }

    private void SetScoreText()
    {
        string text = "";
        if(YandexGame.EnvironmentData.language == "ru")
        {
            text = "счёт: ";
        }
        else if(YandexGame.EnvironmentData.language == "en")
        {
            text = "score: ";
        }
        else if(YandexGame.EnvironmentData.language == "tr")
        {
            text = "kontrol etmek: "; 
        }
        scoreText.text = text + SaveExtension.player.score.ToString();
    }

    public void AddScore(EScoreType scoreType, Vector3 pos)
    {
        foreach (var s in scoreConfig.scoreSettings)
        {
            if (s.scoreType == scoreType)
            {
                float score = s.addScore * (Mathf.Clamp(SaveExtension.player.level, 1, Mathf.Infinity))/2;
                SaveExtension.player.score += (int)score;
                Instantiate(scoreDraw, pos, Quaternion.identity).score.text="+" + ((int)score).ToString();
                SaveExtension.Save();
                SetScoreText();
                break;
            }
        }
    }
}
