using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioClip expolosion;
    public AudioClip background;
    public AudioClip portal;
    public AudioClip levelComplete;
    public AudioClip powerUp;
    public AudioClip monster;
    public AudioClip fail;

    public static AudioPlayer Instance { get; private set; }


    private void Awake()
    {
        Instance = this;
        
    }

    private void Start()
    {
        PlaySound(ESoundType.background);
    }

    public void StopAllSounds()
    {
        var sources = GetComponents<AudioSource>();
        foreach (var item in sources)
        {
            Destroy(item);
        }
    }

    public void PlaySound(ESoundType sound)
    {
        if (sound == ESoundType.explosion)
        {
            var a = gameObject.AddComponent<AudioSource>();
            a.clip = expolosion;
            a.Play();
            Destroy(a, 0.5f);
        }
        else if (sound == ESoundType.background)
        {
            var a = gameObject.AddComponent<AudioSource>();
            a.clip = background;
            a.loop = true;
            a.Play();
        }
        else if (sound == ESoundType.portal)
        {
            var a = gameObject.AddComponent<AudioSource>();
            a.clip = portal;
            a.Play();
            Destroy(a, 0.5f);
        }
        else if (sound == ESoundType.levelComplete)
        {
            var a = gameObject.AddComponent<AudioSource>();
            a.clip = levelComplete;
            a.PlayDelayed(0.5f);
            Destroy(a, 1.5f);
        }
        else if (sound == ESoundType.powerUp)
        {
            var a = gameObject.AddComponent<AudioSource>();
            a.clip = powerUp;
            a.Play();
            Destroy(a, 0.5f);
        }
        else if (sound == ESoundType.monster)
        {
            var a = gameObject.AddComponent<AudioSource>();
            a.clip = monster;
            a.Play();
            Destroy(a, 0.5f);
        }
        else if (sound == ESoundType.fail)
        {
            var a = gameObject.AddComponent<AudioSource>();
            a.clip = fail;
            a.Play();
            Destroy(a, 0.5f);
        }
    }
}
