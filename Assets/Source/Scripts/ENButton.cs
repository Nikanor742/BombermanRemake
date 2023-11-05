using UnityEngine;
using UnityEngine.UI;
using YG;

public class ENButton : MonoBehaviour
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
        SaveExtension.game.enButton = this;
    }

    private void OnButtonPressed()
    {
        SaveExtension.player.language = ELanguages.EN;
        SaveExtension.Save();
        YandexGame.SwitchLanguage("en");
        SaveExtension.game.ruButton.transform.GetChild(0).gameObject.SetActive(false);
        SaveExtension.game.trButton.transform.GetChild(0).gameObject.SetActive(false);
        SaveExtension.game.enButton.transform.GetChild(0).gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }
}
