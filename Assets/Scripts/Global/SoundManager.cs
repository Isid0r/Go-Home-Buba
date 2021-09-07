using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    private AudioClip _deathSound, _jumpSound, _foodSound;
    private AudioSource _as;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        _deathSound = Resources.Load<AudioClip>("death");
        _jumpSound = Resources.Load<AudioClip>("jump");
        _foodSound = Resources.Load<AudioClip>("food");
        _as = GetComponent<AudioSource>();
    }

    public static void PlaySound(string clip)
    {

        switch (clip)
        {
            case "death":
                instance._as.PlayOneShot(instance._deathSound);
                break;
            case "jump":
                instance._as.PlayOneShot(instance._jumpSound);
                break;
            case "food":
                instance._as.PlayOneShot(instance._foodSound);
                break;
            default:
                Debug.LogError($"SoundManager.Неправильный параметр на вход: {clip}");
                break;
        }

    }
}
