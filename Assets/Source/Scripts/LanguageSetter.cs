using UnityEngine;
using YG;

public class LanguageSetter : MonoBehaviour
{
    private void Start()
    {
        SaveExtension.game.OnYandexSDKInitialized += SDKInit;
    }

    private void SDKInit()
    {
        Debug.Log(YandexGame.EnvironmentData);
        if (YandexGame.EnvironmentData.language == "ru")
        {
            YandexGame.SwitchLanguage("ru");
            SaveExtension.game.ruButton.transform.GetChild(0).gameObject.SetActive(true);
            SaveExtension.game.enButton.transform.GetChild(0).gameObject.SetActive(false);
            SaveExtension.game.trButton.transform.GetChild(0).gameObject.SetActive(false);
        }
        else if (YandexGame.EnvironmentData.language == "en")
        {
            YandexGame.SwitchLanguage("en");
            SaveExtension.game.ruButton.transform.GetChild(0).gameObject.SetActive(false);
            SaveExtension.game.enButton.transform.GetChild(0).gameObject.SetActive(true);
            SaveExtension.game.trButton.transform.GetChild(0).gameObject.SetActive(false);
        }
        else if(YandexGame.EnvironmentData.language == "tr")
        {
            YandexGame.SwitchLanguage("tr");
            SaveExtension.game.ruButton.transform.GetChild(0).gameObject.SetActive(false);
            SaveExtension.game.enButton.transform.GetChild(0).gameObject.SetActive(false);
            SaveExtension.game.trButton.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            YandexGame.SwitchLanguage("en");
            SaveExtension.game.ruButton.transform.GetChild(0).gameObject.SetActive(false);
            SaveExtension.game.enButton.transform.GetChild(0).gameObject.SetActive(true);
            SaveExtension.game.trButton.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        SaveExtension.game.OnYandexSDKInitialized -= SDKInit;
    }
}
