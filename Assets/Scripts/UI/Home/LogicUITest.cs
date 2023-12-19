using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LogicUITest : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] Button btnSetting;
    [SerializeField] Button btnCloseSetting;
    [SerializeField] GameObject panelSetting;
    [SerializeField] CanvasGroup panelSettingCG;

    [Header("Star Collector")]
    [SerializeField] Button btnStarCollector;
    [SerializeField] Button btnCloseStarCollector;
    [SerializeField] GameObject panelStarCollector;
    [SerializeField] TextMeshProUGUI txtTimerStarCollector;

    [Header("Play")]
    [SerializeField] Button btnPlay;
    [SerializeField] Button btnStart;
    [SerializeField] Button btnCloseSelectBooster;
    public SelectBoosterManager selectBooster;
    [SerializeField] CanvasGroup selectBoosterCG;


    [Header("Shop")]
    [SerializeField] Button btnShop;
    [SerializeField] TextMeshProUGUI txtShop;
    [SerializeField] RectTransform imgShop;
    [SerializeField] GameObject panelShop;
    [SerializeField] Image imgShopSelected;
    [SerializeField] GameObject imgBottom;

    [Header("Home")]
    [SerializeField] Button btnHome;
    [SerializeField] TextMeshProUGUI txtHome;
    [SerializeField] RectTransform imgHome;
    [SerializeField] GameObject panelHome;
    [SerializeField] Image imgHomeSelected;
    [SerializeField] GameObject topObject;

    [Header("Daily")]
    [SerializeField] Button btnDaily;
    [SerializeField] TextMeshProUGUI txtDaily;
    [SerializeField] RectTransform imgDaily;
    [SerializeField] GameObject panelDaily;
    [SerializeField] Image imgDailySelected;

    [SerializeField] HomeUI homeUI;
    [SerializeField] ControllerIsInGame controller;
    [SerializeField] GameObject usingBooster;
    [SerializeField] CanvasGroup usingBoosterCG;

    public List<Sprite> spriteSelects = new List<Sprite>();
    private void Start()
    {
        btnSetting.onClick.AddListener(OpenPanelSetting);
        btnCloseSetting.onClick.AddListener(ClosePanelSetting);

        btnStarCollector.onClick.AddListener(OpenPanelStarCollector);
        btnCloseStarCollector.onClick.AddListener(ClosePanelStarCollector);

        btnPlay.onClick.AddListener(SelectBooster);
        selectBooster.StateBoosterIfReachLevel();
        btnStart.onClick.AddListener(selectBooster.StartGame);
        btnCloseSelectBooster.onClick.AddListener(ClosePanelBooster);

        btnShop.onClick.AddListener(OpenPanelShop);
        btnHome.onClick.AddListener(OpenPanelHome);
        btnDaily.onClick.AddListener(OpenDailyPanel);
        homeUI.InitTest();
    }


    void OpenPanelSetting()
    {
        AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.clickMenu);
        panelSetting.SetActive(true);
        AnimationPopup.instance.DoTween_Button(panelSettingCG.gameObject, 0, 200, 0.5f);
        panelSettingCG.DOFade(1f, 0.5f);

    }
    void ClosePanelSetting()
    {
        AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.clickMenu);
        AnimationPopup.instance.FadeWhileMoveUp(panelSettingCG.gameObject, 0.5f);
        panelSettingCG.DOFade(0f, 0.5f)
            .OnComplete(() =>
            {
                panelSetting.SetActive(false);
            });
    }

    void OpenPanelStarCollector()
    {
        AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.clickMenu);
        panelStarCollector.SetActive(true);
    }
    void ClosePanelStarCollector()
    {
        AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.clickMenu);
        panelStarCollector.SetActive(false);
    }

    public void SelectBooster()
    {
        if (DataUseInGame.gameData.indexLevel > 5 || DataUseInGame.gameData.isDaily)
        {
            LogicGame.instance.timer.stopTimer = true;
            AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.clickMenu);
            selectBooster.gameObject.SetActive(true);
            AnimationPopup.instance.DoTween_Button(selectBooster.selectBoosterCG.gameObject, 0, 200, 0.5f);
            selectBooster.selectBoosterCG.DOFade(1f, 0.5f);
        }
        else
        {
            if (DataUseInGame.gameData.heart <= 0) return;
            LogicGame.instance.timer.stopTimer = false;
            PlayerPrefs.SetInt("IsInGame", 1);
            PlayerPrefs.Save();
            controller.UpdateStateIsInGame();

            if (!DataUseInGame.gameData.isDaily)
            {
                LogicGame.instance.InitAll();
            }


            DOTween.KillAll();
        }

        if (PlayerPrefs.GetInt("IsInGame") == 1)
        {
            btnCloseSelectBooster.gameObject.SetActive(false);
        }

    }
    public void ClosePanelBooster()
    {
        selectBooster.UnSelectedBtn();
        AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.clickMenu);
        AnimationPopup.instance.FadeWhileMoveUp(selectBooster.selectBoosterCG.gameObject, 0.5f);
        selectBooster.selectBoosterCG.DOFade(0f, 0.5f)
            .OnComplete(() =>
            {
                selectBooster.gameObject.SetActive(false);
            });
    }

    // 1 unselect
    // 0 select
    public void OpenPanelShop()
    {
        AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.clickMenu);
        panelHome.SetActive(false);
        imgHomeSelected.sprite = spriteSelects[1];
        txtHome.gameObject.SetActive(false);
        imgHome.DOAnchorPosY(25, 0.3f, true);
        topObject.SetActive(false);

        panelDaily.SetActive(false);
        imgDailySelected.sprite = spriteSelects[1];
        txtDaily.gameObject.SetActive(false);
        imgDaily.DOAnchorPosY(0, 0.3f, true);
        topObject.SetActive(false);

        panelShop.SetActive(true);
        imgBottom.SetActive(true);
        imgShopSelected.sprite = spriteSelects[0];



        imgShop.DOAnchorPosY(70, 0.3f, true)
            .OnStart(() =>
            {
                txtShop.gameObject.SetActive(true);
            })
            .OnComplete(() =>
            {
                imgShop.gameObject.SetActive(true);
            });

        DataUseInGame.gameData.isDaily = false;
        DataUseInGame.instance.SaveData();

    }

    void OpenPanelHome()
    {
        AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.clickMenu);
        panelShop.SetActive(false);
        imgBottom.SetActive(false);
        imgShopSelected.sprite = spriteSelects[1];
        txtShop.gameObject.SetActive(false);
        imgShop.DOAnchorPosY(0, 0.3f, true);

        panelDaily.SetActive(false);
        imgDailySelected.sprite = spriteSelects[1];
        txtDaily.gameObject.SetActive(false);
        imgDaily.DOAnchorPosY(0, 0.3f, true);

        topObject.SetActive(true);
        panelHome.SetActive(true);
        imgHomeSelected.sprite = spriteSelects[0];
        imgHome.DOAnchorPosY(87, 0.3f, true)
            .OnStart(() =>
            {
                txtHome.gameObject.SetActive(true);
            })
            .OnComplete(() =>
            {
                imgHome.gameObject.SetActive(true);
            });

        DataUseInGame.gameData.isDaily = false;
        DataUseInGame.instance.SaveData();

    }

    void OpenDailyPanel()
    {
        AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.clickMenu);
        panelHome.SetActive(false);
        imgBottom.SetActive(false);
        imgHomeSelected.sprite = spriteSelects[1];
        txtHome.gameObject.SetActive(false);
        imgHome.DOAnchorPosY(25, 0.3f, true);
        topObject.SetActive(true);

        panelShop.SetActive(false);
        imgShopSelected.sprite = spriteSelects[1];
        txtShop.gameObject.SetActive(false);
        imgShop.DOAnchorPosY(0, 0.3f, true);

        panelDaily.SetActive(true);
        imgDailySelected.sprite = spriteSelects[0];
        imgDaily.DOAnchorPosY(70, 0.3f, true)
            .OnStart(() =>
            {
                txtDaily.gameObject.SetActive(true);

            })
            .OnComplete(() =>
            {
                imgDaily.gameObject.SetActive(true);
            });

        DataUseInGame.gameData.isDaily = true;
        DataUseInGame.instance.SaveData();
    }

    public IEnumerator InitTimerSetting()
    {
        if (PlayerPrefs.GetInt(GameSave.BOOSTER_HINT) == 1 && PlayerPrefs.GetInt(GameSave.NUM_BOOSTER_HINT) > 0
            || PlayerPrefs.GetInt(GameSave.BOOSTER_TIMER) == 1 && PlayerPrefs.GetInt(GameSave.NUM_BOOSTER_TIMER) > 0
            || PlayerPrefs.GetInt(GameSave.BOOSTER_LIGHTNING) == 1 && PlayerPrefs.GetInt(GameSave.NUM_BOOSTER_LIGHTNING) > 0
            )
        {
            usingBooster.SetActive(true);
            LogicGame.instance.timer.Init();
            LogicGame.instance.isUseBooster = true;
            LogicGame.instance.timer.stopTimer = true;
            AnimationPopup.instance.FadeWhileMoveUp(LogicGame.instance.timer.usingBoosterCG.gameObject, 1f);
            usingBoosterCG.DOFade(0f, 1f)
                .OnComplete(() =>
                {
                    usingBooster.SetActive(false);
                });

            yield return new WaitForSeconds(1f);

            LogicGame.instance.UseBooster();

            yield return new WaitForSeconds(1f);

            LogicGame.instance.isUseBooster = false;
            SetStateDefaultUseBooster();
            LogicGame.instance.timer.stopTimer = false;
            LogicGame.instance.timer.timeOut = false;
            LogicGame.instance.timer.isFreeze = false;
        }
        else
        {
            LogicGame.instance.timer.stopTimer = false;
            LogicGame.instance.timer.timeOut = false;
            LogicGame.instance.timer.isFreeze = false;
            LogicGame.instance.isUseBooster = false;
        }
    }

    private void SetStateDefaultUseBooster()
    {
        PlayerPrefs.SetInt(GameSave.BOOSTER_HINT, 0);
        PlayerPrefs.SetInt(GameSave.BOOSTER_TIMER, 0);
        PlayerPrefs.SetInt(GameSave.BOOSTER_LIGHTNING, 0);
        PlayerPrefs.Save();
    }



    private void OnGUI()
    {
        float timerStarCollector = DataUseInGame.gameData.timeStarCollector;
        float hours = Mathf.Floor(timerStarCollector / 3600);

        float timePerHour = timerStarCollector - hours * 3600;
        float minutes = Mathf.Floor(timePerHour / 60);
        float seconds = Mathf.RoundToInt(timePerHour % 60);

        txtTimerStarCollector.text = hours.ToString("00") + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    private void OnApplicationQuit()
    {
        SetStateDefaultUseBooster();

        PlayerPrefs.SetInt("IsInGame", 0);
        PlayerPrefs.Save();
    }


    //bool isPaused = false;
    //void OnApplicationFocus(bool hasFocus)
    //{
    //    isPaused = !hasFocus;
    //    if (hasFocus)
    //    {
    //        PlayerPrefs.SetInt("IsInGame", 1);
    //        PlayerPrefs.Save();
    //    }
    //    else
    //    {
    //        PlayerPrefs.SetInt("IsInGame", 0);
    //        PlayerPrefs.Save();
    //    }
    //}
    //void OnApplicationPause(bool pauseStatus)
    //{
    //    isPaused = pauseStatus;
    //}

}
