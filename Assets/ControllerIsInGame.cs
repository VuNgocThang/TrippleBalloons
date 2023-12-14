using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using DG.Tweening;

public class ControllerIsInGame : MonoBehaviour
{
    public GameObject homePanel;
    public GameObject topHome;
    public GameObject bottomHome;

    public UIGameManager uiInGame;
    public GameObject listPos;
    public LogicUITest logicUI;


    public void UpdateStateIsInGame()
    {
        if (PlayerPrefs.GetInt("IsInGame") == 1)
        {
            homePanel.SetActive(false);
            topHome.SetActive(false);
            bottomHome.SetActive(false);
            uiInGame.gameObject.SetActive(true);
            listPos.SetActive(true);
            StartCoroutine(logicUI.InitTimerSetting());

            if (DataUseInGame.gameData.indexLevel == 0 && !DataUseInGame.gameData.isDaily)
            {
                LogicGame.instance.tutorialManager.handClick.gameObject.SetActive(true);
                LogicGame.instance.tutorialManager.AnimHand();
            }
            if (DataUseInGame.gameData.indexLevel >= 6 && !DataUseInGame.gameData.isDaily)
            {
                logicUI.SelectBooster();
            }
        }
        else
        {
            homePanel.SetActive(true);
            topHome.SetActive(true);
            bottomHome.SetActive(true);
            uiInGame.gameObject.SetActive(false);
            listPos.SetActive(false);
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("IsInGame", 0);
        PlayerPrefs.Save();
    }

}
