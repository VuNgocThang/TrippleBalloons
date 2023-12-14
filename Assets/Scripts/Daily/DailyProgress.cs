using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyProgress : MonoBehaviour
{
    public float currentReward;
    public float reward;
    public float max;
    public Image progressImage;
    public List<RewardDaily> listRewardDaily;
    public List<int> GetRequiredReward = new List<int>()
    {
        7, 14, 28
    };

    private void Start()
    {
        max = DataUseInGame.gameData.maxRewardDaily;
        currentReward = DataUseInGame.gameData.currentRewardDaily;
        if (PlayerPrefs.HasKey("Reward"))
        {
            reward = PlayerPrefs.GetFloat("Reward");
        }
        else
        {
            reward = 0;
        }
        Init();
        LoadReward();
        UpdateReward();
    }
    void Init()
    {
        for (int i = 0; i < listRewardDaily.Count; i++)
        {
            listRewardDaily[i].SaveStateReward(i);
        }
    }

    private void Update()
    {
        LoadReward();
    }
    public void LoadReward()
    {
        if (reward < currentReward)
        {
            reward += Time.deltaTime;
        }
        progressImage.fillAmount = reward / max;
    }

    private void OnDisable()
    {
        reward = currentReward;
        PlayerPrefs.SetFloat("Reward", reward);
    }

    void UpdateReward()
    {
        //if (currentReward < 7)
        //{
        //    listRewardDaily[0].btnSelect.interactable = false;
        //    listRewardDaily[0].vfx.SetActive(false);
        //}
        //else
        //{
        //    if (listRewardDaily[0].isCollected)
        //    {
        //        listRewardDaily[0].btnSelect.interactable = false;
        //        listRewardDaily[0].vfx.SetActive(false);
        //    }
        //    else
        //    {
        //        listRewardDaily[0].btnSelect.interactable = true;
        //        listRewardDaily[0].vfx.SetActive(true);
        //    }

        //}

        //if (currentReward < 14)
        //{
        //    listRewardDaily[1].btnSelect.interactable = false;
        //    listRewardDaily[1].vfx.SetActive(false);
        //}
        //else
        //{
        //    if (listRewardDaily[1].isCollected)
        //    {
        //        listRewardDaily[1].btnSelect.interactable = false;
        //        listRewardDaily[1].vfx.SetActive(false);
        //    }
        //    else
        //    {
        //        listRewardDaily[1].btnSelect.interactable = true;
        //        listRewardDaily[1].vfx.SetActive(true);
        //    }
        //}

        //if (currentReward < 28)
        //{
        //    listRewardDaily[2].btnSelect.interactable = false;
        //    listRewardDaily[2].vfx.SetActive(false);
        //}
        //else
        //{
        //    if (listRewardDaily[2].isCollected)
        //    {
        //        listRewardDaily[2].btnSelect.interactable = false;
        //        listRewardDaily[2].vfx.SetActive(false);
        //    }
        //    else
        //    {
        //        listRewardDaily[2].btnSelect.interactable = true;
        //        listRewardDaily[2].vfx.SetActive(true);
        //    }
        //}

        for (int i = 0; i < listRewardDaily.Count; i++)
        {
            if (currentReward < GetRequiredReward[i])
            {
                listRewardDaily[i].btnSelect.interactable = false;
                listRewardDaily[i].vfx.SetActive(false);
            }
            else
            {
                if (listRewardDaily[i].isCollected)
                {
                    listRewardDaily[i].btnSelect.interactable = false;
                    listRewardDaily[i].vfx.SetActive(false);
                }
                else
                {
                    listRewardDaily[i].btnSelect.interactable = true;
                    listRewardDaily[i].vfx.SetActive(true);
                }
            }
        }
    }
}
