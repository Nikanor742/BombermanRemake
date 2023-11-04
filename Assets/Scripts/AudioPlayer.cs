using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioClip expolosion;
    public static AudioPlayer Instance { get; private set; }

    private AudioSource audioSource;

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(ESoundType sound)
    {
        if (sound == ESoundType.explosion)
        {
            audioSource.clip = expolosion;
            audioSource.Play();
        }
    }
}
