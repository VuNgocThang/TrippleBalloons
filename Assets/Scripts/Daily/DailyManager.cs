using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyManager : MonoBehaviour
{
    public Button btnPlayThisDay;
    public TextMeshProUGUI txtPlayThisDay;
    public LogicUITest logicUI;

    private void Start()
    {
        btnPlayThisDay.onClick.AddListener(LoadSceneGame);
    }

    public void StateBtnPlay()
    {
        if (DataUseInGame.gameData.indexDailyLV >= 0)
        {
            btnPlayThisDay.interactable = true;
        }
        else
        {
            btnPlayThisDay.interactable = false;
        }
    }

    void LoadSceneGame()
    {
        logicUI.SelectBooster();
    }


}
