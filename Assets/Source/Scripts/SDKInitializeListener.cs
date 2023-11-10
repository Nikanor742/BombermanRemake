using System.Collections;
using UnityEngine;
using YG;

public class SDKInitializeListener : MonoBehaviour
{
    private IEnumerator CheckInit()
    {
        yield return new WaitForSeconds(0.1f);
        if (YandexGame.SDKEnabled)
        {
            SaveExtension.game.InvokeOnYandexSDKInitialized();
        }
        else
        {
            StartCoroutine(CheckInit());
        }
    }

    private void Start()
    {
        StartCoroutine(CheckInit());
    }
}
