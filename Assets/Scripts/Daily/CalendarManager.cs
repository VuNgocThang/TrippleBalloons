using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class CalendarManager : MonoBehaviour
{
    [SerializeField] HeaderManager headerManager;
    [SerializeField] BodyManager bodyManager;
    [SerializeField] DailyManager dailyManager;

    private DateTime targetDateTime;
    private CultureInfo cultureInfo;
    [SerializeField] Button btnPrev;
    [SerializeField] Button btnNext;


    private void Start()
    {
        targetDateTime = DateTime.Now;
        cultureInfo = new CultureInfo("en-US");
        Refresh(targetDateTime.Year, targetDateTime.Month);
        dailyManager.StateBtnPlay();
        bodyManager.SetStateBeforeNow();
        bodyManager.OnClickButton();
        btnPrev.onClick.AddListener(OnGoToPreviousMonthButtonClicked);
        btnNext.onClick.AddListener(OnGoToNextMonthButtonClicked);
    }
    public void OnGoToPreviousMonthButtonClicked()
    {
        if (targetDateTime.Month < 2) return;

        targetDateTime = targetDateTime.AddMonths(-1);
        Refresh(targetDateTime.Year, targetDateTime.Month);
        bodyManager.SetStateBeforeNow();
        bodyManager.OnClickButton();

    }
    public void OnGoToNextMonthButtonClicked()
    {
        if (targetDateTime.Month > 11) return;

        targetDateTime = targetDateTime.AddMonths(+1);
        Refresh(targetDateTime.Year, targetDateTime.Month);
        bodyManager.SetStateBeforeNow();
        bodyManager.OnClickButton();
    }

    private void Refresh(int year, int month)
    {
        headerManager.SetTitle($"{year} {cultureInfo.DateTimeFormat.GetMonthName(month)}");
        bodyManager.buttonsManager.Clear();
        bodyManager.Initialize(year, month);
        if (!bodyManager.buttonsManager[targetDateTime.Day - 1].isDone)
        {
            if (bodyManager.dateTime.Month == DateTime.Now.Month)
            {
                bodyManager.SetSelected(bodyManager.buttonsManager[targetDateTime.Day - 1], true);
            }
            DataUseInGame.gameData.indexDailyLV = bodyManager.buttonsManager[targetDateTime.Day - 1].index - 1;
            DataUseInGame.gameData.year = targetDateTime.Year;
            DataUseInGame.gameData.month = targetDateTime.Month;
            DataUseInGame.gameData.day = bodyManager.buttonsManager[targetDateTime.Day - 1].index;
            DataUseInGame.instance.SaveData();
        }
        else
        {
            DataUseInGame.gameData.indexDailyLV = -1;
            DataUseInGame.instance.SaveData();
        }
    }

}
