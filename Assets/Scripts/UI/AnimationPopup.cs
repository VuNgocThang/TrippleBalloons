using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class AnimationPopup : MonoBehaviour
{
    public static AnimationPopup instance;
    public AnimationCurve curveZoomIn;
    public AnimationCurve curveZoomOut;
    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void AnimScaleZoom(Transform trans)
    {
        trans.DOScale(new Vector3(1f, 1f, 1f), 1f)
            .SetEase(curveZoomIn);
    }

    public void AnimScaleZero(GameObject panel, Transform trans)
    {
        trans.DOScale(new Vector3(0f, 0f, 0f), 0.5f)
            .SetEase(curveZoomOut)
            .OnComplete(() =>
            {
                if (panel != null)
                {
                    panel.gameObject.SetActive(false);
                }
            });
    }

    public void DoTween_Button(GameObject Obj, int posX, int posY, float timer)
    {
        if (Obj != null)
        {
            Vector3 pos = Obj.GetComponent<RectTransform>().anchoredPosition;
            Obj.GetComponent<RectTransform>().anchoredPosition += new Vector2(posX, posY);
            Obj.GetComponent<RectTransform>().DOAnchorPos(pos, timer);
        }

    }
    public void FadeWhileMoveUp(GameObject obj, float timer)
    {
        var rectObj = obj.GetComponent<RectTransform>();
        Vector3 pos = rectObj.anchoredPosition;
        Vector3 newPos = pos + new Vector3(0, 200f, 0);
        rectObj.DOAnchorPos(newPos, timer).OnComplete(() =>
        {
            rectObj.anchoredPosition = pos;
        });
    }

    public void MoveAndActiveFalse(GameObject Obj, int posX, int posY, float timer)
    {
        Vector3 pos = Obj.GetComponent<RectTransform>().anchoredPosition;
        Obj.GetComponent<RectTransform>().anchoredPosition += new Vector2(posX, posY);
        Obj.GetComponent<RectTransform>().DOAnchorPos(pos, timer)
            .OnComplete(() =>
            {
                Obj.SetActive(false);
            });
    }


    //void OpenPanelSetting()
    //{
    //    panelSetting.SetActive(true);
    //    AnimationPopup.instance.DoTween_Button(panelSettingCG.gameObject, 0, 200, 0.5f);
    //    panelSettingCG.DOFade(1f, 0.5f);

    //}
    //void ClosePanelSetting()
    //{
    //    AnimationPopup.instance.FadeWhileMoveUp(panelSettingCG.gameObject, 0.5f);
    //    panelSettingCG.DOFade(0f, 0.5f)
    //        .OnComplete(() =>
    //        {
    //            panelSetting.SetActive(false);
    //        });
    //}


}
