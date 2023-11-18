using UnityEngine;
using YG;

public class LabelSystem : MonoBehaviour
{
    private void Start()
    {
        SaveExtension.game.OnYandexSDKInitialized += OnSDKInit;
    }
    private void OnSDKInit()
    {
        if (YandexGame.EnvironmentData.promptCanShow && !YandexGame.savesData.promptDone)
        {
            YandexGame.PromptShow();
        }
    }

    private void OnDestroy()
    {
        SaveExtension.game.OnYandexSDKInitialized -= OnSDKInit;
    }
}
