using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardDaily : MonoBehaviour
{
    public int index;
    public int value;
    public bool isCollected;
    public Button btnSelect;
    public Button btnClaim;
    public GameObject vfx;
    public GameObject popup;
    private void Start()
    {
        btnSelect.onClick.AddListener(OpenReward);
    }
    private void Update()
    {
        vfx.transform.Rotate(new Vector3(0, 0, 1) * 100f * Time.deltaTime);
    }
    void OpenReward()
    {
        if (!isCollected)
        {
            vfx.SetActive(false);
            popup.SetActive(true);
        }
    }
    public void ClaimReward()
    {
        isCollected = true;
        btnSelect.interactable = false;
        PlayerPrefs.SetInt($"IsCollected{index}", 1);
        PlayerPrefs.Save();
    }

    public void SaveStateReward(int index)
    {
        if (PlayerPrefs.GetInt($"IsCollected{index}") == 1)
        {
            isCollected = true;
            vfx.SetActive(false);
            btnSelect.interactable = false;
        }
    }

    
}
