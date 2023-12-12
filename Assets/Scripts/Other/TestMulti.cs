using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestMulti : MonoBehaviour
{
    public TextMeshProUGUI rewardToShow;
    public Transform hand;
    public Vector3 startPos;
    public Vector3 endPos;
    public Button btnStop;

    private void Start()
    {
        Move();
        btnStop.onClick.AddListener(StopMoveHand);
    }
   
    void Move()
    {
        hand.DOLocalMove(endPos, 0.75f)
            .SetEase(Ease.InOutCubic)
            .OnComplete(() =>
            {
                hand.DOLocalMove(startPos, 0.75f)
                .SetEase(Ease.InOutCubic)
                .OnComplete(Move);
            });
    }
    void StopMoveHand()
    {
        DOTween.Kill(hand);
        MultiResult(hand.GetComponent<RectTransform>());
        Debug.Log(MultiResult(hand.GetComponent<RectTransform>()));
    }

    int MultiResult(RectTransform hand)
    {
        int multi;
        float x = Mathf.Abs(hand.anchoredPosition.x);
        if (x < 22f)
        {
            multi = 5;
        }
        else if (x < 82f)
        {
            multi = 4;
        }
        else if (x < 147f)
        {
            multi = 3;
        }
        else
        {
            multi = 2;
        }
        return multi;
    }

   
}
