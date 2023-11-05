using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private ScoreConfig scoreConfig;

    public static ScoreSystem Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SetScoreText();
    }

    private void SetScoreText()
    {
        string text = "";
        if(SaveExtension.player.language == ELanguages.RU)
        {
            text = "счёт: ";
        }
        else if(SaveExtension.player.language == ELanguages.EN)
        {
            text = "score: ";
        }
        else if(SaveExtension.player.language == ELanguages.TR)
        {
            text = "kontrol etmek: "; 
        }
        scoreText.text = text + SaveExtension.player.score.ToString();
    }

    public void AddScore(EScoreType scoreType)
    {
        foreach (var s in scoreConfig.scoreSettings)
        {
            if (s.scoreType == scoreType)
            {
                SaveExtension.player.score += s.addScore;
                SaveExtension.Save();
                SetScoreText();
                break;
            }
        }
    }
}
