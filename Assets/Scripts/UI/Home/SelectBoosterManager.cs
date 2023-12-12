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
    //private void Start()
    //{
    //    btnStart.onClick.AddListener(StartGame);
    //}

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
                    Debug.Log("saasmcsacacssmacc ");
                    gameObject.SetActive(false);
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
                    DOTween.KillAll();
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

}
