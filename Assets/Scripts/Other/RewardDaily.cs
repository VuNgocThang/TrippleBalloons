using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RewardDaily : MonoBehaviour
{
    public int index;
    public int value;
    public bool isCollected;
    public Button btnSelect;
    public Button btnClaimDaily;
    public GameObject vfx;
    public GameObject popup;
    public CanvasGroup popupCG;
    private void Start()
    {
        btnSelect.onClick.AddListener(OpenReward);
        btnClaimDaily.onClick.AddListener(ClaimReward);
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
        GameManager.Instance.IncreaseItemBig(value);
        isCollected = true;
        btnSelect.interactable = false;
        PlayerPrefs.SetInt($"IsCollected{index}", 1);
        PlayerPrefs.Save();
        AnimationPopup.instance.FadeWhileMoveUp(popupCG.gameObject, 0.5f);
        popupCG.DOFade(0f, 0.5f)
            .OnComplete(() =>
            {
                popup.SetActive(false);
            });
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
