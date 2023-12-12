using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HomeUI : MonoBehaviour
{
    [SerializeField] GameObject heartUI;
    [SerializeField] GameObject starUI;
    [SerializeField] GameObject goldUI;
    [SerializeField] GameObject settingUI;
    [SerializeField] GameObject removeAdsUI;
    [SerializeField] GameObject starCollectorUI;
    [SerializeField] GameObject playUI;
    [SerializeField] GameObject shopUI;
    [SerializeField] GameObject homeUI;
    [SerializeField] GameObject dailyUI;

    public int star;
    public TextMeshProUGUI txtStar;

    public int gold;
    public TextMeshProUGUI txtGold;

   

    public void InitTest()
    {
        AnimationPopup.instance.DoTween_Button(heartUI, 0, 300, 1f);
        AnimationPopup.instance.DoTween_Button(starUI, 0, 300, 1f);
        AnimationPopup.instance.DoTween_Button(goldUI, 0, 300, 1f);
        AnimationPopup.instance.DoTween_Button(settingUI, 0, 300, 1f);

        AnimationPopup.instance.DoTween_Button(removeAdsUI, -300, 0, 1f);
        AnimationPopup.instance.DoTween_Button(starCollectorUI, -300, 0, 1f);

        AnimationPopup.instance.DoTween_Button(shopUI, 0, -300, 1f);
        AnimationPopup.instance.DoTween_Button(homeUI, 0, -300, 1f);
        AnimationPopup.instance.DoTween_Button(dailyUI, 0, -300, 1f);
    }

    private void OnGUI()
    {
        star = DataUseInGame.gameData.star;
        gold = DataUseInGame.gameData.gold;

        txtStar.text = star.ToString();
        txtGold.text = gold.ToString();
    }
}
