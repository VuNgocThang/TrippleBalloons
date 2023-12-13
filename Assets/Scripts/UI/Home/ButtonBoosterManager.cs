using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBoosterManager : MonoBehaviour
{
    public List<ButtonBooster> buttons = new List<ButtonBooster>();
    private void Start()
    {
        Init();
        OnClick(buttons[0], buttons[0].selected, buttons[0].isSelected);
        OnClick(buttons[1], buttons[1].selected, buttons[1].isSelected);
        OnClick(buttons[2], buttons[2].selected, buttons[2].isSelected);

    }
    private void OnGUI()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].InitButton();
        }
    }
    void Init()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].InitButton();
            buttons[i].SaveStateBooster(buttons[i].nameBooster, 0);
        }
    }

    private void Update()
    {
        IncreaseTest();
    }

    public void OnClick(ButtonBooster buttonBooster, GameObject selectedObj, bool isSelected)
    {
        buttonBooster.btn.onClick.AddListener(() =>
        {
            AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.clickMenu);

            if (!selectedObj.activeSelf)
            {
                selectedObj.SetActive(true);
                if (buttonBooster.count > 0)
                {
                    buttonBooster.isSelected = true;
                    buttonBooster.SaveStateBooster($"{buttonBooster.nameBooster}", 1);
                }
            }
            else
            {
                selectedObj.SetActive(false);
                if (buttonBooster.count > 0)
                {
                    buttonBooster.isSelected = false;
                    buttonBooster.SaveStateBooster($"{buttonBooster.nameBooster}", 0);
                }
            }
        });
    }

    public void UpdateStateSelect()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].UpdateStateSelect();
        }
    }

    void IncreaseTest()
    {
        if (Input.GetKeyUp(KeyCode.V))
        {
            buttons[0].count++;
            PlayerPrefs.SetInt("NumLightning", buttons[0].count);
            PlayerPrefs.Save();
            Init();
        }

        if (Input.GetKeyUp(KeyCode.B))
        {
            buttons[1].count++;
            PlayerPrefs.SetInt("NumTimer", buttons[1].count);
            PlayerPrefs.Save();
            Init();
        }

        if (Input.GetKeyUp(KeyCode.N))
        {
            buttons[2].count++;
            PlayerPrefs.SetInt("NumHint", buttons[2].count);
            PlayerPrefs.Save();
            Init();
        }
    }



}
