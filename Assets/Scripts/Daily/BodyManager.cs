using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using UnityEngine;
using UnityEngine.UIElements;

public class BodyManager : MonoBehaviour
{
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private GameObject placeHolderPrefab;
    private List<GameObject> cells;
    public List<ButtonManager> buttonsManager;
    [SerializeField] DailyManager dailyManager;

    public DateTime dateTime = new DateTime();
    public void Initialize(int year, int month)
    {
        dateTime = new DateTime(year, month, 1);
        var daysInMonth = DateTime.DaysInMonth(year, month);

        var dayOfWeek = (int)dateTime.DayOfWeek;
        var size = (dayOfWeek + daysInMonth) / 7;

        if ((dayOfWeek + daysInMonth) % 7 > 0)
            size++;

        var arr = new int[size * 7];

        for (var i = 0; i < daysInMonth; i++)
            arr[dayOfWeek + i] = i + 1;

        if (cells == null)
            cells = new List<GameObject>();

        foreach (var c in cells)
            Destroy(c);

        cells.Clear();

        foreach (var a in arr)
        {
            var instance = Instantiate(a == 0 ? placeHolderPrefab : buttonPrefab, transform);
            var buttonManager = instance.GetComponent<ButtonManager>();

            if (buttonManager != null)
            {
                buttonManager.Initialize(a.ToString());
                buttonsManager.Add(buttonManager);
            }
            cells.Add(instance);
        }
        for (int j = 0; j < buttonsManager.Count; j++)
        {
            for (int i = 0; i < DataUseInGame.gameData.dailyData.Count; i++)
            {
                Debug.Log(dateTime.Year + " --- " + dateTime.Month  +" ---" + buttonsManager[j].index);
                Debug.Log(DataUseInGame.gameData.dailyData[i].year + " ... " + DataUseInGame.gameData.dailyData[i].month + " ... " + DataUseInGame.gameData.dailyData[i].day);
                if (dateTime.Year == DataUseInGame.gameData.dailyData[i].year
                    && dateTime.Month == DataUseInGame.gameData.dailyData[i].month
                    && buttonsManager[j].index == DataUseInGame.gameData.dailyData[i].day)
                {
                    buttonsManager[j].label.color = Color.red;
                    buttonsManager[j].isDone = true;
                }
            }
        }
    }

    public void OnClickButton()
    {
        foreach (var button in buttonsManager)
        {
            button.button.onClick.AddListener(() =>
            {
                if (!button.isDone)
                {
                    AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.clickMenu);

                    OnClickState();
                    SetSelected(button, true);
                    UpdateState();
                    //UpdateTextPlay(button);
                    DataUseInGame.gameData.isDaily = true;
                    DataUseInGame.gameData.indexDailyLV = button.index - 1;
                    DataUseInGame.gameData.year = dateTime.Year;
                    DataUseInGame.gameData.month = dateTime.Month;
                    DataUseInGame.gameData.day = button.index;
                    //Debug.Log(dateTime.Year + " ------ " + DataUseInGame.gameData.year);
                    DataUseInGame.instance.SaveData();

                    dailyManager.StateBtnPlay();

                }
            });
        }
    }

    public void OnClickState()
    {
        foreach (var button in buttonsManager)
        {
            SetSelected(button, false);
        }
    }

    public void SetSelected(ButtonManager btn, bool selected)
    {
        btn.isSelected = selected;

        if (btn.isSelected)
        {
            btn.mark.SetActive(true);
        }
    }

    public void UpdateState()
    {
        foreach (var button in buttonsManager)
        {
            if (button.isSelected)
            {
                button.mark.SetActive(true);
            }
            else
            {
                button.mark.SetActive(false);
            }
        }
    }

    public void SetStateBeforeNow()
    {
        foreach (var button in buttonsManager)
        {
            if (dateTime.Month < DateTime.Now.Month || button.index < DateTime.Now.Day && dateTime.Month <= DateTime.Now.Month)
            {
                if (!button.isDone)
                {
                    button.label.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                }
            }

            if (dateTime.Month > DateTime.Now.Month || button.index > DateTime.Now.Day && dateTime.Month >= DateTime.Now.Month)
            {
                button.button.interactable = false;
            }

        }
    }

    public void UpdateTextPlay(ButtonManager button)
    {
        CultureInfo cultureInfo = new CultureInfo("en-US");
        if (dateTime.Month < DateTime.Now.Month || button.index < DateTime.Now.Day && dateTime.Month <= DateTime.Now.Month)
        {
            if (!button.isDone)
            {
                dailyManager.txtPlayThisDay.text = $"Play {button.index} - {cultureInfo.DateTimeFormat.GetMonthName(dateTime.Month)} ";
            }
        }
        else
        {
            dailyManager.txtPlayThisDay.text = $"Play";
        }
    }

}
