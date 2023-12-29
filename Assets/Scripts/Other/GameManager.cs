using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool canRotate;
    public bool isInGame;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        //Debug.Log(GameSave.IS_IN_GAME + " --- ISINGAME");
    }
    public void SubHeart()
    {
        int heart = DataUseInGame.gameData.heart;

        if (heart >= 5)
        {
            PlayerPrefs.SetString("LastHeartLossTime", DateTime.Now.ToString());
            PlayerPrefs.Save();
        }
        if (!DataUseInGame.gameData.isHeartInfinity)
        {
            heart--;
            Debug.Log("heartttt" + heart);
            DataUseInGame.gameData.heart = heart;
            DataUseInGame.instance.SaveData();
        }

        if (heart <= 0)
        {
            heart = 0;
        }

        DataUseInGame.gameData.heart = heart;
        DataUseInGame.instance.SaveData();
    }
    public void AddStar(int multi)
    {
        //100 + số level * 20 + số giây còn lại * 2
        int star = DataUseInGame.gameData.star;
        int starAdd = 100 + (DataUseInGame.gameData.indexLevel + 1) * 20 + Mathf.RoundToInt(LogicGame.instance.timer.timeLeft) * 2;
        int multiStar = multi * starAdd;
        DataUseInGame.gameData.star = star + multiStar;
        DataUseInGame.instance.SaveData();
    }
    public void SubStar(int starSub)
    {
        int star = DataUseInGame.gameData.star;
        star -= starSub;
        DataUseInGame.gameData.star = star;
        DataUseInGame.instance.SaveData();
    }

    public void AddGold(int goldAdd)
    {
        int gold = DataUseInGame.gameData.gold;
        gold += goldAdd;
        DataUseInGame.gameData.gold = gold;
        DataUseInGame.instance.SaveData();
    }
    public void SubGold(int goldSub)
    {
        int gold = DataUseInGame.gameData.gold;
        gold -= goldSub;
        DataUseInGame.gameData.gold = gold;
        DataUseInGame.instance.SaveData();
    }

    public void IncreaseItemBig(int numItem)
    {
        AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.clickMenu);

        int numHint = DataUseInGame.gameData.numHintItem;
        int numUndo = DataUseInGame.gameData.numUndoItem;
        int numTrippleUndo = DataUseInGame.gameData.numTrippleUndoItem;
        int numShuffle = DataUseInGame.gameData.numShuffleItem;
        int numFreezeTime = DataUseInGame.gameData.numFreezeTimeItem;

        int numHintBooster = DataUseInGame.gameData.numBoosterHint;
        int numTimerBooster = DataUseInGame.gameData.numBoosterTimer;
        int numLightningBooster = DataUseInGame.gameData.numBoosterLightning;

       

        numHint += numItem;
        numUndo += numItem;
        numTrippleUndo += numItem;
        numShuffle += numItem;
        numFreezeTime += numItem;

        numHintBooster += numItem;
        numTimerBooster += numItem;
        numLightningBooster += numItem;

        

        DataUseInGame.gameData.numHintItem = numHint;
        DataUseInGame.gameData.numUndoItem = numUndo;
        DataUseInGame.gameData.numTrippleUndoItem = numTrippleUndo;
        DataUseInGame.gameData.numShuffleItem = numShuffle;
        DataUseInGame.gameData.numFreezeTimeItem = numFreezeTime;
        DataUseInGame.gameData.numBoosterHint = numHintBooster;
        DataUseInGame.gameData.numBoosterTimer = numTimerBooster;
        DataUseInGame.gameData.numBoosterLightning = numLightningBooster;
        DataUseInGame.instance.SaveData();
    }
}
