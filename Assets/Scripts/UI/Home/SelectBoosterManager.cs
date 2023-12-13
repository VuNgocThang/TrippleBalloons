using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectBoosterManager : MonoBehaviour
{
    [SerializeField] ButtonBoosterManager btnBoosterManager;
    [SerializeField] List<ButtonBooster> btnBoosters;
    public CanvasGroup selectBoosterCG;
    [SerializeField] TextMeshProUGUI txtNumLV;
    [SerializeField] ControllerIsInGame controller;
    [SerializeField] DailyManager dailyManager;
    [SerializeField] LogicUITest logicUI;
    [SerializeField] GameObject textTutLight;
    [SerializeField] GameObject textTutTimer;
    [SerializeField] GameObject textTutHint;

    public void StartGame()
    {
        AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.clickMenu);
        if (DataUseInGame.gameData.heart > 0 || DataUseInGame.gameData.isHeartInfinity)
        {
            PlayerPrefs.SetInt("IsInGame", 1);
            PlayerPrefs.Save();

            AnimationPopup.instance.FadeWhileMoveUp(selectBoosterCG.gameObject, 0.5f);
            selectBoosterCG.DOFade(0f, 0.5f)
                .OnComplete(() =>
                {
                    btnBoosterManager.UpdateStateSelect();

                    controller.UpdateStateIsInGame();

                    if (!DataUseInGame.gameData.isDaily)
                    {
                        LogicGame.instance.InitAll();
                    }
                    else
                    {
                        dailyManager.gameObject.SetActive(false);
                        LogicGame.instance.Instantiate();
                    }
                    if (!LogicGame.instance.isUseBooster)
                    {
                        LogicGame.instance.timer.stopTimer = false;
                    }

                    logicUI.selectBooster.gameObject.SetActive(false);
                });
        }
        else
        {
            Debug.Log("Not Enough Heart");
        }
    }

    public void UnSelectedBtn()
    {
        for (int i = 0; i < btnBoosters.Count; i++)
        {
            btnBoosters[i].selected.SetActive(false);
            btnBoosters[i].SaveStateBooster(btnBoosters[i].nameBooster, 0);
        }
    }

    private void OnGUI()
    {
        if (!DataUseInGame.gameData.isDaily)
        {
            int indexLevel = DataUseInGame.gameData.indexLevel + 1;
            txtNumLV.text = indexLevel.ToString();
        }
        else
        {
            int indexLevel = DataUseInGame.gameData.indexDailyLV + 1;
            txtNumLV.text = indexLevel.ToString();
        }

    }

    public void StateBoosterIfReachLevel()
    {
        for (int i = 0; i < 3; i++)
        {
            btnBoosterManager.buttons[i].btn.interactable = false;
            btnBoosterManager.buttons[i].lockImg.gameObject.SetActive(true);
        }

        if (DataUseInGame.gameData.indexLevel >= 6 || DataUseInGame.gameData.isDaily)
        {
            btnBoosterManager.buttons[0].btn.interactable = true;
            btnBoosterManager.buttons[0].lockImg.gameObject.SetActive(false);
        }
        if (DataUseInGame.gameData.indexLevel >= 7 || DataUseInGame.gameData.isDaily)
        {
            btnBoosterManager.buttons[1].btn.interactable = true;
            btnBoosterManager.buttons[1].lockImg.gameObject.SetActive(false);
        }
        if (DataUseInGame.gameData.indexLevel >= 8 || DataUseInGame.gameData.isDaily)
        {
            btnBoosterManager.buttons[2].btn.interactable = true;
            btnBoosterManager.buttons[2].lockImg.gameObject.SetActive(false);
        }

        ShowTextTutBooster();
    }

    public void ShowTextTutBooster()
    {
        int indexLevel = DataUseInGame.gameData.indexLevel;
        int isInGame = PlayerPrefs.GetInt("IsInGame");

        if (isInGame == 1)
        {
            if (indexLevel == 6)
            {
                textTutLight.SetActive(true);
                btnBoosterManager.buttons[0].isSelected = true;
                btnBoosterManager.buttons[0].selected.SetActive(true);
            }
            else if (indexLevel == 7)
            {
                textTutTimer.SetActive(true);
                btnBoosterManager.buttons[1].isSelected = true;
                btnBoosterManager.buttons[1].selected.SetActive(true);
            }
            else if (indexLevel == 8)
            {
                textTutHint.SetActive(true);
                btnBoosterManager.buttons[2].isSelected = true;
                btnBoosterManager.buttons[2].selected.SetActive(true);
            }
        }
    }



}
