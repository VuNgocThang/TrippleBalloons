using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardDaily : MonoBehaviour
{
    public int index;
    public Button btnSelect;
    public GameObject vfx;
    public bool isCollected;
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
            Debug.Log("index : " + index);
            isCollected = true;
            vfx.SetActive(false);
            popup.SetActive(true);
            btnSelect.interactable = false;
            PlayerPrefs.SetInt($"IsCollected{index}", 1);
            PlayerPrefs.Save();
        }
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
