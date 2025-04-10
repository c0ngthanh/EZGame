using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioClip mainSceneMusic;
    public AudioClip inGameMusic;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        PlayMainSceneMusic();
    }
    public void PlayMainSceneMusic()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = mainSceneMusic;
        audioSource.Play();
    }
    public void PlayInGameMusic()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.clip = inGameMusic;
        audioSource.Play();
    }
}
