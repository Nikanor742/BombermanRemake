using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class RUButton : MonoBehaviour
{
    private Button button;
    private void Start()
    {
        Init();
    }
    public void Init()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonPressed);
        SaveExtension.game.ruButton = this;
    }

    private void OnButtonPressed()
    {
        YandexGame.SwitchLanguage("ru");
        SaveExtension.game.enButton.transform.GetChild(0).gameObject.SetActive(false);
        SaveExtension.game.trButton.transform.GetChild(0).gameObject.SetActive(false);
        SaveExtension.game.ruButton.transform.GetChild(0).gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }
}
