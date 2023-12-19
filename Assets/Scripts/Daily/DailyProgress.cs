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
        if (PlayerPrefs.HasKey(GameSave.REWARD_DAILY))
        {
            reward = PlayerPrefs.GetFloat(GameSave.REWARD_DAILY);
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
        PlayerPrefs.SetFloat(GameSave.REWARD_DAILY, reward);
    }

    void UpdateReward()
    {
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
