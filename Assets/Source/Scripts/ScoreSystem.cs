using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
        scoreText.text = "счёт: " + SaveExtension.player.score.ToString();
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
