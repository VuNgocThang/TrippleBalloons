using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float timeLeft;
    float minutes;
    float seconds;
    public bool timeOut;
    public bool stopTimer;
    public bool isFreeze;

    public GameObject usingBooster;
    public GameObject usingBoosterHint;
    public GameObject usingBoosterTimer;
    public GameObject usingBoosterLightning;
    public CanvasGroup usingBoosterCG;

    public ParticleSystem particleTimer;


    public void Init()
    {
        if (PlayerPrefs.GetInt(GameSave.BOOSTER_HINT) == 1)
        {
            usingBoosterHint.SetActive(true);
        }

        if (PlayerPrefs.GetInt(GameSave.BOOSTER_TIMER) == 1)
        {
            usingBoosterTimer.SetActive(true);
        }

        if (PlayerPrefs.GetInt(GameSave.BOOSTER_LIGHTNING) == 1)
        {
            usingBoosterLightning.SetActive(true);
        }


    }
    private void Update()
    {
        if (!stopTimer && !isFreeze)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0 && !LogicGame.instance.checkLose)
            {
                timeLeft = 0;
                stopTimer = true;
                timeOut = true;
                Debug.Log("you lose out of time");
                LogicGame.instance.checkLose = true;
                LogicGame.instance.Lose();
                LogicGame.instance.logicUI.OpenLoseUI();
                LogicGame.instance.logicUI.loseUI.OpenPanelTimeUp();

            }
        }

    }
    public void OnGUI()
    {
        if (!timeOut)
        {
            if (timeLeft > 0)
            {
                minutes = Mathf.Floor(timeLeft / 60);
                seconds = Mathf.RoundToInt(timeLeft % 60);
                timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
            }
            else
            {
                stopTimer = true;
            }
        }
    }

}