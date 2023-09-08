using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [Header("Source")]
    [SerializeField] private AudioSource _soundSource;

    [Header("Sound")]
    [SerializeField] private AudioClip _explosion;

    private static AudioController instance;

    public static AudioController GetInstance() => instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayExplosionSound()
    {
        _soundSource.PlayOneShot(_explosion);
    }
}
