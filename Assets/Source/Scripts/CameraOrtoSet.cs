using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class CameraOrtoSet : MonoBehaviour
{
    public AnimationCurve curve;
    private CinemachineVirtualCamera vCam;

    public bool active;

    private void Start()
    {
        vCam = GetComponent<CinemachineVirtualCamera>();
        SaveExtension.game.OnYandexSDKInitialized += OnYandexSDKInitialized;
    }
    private void OnYandexSDKInitialized()
    {
        if (YandexGame.EnvironmentData.isDesktop)
        {
            active = true;
        }
    }

    private void Update()
    {
        if (active)
        {
            float l = curve.Evaluate(Screen.width);
            Debug.Log(l);
            vCam.m_Lens.OrthographicSize = l;
        }
    }

    private void OnDestroy()
    {
        SaveExtension.game.OnYandexSDKInitialized -= OnYandexSDKInitialized;
    }
}
