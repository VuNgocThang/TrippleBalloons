using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerIsInGame : MonoBehaviour
{
    public GameObject homePanel;
    public GameObject topHome;
    public GameObject bottomHome;

    public GameObject uiInGame;
    public GameObject listPos;
    public LogicUITest logicUI;
  
    public void UpdateStateIsInGame()
    {
        if (PlayerPrefs.GetInt("IsInGame") == 1)
        {
            homePanel.SetActive(false);
            topHome.SetActive(false);
            bottomHome.SetActive(false);
            uiInGame.SetActive(true);
            listPos.SetActive(true);
            StartCoroutine(CanClickTrueAgain());
        }
        else
        {
            homePanel.SetActive(true);
            topHome.SetActive(true);
            bottomHome.SetActive(true);
            uiInGame.SetActive(false);
            listPos.SetActive(false);
        }
    }

    IEnumerator CanClickTrueAgain()
    {
        LogicGame.instance.canClick = false;
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(LogicGame.instance.timer.InitTimerSetting());
        LogicGame.instance.canClick = true;
    }

}
