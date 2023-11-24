using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class TRButton : MonoBehaviour
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
        SaveExtension.game.trButton = this;
    }

    private void OnButtonPressed()
    {
        YandexGame.SwitchLanguage("tr");
        SaveExtension.game.ruButton.transform.GetChild(0).gameObject.SetActive(false);
        SaveExtension.game.enButton.transform.GetChild(0).gameObject.SetActive(false);
        SaveExtension.game.trButton.transform.GetChild(0).gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }
}
