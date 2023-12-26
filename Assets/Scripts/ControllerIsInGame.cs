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
        if (GameSave.IS_IN_GAME)
        {
            PanelInGame();
            StartCoroutine(logicUI.InitTimerSetting());

            if (DataUseInGame.gameData.indexLevel == 0 && !DataUseInGame.gameData.isDaily)
            {
                LogicGame.instance.tutorialManager.handClick.gameObject.SetActive(true);
                //LogicGame.instance.tutorialManager.AnimHand();
            }
            if (DataUseInGame.gameData.indexLevel >= 6 && !DataUseInGame.gameData.isDaily)
            {
                logicUI.SelectBooster();
            }
        }
        else
        {
            PanelInHome();
        }
    }

    public void PanelInHome()
    {
        homePanel.SetActive(true);
        topHome.SetActive(true);
        bottomHome.SetActive(true);
        uiInGame.gameObject.SetActive(false);
        listPos.SetActive(false);
    }

    public void PanelInGame()
    {
        homePanel.SetActive(false);
        topHome.SetActive(false);
        bottomHome.SetActive(false);
        uiInGame.gameObject.SetActive(true);
        listPos.SetActive(true);
    }


}
