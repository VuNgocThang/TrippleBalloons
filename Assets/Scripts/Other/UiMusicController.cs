using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiMusicController : MonoBehaviour
{
    public SwitchToggle toggleMusic;
    public SwitchToggle toggleSound;
    public SwitchToggle toggleVibrate;

    public static UiMusicController ins;
    private void Awake()
    {
        ins = this;
    }
    void Start()
    {
        toggleMusic.toggle.onValueChanged.AddListener(delegate { ToggleValueChanged(toggleMusic); });
        toggleSound.toggle.onValueChanged.AddListener(delegate { ToggleValueChanged(toggleSound); });
        toggleVibrate.toggle.onValueChanged.AddListener(delegate { ToggleValueChanged(toggleVibrate); });
        SaveSoundAndMusic();
        AudioManager.instance.BackgroundMusic();
    }


    public void SaveSoundAndMusic()
    {
        if (PlayerPrefs.GetInt("isSound") == 1)
        {
            OnSoundOn();
        }
        else if (PlayerPrefs.GetInt("isSound") == 0)
        {
            OnSoundOff();
        }


        if (PlayerPrefs.GetInt("isMusic") == 1)
        {
            OnMusicOn();
        }
        else if (PlayerPrefs.GetInt("isMusic") == 0)
        {
            OnMusicOff();
        }
    }


    void ToggleValueChanged(SwitchToggle changedToggle)
    {
        if (changedToggle.toggle.isOn)
        {
            if (changedToggle == toggleMusic)
            {
                OnMusicOn();

            }
            else if (changedToggle == toggleSound)
            {
                OnSoundOn();

            }
            else if (changedToggle == toggleVibrate)
            {
                PlayerPrefs.SetInt("isVibrate", 1);
            }
        }
        else
        {
            if (changedToggle == toggleMusic)
            {
                OnMusicOff();
            }
            else if (changedToggle == toggleSound)
            {
                OnSoundOff();
            }
            else if (changedToggle == toggleVibrate)
            {
                PlayerPrefs.SetInt("isVibrate", 0);
            }
        }
    }

    void OnMusicOn()
    {
        toggleMusic.toggle.isOn = true;
        PlayerPrefs.SetInt("isMusic", 1);
        AudioManager.instance.BackgroundMusic();
    }

    void OnMusicOff()
    {
        toggleMusic.toggle.isOn = false;
        PlayerPrefs.SetInt("isMusic", 0);
        AudioManager.instance.BackgroundMusic();
    }

    void OnSoundOn()
    {
        toggleSound.toggle.isOn = true;
        PlayerPrefs.SetInt("isSound", 1);
    }

    void OnSoundOff()
    {
        toggleSound.toggle.isOn = false;
        PlayerPrefs.SetInt("isSound", 0);
    }
}
