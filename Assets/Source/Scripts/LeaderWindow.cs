using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class LeaderWindow : GameWindow
{
    [SerializeField] private Button exitButton;
    public LeaderboardYG leaderboard;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
        showPosition = rect.anchoredPosition;
        rect.anchoredPosition = hidePosition;
        exitButton.onClick.AddListener(HideWindow);
    }

    public override void ShowWindow()
    {
        base.ShowWindow();
        leaderboard.UpdateLB();
    }

    private void OnDestroy()
    {
        exitButton.onClick.RemoveAllListeners();
    }

    public override void HideWindow()
    {
        base.HideWindow();
    }
}
