using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public void AddItemHint(int itemHint)
    {
        int hint = DataUseInGame.gameData.numHintItem;
        hint += itemHint;
        DataUseInGame.gameData.gold = hint;
        DataUseInGame.instance.SaveData();
    }
}
