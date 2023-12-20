using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;

public class HeartTest : MonoBehaviour
{
    // ∞
    public static HeartTest Instance;
    public int heart;
    public int maxHeart = 5;
    public float countdownTimer;
    public TextMeshProUGUI txtNumHeart;
    public TextMeshProUGUI txtCountdownTimer;
    float minutes;
    float seconds;
    public bool canPlusHeart;
    public int time = 30;
    public DateTime lastHeartLossTime;

    private void Awake()
    {
        if (Instance == null) Instance = this;

    }
    private void Start()
    {
        canPlusHeart = true;

        heart = DataUseInGame.gameData.heart;


        if (PlayerPrefs.HasKey("CountdownTimer"))
        {
            float timeSinceLastLoss = (float)(DateTime.Now - DateTime.Parse(PlayerPrefs.GetString("LastHeartLossTime"))).TotalSeconds;

            int increaseHeart = (int)timeSinceLastLoss / time;

            float timeSub = timeSinceLastLoss % time;
            //Debug.Log(DateTime.Now  + " --- " + PlayerPrefs.GetString("LastHeartLossTime") + " --- " + timeSinceLastLoss +  " -- " + timeSub);

            if (DataUseInGame.gameData.maxHeart > DataUseInGame.gameData.heart)
            {
                heart += increaseHeart;
            }
            heart = Mathf.Min(heart, DataUseInGame.gameData.maxHeart);
            DataUseInGame.gameData.heart = heart;
            DataUseInGame.instance.SaveData();

            countdownTimer = PlayerPrefs.GetFloat("CountdownTimer") - timeSub;
            //Debug.Log(countdownTimer + " timer Count Down");
            countdownTimer = Mathf.Max(countdownTimer, 0);

            if (DataUseInGame.gameData.heart >= DataUseInGame.gameData.maxHeart)
            {
                countdownTimer = time;
            }
        }
        else
        {
            countdownTimer = time;
        }

        txtNumHeart.text = heart.ToString();


        lastHeartLossTime = DateTime.Now;
    }
    private void OnDisable()
    {

        //infinityHeart
        PlayerPrefs.SetFloat("CountdownTimerHeartInfinity", DataUseInGame.instance.countdownTimerHeartInfinity);
        PlayerPrefs.SetString("LastTimerQuit", DateTime.Now.ToString());
        PlayerPrefs.Save();

    }

    private void OnGUI()
    {
        if (DataUseInGame.gameData.isHeartInfinity)
        {
            float timer = DataUseInGame.gameData.timeHeartInfinity;
            float hours = Mathf.Floor(timer / 3600);
            float timePerHour = timer - hours * 3600;
            float minutes = Mathf.Floor(timePerHour / 60);
            float seconds = Mathf.RoundToInt(timePerHour % 60);


            txtNumHeart.text = "∞";
            if (hours > 0)
            {
                txtCountdownTimer.text = hours.ToString("00") + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");
            }
            else
            {
                txtCountdownTimer.text = minutes.ToString("00") + ":" + seconds.ToString("00");
            }

        }
        else
        {
            txtNumHeart.text = DataUseInGame.gameData.heart.ToString();

            if (DataUseInGame.gameData.heart == DataUseInGame.gameData.maxHeart)
            {
                txtCountdownTimer.text = "FULL";
            }
            else
            {
                if (countdownTimer > 0)
                {
                    canPlusHeart = true;
                    minutes = Mathf.Floor(countdownTimer / 60);
                    seconds = Mathf.RoundToInt(countdownTimer % 60);

                    txtCountdownTimer.text = minutes.ToString("00") + ":" + seconds.ToString("00");
                }
            }

        }
    }
    private void OnApplicationQuit()
    {
        SaveHeart();

        PlayerPrefs.SetFloat("CountdownTimer", countdownTimer);
        PlayerPrefs.Save();
        if (DataUseInGame.gameData.heart <= DataUseInGame.gameData.maxHeart)
        {
            PlayerPrefs.SetString("LastHeartLossTime", DateTime.Now.ToString());
            PlayerPrefs.Save();
        }
    }

    public void SaveHeart()
    {
        DataUseInGame.gameData.heart = heart;
        DataUseInGame.instance.SaveData();
    }
}
