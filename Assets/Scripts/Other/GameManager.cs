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

    private void Start()
    {
        if (PlayerPrefs.HasKey("IsInGame"))
        {
            PlayerPrefs.GetInt("IsInGame");
        }
        else
        {
            PlayerPrefs.SetInt("IsInGame", 0);
        }
    }


    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("IsInGame", 0);
        PlayerPrefs.Save();
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

        DataUseInGame.gameData.numHintItem = numHint;
        DataUseInGame.gameData.numUndoItem = numUndo;
        DataUseInGame.gameData.numTrippleUndoItem = numTrippleUndo;
        DataUseInGame.gameData.numShuffleItem = numShuffle;
        DataUseInGame.gameData.numFreezeTimeItem = numFreezeTime;
        DataUseInGame.instance.SaveData();
    }
}
