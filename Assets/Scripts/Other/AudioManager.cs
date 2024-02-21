using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioClip eat, pop, click, fail, win, clickMenu, coinCollection;
    public AudioSource aus;
    public AudioSource backGroundMusic;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);

    }
    private void Start()
    {
        if (PlayerPrefs.HasKey("isSound"))
        {
            PlayerPrefs.GetInt("isSound");
        }
        else
        {
            PlayerPrefs.SetInt("isSound", 1);
            PlayerPrefs.Save();
        }

        if (PlayerPrefs.HasKey("isMusic"))
        {
            PlayerPrefs.GetInt("isMusic");
        }
        else
        {
            PlayerPrefs.SetInt("isMusic", 1);
            PlayerPrefs.Save();
        }
        BackgroundMusic();

    }

    public void UpdateSoundAndMusic(AudioSource audioSource, AudioClip audioClip)
    {
        int isSound = PlayerPrefs.GetInt("isSound");
        audioSource.PlayOneShot(audioClip);

        audioSource.volume = isSound == 1 ? 1 : 0;
        if (audioClip == pop && isSound == 1) audioSource.volume = 0.1f;
    }
    public void BackgroundMusic()
    {
        int isMusic = PlayerPrefs.GetInt("isMusic");

        if (isMusic == 0)
        {
            backGroundMusic.volume = 0;
        }
        else
        {
            backGroundMusic.volume = 0.25f;
        }

    }
}
