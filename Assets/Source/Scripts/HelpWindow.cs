using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpWindow : GameWindow
{
    [SerializeField] private Button exitButton;
    private void Start()
    {
        rect = GetComponent<RectTransform>();
        showPosition = rect.anchoredPosition;
        rect.anchoredPosition = hidePosition;
        exitButton.onClick.AddListener(HideWindow);
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
