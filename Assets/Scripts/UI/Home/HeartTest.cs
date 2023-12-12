using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class HeartTest : MonoBehaviour
{
    // ∞
    public int heart;
    public int maxHeart = 5;
    public float countdownTimer;
    public TextMeshProUGUI txtNumHeart;
    public TextMeshProUGUI txtCountdownTimer;
    float minutes;
    float seconds;
    public bool canPlusHeart;
    int time = 30;
    private DateTime lastHeartLossTime;

    private void Start()
    {
        canPlusHeart = true;

        heart = DataUseInGame.gameData.heart;


        if (PlayerPrefs.HasKey("CountdownTimer"))
        {
            float timeSinceLastLoss = (float)(DateTime.Now - DateTime.Parse(PlayerPrefs.GetString("LastHeartLossTime"))).TotalSeconds;

            int increaseHeart = (int)timeSinceLastLoss / time;
            float timeSub = timeSinceLastLoss % time;

            heart += increaseHeart;
            heart = Mathf.Min(heart, maxHeart);
            SaveHeart();
            countdownTimer = PlayerPrefs.GetFloat("CountdownTimer") - timeSub;
            countdownTimer = Mathf.Max(countdownTimer, 0);

            if (heart >= maxHeart)
            {
                countdownTimer = time;
            }
        }
        else
        {
            countdownTimer = time;
        }


        lastHeartLossTime = DateTime.Now;
        txtNumHeart.text = heart.ToString();
    }
    private void OnDisable()
    {
        //heart
        PlayerPrefs.SetFloat("CountdownTimer", countdownTimer);
        if (heart <= maxHeart)
        {
            PlayerPrefs.SetString("LastHeartLossTime", DateTime.Now.ToString());
            PlayerPrefs.Save();
        }
        PlayerPrefs.Save();

        //infinityHeart
        PlayerPrefs.SetFloat("CountdownTimerHeartInfinity", DataUseInGame.instance.countdownTimerHeartInfinity);
        PlayerPrefs.SetString("LastTimerQuit", DateTime.Now.ToString());
        PlayerPrefs.Save();

    }
    private void Update()
    {
        if (countdownTimer <= 0 && canPlusHeart)
        {
            heart++;
            SaveHeart();
            lastHeartLossTime = DateTime.Now;
            countdownTimer = time;
            canPlusHeart = false;
        }
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

            //float minutes = Mathf.Floor(DataUseInGame.gameData.timeHeartInfinity / 60);
            //float seconds = Mathf.RoundToInt(DataUseInGame.gameData.timeHeartInfinity % 60);

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
            txtNumHeart.text = heart.ToString();

            if (heart == maxHeart)
            {
                txtCountdownTimer.text = "FULL";
            }
            else
            {
                TimeSpan timeSinceLoss = DateTime.Now - lastHeartLossTime;

                if (timeSinceLoss.TotalSeconds < countdownTimer)
                {
                    countdownTimer -= (float)timeSinceLoss.TotalSeconds;
                }
                else
                {
                    countdownTimer = 0;
                }

                lastHeartLossTime = DateTime.Now;

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
        PlayerPrefs.SetString("LastHeartLossTime", DateTime.Now.ToString());
        PlayerPrefs.Save();
    }

    public void SaveHeart()
    {
        DataUseInGame.gameData.heart = heart;
        DataUseInGame.instance.SaveData();

    }
}
