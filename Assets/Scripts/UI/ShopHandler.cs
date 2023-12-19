using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopHandler : MonoBehaviour
{
    [SerializeField] Button btnGoldBigBundle;
    [SerializeField] Button btnGoldSuperBundle;
    [SerializeField] Button btnGoldMegaBundle;

    private void Start()
    {
        btnGoldBigBundle.onClick.AddListener(() =>
        {
            IncreaseItemBig(1, 900f, 100);
        });

        btnGoldSuperBundle.onClick.AddListener(() =>
        {
            IncreaseItemBig(5, 5400f, 450);
        });

        btnGoldMegaBundle.onClick.AddListener(() =>
        {
            IncreaseItemBig(10, 10800f, 800);
        });


    }

    void IncreaseItemBig(int numItem, float numTimer, int goldUse)
    {
        AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.clickMenu);

        int numHint = DataUseInGame.gameData.numHintItem;
        int numUndo = DataUseInGame.gameData.numUndoItem;
        int numTrippleUndo = DataUseInGame.gameData.numTrippleUndoItem;
        int numShuffle = DataUseInGame.gameData.numShuffleItem;
        int numFreezeTime = DataUseInGame.gameData.numFreezeTimeItem;

        int numHintBooster;
        int numTimerBooster;
        int numLightningBooster;

        if (PlayerPrefs.HasKey("NumLightning"))
        {
            numLightningBooster = PlayerPrefs.GetInt("NumLightning");
        }
        else
        {
            numLightningBooster = 99;
        }
       
        if (PlayerPrefs.HasKey("NumHint"))
        {
            numHintBooster = PlayerPrefs.GetInt("NumHint");

        }
        else
        {
            numHintBooster = 99;
        }
       
        if (PlayerPrefs.HasKey("NumTimer"))
        {
            numTimerBooster = PlayerPrefs.GetInt("NumTimer");

        }
        else
        {
            numTimerBooster = 99;
        }
       

        bool isHeartInfinity = DataUseInGame.gameData.isHeartInfinity;
        float timeHeartInfinity = DataUseInGame.gameData.timeHeartInfinity;

        if (DataUseInGame.gameData.gold < goldUse) return;

        GameManager.Instance.SubGold(goldUse);

        numHint += numItem;
        numUndo += numItem;
        numTrippleUndo += numItem;
        numShuffle += numItem;
        numFreezeTime += numItem;

        numHintBooster += numItem;
        numTimerBooster += numItem;
        numLightningBooster += numItem;

        PlayerPrefs.SetInt("NumHint", numHintBooster);
        PlayerPrefs.SetInt("NumTimer", numTimerBooster);
        PlayerPrefs.SetInt("NumLightning", numLightningBooster);
        PlayerPrefs.Save();

        isHeartInfinity = true;
        timeHeartInfinity += numTimer;

        DataUseInGame.gameData.numHintItem = numHint;
        DataUseInGame.gameData.numUndoItem = numUndo;
        DataUseInGame.gameData.numTrippleUndoItem = numTrippleUndo;
        DataUseInGame.gameData.numShuffleItem = numShuffle;
        DataUseInGame.gameData.numFreezeTimeItem = numFreezeTime;
        DataUseInGame.gameData.isHeartInfinity = isHeartInfinity;
        DataUseInGame.gameData.timeHeartInfinity = timeHeartInfinity;

        DataUseInGame.instance.SaveData();

       
    }
}
