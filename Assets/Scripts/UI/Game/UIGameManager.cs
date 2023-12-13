using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class UIGameManager : MonoBehaviour
{
    public GameObject star;
    public GameObject level;
    public GameObject timer;
    public GameObject noAds;
    public GameObject pause;

    public TextMeshProUGUI txtStar;
    public TextMeshProUGUI txtLevel;

    public RectTransform timerSecond;
    public TextMeshProUGUI txtUseHint;
    public CanvasGroup txtUseTimerCG;

    public TextMeshProUGUI txtUseTimer;

    public void InitAnim()
    {
        AnimationPopup.instance.DoTween_Button(star, 0, 200, 1f);
        AnimationPopup.instance.DoTween_Button(level, 0, 200, 1f);
        AnimationPopup.instance.DoTween_Button(timer, 0, 200, 1f);
        AnimationPopup.instance.DoTween_Button(noAds, 0, 200, 1f);
        AnimationPopup.instance.DoTween_Button(pause, 0, 200, 1f);

        if (PlayerPrefs.GetInt("BoosterTimer") == 1 && PlayerPrefs.GetInt("NumTimer") > 0)
        {
            Vector3 pos = timerSecond.GetComponent<RectTransform>().anchoredPosition;
            txtUseTimer.gameObject.SetActive(true);
            AnimationPopup.instance.DoTween_Button(txtUseTimer.gameObject, 0, 0, 0.5f);
            txtUseTimerCG.DOFade(1f, 0.95f)
                .OnComplete(() =>
                {
                    txtUseTimer.GetComponent<RectTransform>().DOAnchorPos(pos, 1f);
                    txtUseTimerCG.DOFade(0f, 0.95f)
                    .OnComplete(() =>
                    {
                        txtUseTimer.gameObject.SetActive(false);
                    });
                });
        }
    }

    private void Start()
    {
        int star = DataUseInGame.gameData.star;
        txtStar.text = star.ToString();

        int indexLevel;
        if (!DataUseInGame.gameData.isDaily)
        {
            indexLevel = DataUseInGame.gameData.indexLevel + 1;
        }
        else
        {
            indexLevel = DataUseInGame.gameData.indexDailyLV + 1;
        }
        txtLevel.text = indexLevel.ToString();

        InitAnim();
    }

    

}
