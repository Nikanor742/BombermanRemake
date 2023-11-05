using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    [SerializeField] private Sprite soundOn;
    [SerializeField] private Sprite soundOff;
    private Button button;
    private AudioSource audio;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonPressed);
        SetButtonState();
    }

    private void SetButtonState()
    {
        if (SaveExtension.player.sound)
        {
            button.image.sprite = soundOff;
        }
        else
        {
            button.image.sprite = soundOn;
        }
    }

    private void OnButtonPressed()
    {
        SaveExtension.player.sound = !SaveExtension.player.sound;
        SaveExtension.Save();
        SetButtonState();
        if (SaveExtension.player.sound)
        {
            audio.Play();
        }
    }

    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }
}
