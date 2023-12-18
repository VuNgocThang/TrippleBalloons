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
    public GameObject lockImg;
    public TextMeshProUGUI txtNumBtn;
    public bool interactable;

    private void Update()
    {
        interactable = btn.interactable;
    }

    public void InitButton()
    {
        if (!PlayerPrefs.HasKey($"{txt}"))
        {
            count = 99;
            btnPlus.gameObject.SetActive(true);
            PlayerPrefs.SetInt($"{txt}", count);
            PlayerPrefs.Save();
        }
        else
        {
            count = PlayerPrefs.GetInt($"{txt}");
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

    private void OnGUI()
    {
        UpdateNumBooster();
    }

    void UpdateNumBooster()
    {
        if (count > 0)
        {
            numBtn.gameObject.SetActive(true);
            txtNumBtn.text = count.ToString();
        }
        else
        {
            btnPlus.gameObject.SetActive(true);
            btn.interactable = false;
        }
    }

}
