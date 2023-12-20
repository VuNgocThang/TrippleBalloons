using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBooster : MonoBehaviour
{
    public string nameBooster;
    public string txt;
    public int count;
    public bool isSelected;
    public Button btn;
    public Button btnPlus;
    public GameObject numBtn;
    public GameObject selected;
    public GameObject imgLock;
    public GameObject imgNotice;
    public TextMeshProUGUI txtNumBtn;
    public bool interactable;

    private void Update()
    {
        interactable = btn.interactable;
    }

    public void InitButton()
    {
        SwitchChange(txt);
    }

    void SwitchChange(string txt)
    {
        switch (txt)
        {
            case "NumHint":
                count = DataUseInGame.gameData.numBoosterHint;
                break;
            case "NumTimer":
                count = DataUseInGame.gameData.numBoosterTimer;
                break;
            case "NumLightning":
                count = DataUseInGame.gameData.numBoosterLightning;
                break;
        }
    }
    public void SaveStateBooster(string str, int i)
    {
        PlayerPrefs.SetInt(str, i);
        PlayerPrefs.Save();
    }

    public void UpdateStateSelect()
    {
        if (isSelected)
        {
            PlayerPrefs.SetInt(nameBooster, 1);
            PlayerPrefs.Save();
        }
        else
        {
            PlayerPrefs.SetInt(nameBooster, 0);
            PlayerPrefs.Save();
        }
    }

   

}
