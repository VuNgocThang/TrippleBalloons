using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
    public GameObject handClick;

    public List<GameObject> listBtnAndBg;
    public GameObject bgBlackFeed3;

    private void OnEnable()
    {
        StateBoosterIfReachLevel();
    }

    private void OnDisable()
    {
        handClick.SetActive(false);
    }

    
    public void StartGame()
    {
        AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.clickMenu);
        if (DataUseInGame.gameData.heart > 0 || DataUseInGame.gameData.isHeartInfinity)
        {
            GameSave.IS_IN_GAME = true;

            AnimationPopup.instance.FadeWhileMoveUp(selectBoosterCG.gameObject, 0.5f);
            selectBoosterCG.DOFade(0f, 0.5f)
                .OnComplete(() =>
                {
                    btnBoosterManager.UpdateStateSelect();

                    controller.UpdateStateIsInGame();

                    if (DataUseInGame.gameData.isDaily)
                    {
                        dailyManager.gameObject.SetActive(false);
                        LogicGame.instance.Instantiate();
                    }
                    else
                    {
                        LogicGame.instance.InitAll();
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
        UpdateNumBooster();
    }

    public void StateBoosterIfReachLevel()
    {
        for (int i = 0; i < 3; i++)
        {
            btnBoosterManager.buttons[i].btn.interactable = false;
            btnBoosterManager.buttons[i].imgLock.gameObject.SetActive(true);
            btnBoosters[i].imgNotice.SetActive(false);
        }

        if (DataUseInGame.gameData.indexLevel >= 6 || DataUseInGame.gameData.isDaily)
        {

            btnBoosterManager.buttons[0].btn.interactable = true;
            btnBoosterManager.buttons[0].imgLock.gameObject.SetActive(false);
            btnBoosters[0].imgNotice.SetActive(true);

        }
        if (DataUseInGame.gameData.indexLevel >= 7 || DataUseInGame.gameData.isDaily)
        {

            btnBoosterManager.buttons[1].btn.interactable = true;
            btnBoosterManager.buttons[1].imgLock.gameObject.SetActive(false);
            btnBoosters[1].imgNotice.SetActive(true);

        }
        if (DataUseInGame.gameData.indexLevel >= 8 || DataUseInGame.gameData.isDaily)
        {
            btnBoosterManager.buttons[2].btn.interactable = true;
            btnBoosterManager.buttons[2].imgLock.gameObject.SetActive(false);
            btnBoosters[2].imgNotice.SetActive(true);
        }

        ShowTextTutBooster();
    }
   
    public void ShowTextTutBooster()
    {
        int indexLevel = DataUseInGame.gameData.indexLevel;
        bool isInGame = GameSave.IS_IN_GAME;

        if (isInGame)
        {
            if (indexLevel == 6)
            {
                //handClick.SetActive(true);
                //Vector3 pos = new Vector3(btnBoosterManager.buttons[0].transform.position.x,
                //    btnBoosterManager.buttons[0].transform.position.y - 0,
                //    btnBoosterManager.buttons[0].transform.position.z);
                //handClick.transform.position = pos;
                StartCoroutine(ActiveHandClick(0));
                var temp = listBtnAndBg[0];
                temp.transform.SetSiblingIndex(4);
                bgBlackFeed3.SetActive(true);

                textTutLight.SetActive(true);
            }
            else if (indexLevel == 7)
            {
                //handClick.SetActive(true);
                //Vector3 pos = new Vector3(btnBoosterManager.buttons[1].transform.position.x,
                //   btnBoosterManager.buttons[1].transform.position.y - 0,
                //   btnBoosterManager.buttons[1].transform.position.z);
                //handClick.transform.position = pos;
                StartCoroutine(ActiveHandClick(1));

                var temp = listBtnAndBg[1];
                temp.transform.SetSiblingIndex(4);
                bgBlackFeed3.SetActive(true);

                textTutTimer.SetActive(true);
            }
            else if (indexLevel == 8)
            {
                //handClick.SetActive(true);
                //Vector3 pos = new Vector3(btnBoosterManager.buttons[2].transform.position.x,
                //   btnBoosterManager.buttons[2].transform.position.y - 0,
                //   btnBoosterManager.buttons[2].transform.position.z);
                //handClick.transform.position = pos;
                StartCoroutine(ActiveHandClick(2));

                var temp = listBtnAndBg[2];
                temp.transform.SetSiblingIndex(4);
                bgBlackFeed3.SetActive(true);

                textTutHint.SetActive(true);
            }
        }
    }

    IEnumerator ActiveHandClick(int index)
    {
        yield return new WaitForSeconds(0.5f);
        handClick.SetActive(true);
        Vector3 pos = new Vector3(btnBoosterManager.buttons[index].transform.position.x,
                  btnBoosterManager.buttons[index].transform.position.y - 0,
                  btnBoosterManager.buttons[index].transform.position.z);
        handClick.transform.position = pos;
    }
    public void UpdateNumBooster()
    {
        for (int i = 0; i < btnBoosters.Count; i++)
        {
            if (DataUseInGame.gameData.indexLevel >= 6 || DataUseInGame.gameData.isDaily)
            {
                if (btnBoosters[0].count > 0)
                {
                    btnBoosters[0].numBtn.gameObject.SetActive(true);
                    btnBoosters[0].txtNumBtn.text = btnBoosters[0].count.ToString();
                }
                else
                {
                    btnBoosters[0].btnPlus.gameObject.SetActive(true);
                    btnBoosters[0].btn.interactable = false;
                }
            }

            if (DataUseInGame.gameData.indexLevel >= 7 || DataUseInGame.gameData.isDaily)
            {
                if (btnBoosters[1].count > 0)
                {
                    btnBoosters[1].numBtn.gameObject.SetActive(true);
                    btnBoosters[1].txtNumBtn.text = btnBoosters[1].count.ToString();
                }
                else
                {
                    btnBoosters[1].btnPlus.gameObject.SetActive(true);
                    btnBoosters[1].btn.interactable = false;
                }
            }

            if (DataUseInGame.gameData.indexLevel >= 8 || DataUseInGame.gameData.isDaily)
            {
                if (btnBoosters[2].count > 0)
                {
                    btnBoosters[2].numBtn.gameObject.SetActive(true);
                    btnBoosters[2].txtNumBtn.text = btnBoosters[2].count.ToString();
                }
                else
                {
                    btnBoosters[2].btnPlus.gameObject.SetActive(true);
                    btnBoosters[2].btn.interactable = false;
                }
            }

        }

    }

}
