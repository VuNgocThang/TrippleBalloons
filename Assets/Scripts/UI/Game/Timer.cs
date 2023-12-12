using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    public IEnumerator InitTimerSetting()
    {
        if (PlayerPrefs.GetInt("BoosterHint") == 1 && PlayerPrefs.GetInt("NumHint") > 0
            || PlayerPrefs.GetInt("BoosterTimer") == 1 && PlayerPrefs.GetInt("NumTimer") > 0
            || PlayerPrefs.GetInt("BoosterLightning") == 1 && PlayerPrefs.GetInt("NumLightning") > 0
            )
        {
            usingBooster.SetActive(true);
            Init();
            LogicGame.instance.isUseBooster = true;
            Debug.Log("asdadadasdasdad");
            AnimationPopup.instance.FadeWhileMoveUp(usingBoosterCG.gameObject, 2f);
            usingBoosterCG.DOFade(0f, 2f)
                .OnComplete(() =>
                {
                    usingBooster.SetActive(false);
                });
            yield return new WaitForSeconds(2f);
            LogicGame.instance.UseBooster();
            yield return new WaitForSeconds(1f);
            LogicGame.instance.isUseBooster = false;
            PlayerPrefs.SetInt("BoosterHint", 0);
            PlayerPrefs.SetInt("BoosterTimer", 0);
            PlayerPrefs.SetInt("BoosterLightning", 0);
            PlayerPrefs.Save();
            stopTimer = false;
            timeOut = false;
            isFreeze = false;
        }
        else
        {
            timeOut = false;
            isFreeze = false;
            LogicGame.instance.isUseBooster = false;
        }
    }

    void Init()
    {
        if (PlayerPrefs.GetInt("BoosterHint") == 1)
        {
            usingBoosterHint.SetActive(true);
        }

        if (PlayerPrefs.GetInt("BoosterTimer") == 1)
        {
            usingBoosterTimer.SetActive(true);
        }

        if (PlayerPrefs.GetInt("BoosterLightning") == 1)
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