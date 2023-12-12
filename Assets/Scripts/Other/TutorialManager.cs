using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public Image handClick;
    public Image handRotate;
    public GameObject fingerRotate;
    public GameObject imgRotate;
    public int currentStepClick = 0;
    public bool isDoneClick;
    public bool isDone;
    public List<Bubble> listBubbles = new List<Bubble>();
    public List<int> listIndex = new List<int>()
    {
        0,0,0,2,4,4,2,4,2
    };

    public GameObject txtWrappedBB;
    private void Start()
    {
        isDoneClick = false;
        isDone = false;
    }
    public void ShowTutorial()
    {
        InitTutorial();
        MoveHandClick();
    }
    void InitTutorial()
    {
        foreach (Bubble bb in LogicGame.instance.listBB)
        {
            listBubbles.Add(bb);
        }
    }
    void MoveHandClick()
    {
        StartCoroutine(WaitForMoveClick());
    }
    public void OnClick(Bubble bb)
    {
        if (bb.gameObject.transform.position == listBubbles[currentStepClick].transform.position)
        {
            currentStepClick++;
            if (currentStepClick < 3)
            {
                MoveHandClick();
            }
            else
            {
                StartCoroutine(HideHandClick());

            }
        }
    }
    IEnumerator WaitForMoveClick()
    {
        yield return new WaitForSeconds(0.5f);
        Vector3 targetPos = listBubbles[currentStepClick].transform.position;

        foreach (Bubble bb in LogicGame.instance.listBB)
        {
            bb.click = false;
            LogicGame.instance.listBB[0].click = true;
        }
        //shader.transform.position = targetPos;
        handClick.transform.DOMove(Camera.main.WorldToScreenPoint(targetPos), 0.4f);
    }
    IEnumerator HideHandClick()
    {
        handClick.gameObject.SetActive(false);
        foreach (Bubble bb in LogicGame.instance.listBB)
        {
            bb.click = true;
        }
        yield return new WaitForSeconds(0.5f);
        if (!isDoneClick)
        {
            isDoneClick = true;
            handRotate.gameObject.SetActive(true);
            imgRotate.SetActive(true);
            fingerRotate.SetActive(true);
            GameManager.Instance.canRotate = true;
        }
    }
    public void AnimHandRotate()
    {
        if (handRotate.gameObject != null && !isDone)
        {
            isDone = true;
            handRotate.gameObject.SetActive(false);
            imgRotate.SetActive(false);
            fingerRotate.SetActive(false);
        }
    }
    public void AnimHand()
    {
        if (handClick.gameObject != null)
        {
            handClick.transform.DOScale(new Vector3(1.3f, 1.3f, 1.3f), 1f)
                .OnComplete(() =>
                {
                    handClick.transform.DOScale(new Vector3(1f, 1f, 1f), 1f)
                    .OnComplete(() =>
                    {
                        AnimHand();
                    });
                });
        }
    }
    public void ShowTutWrappedBB()
    {
        if (DataUseInGame.gameData.indexLevel == 5 && !DataUseInGame.gameData.isDaily && !DataUseInGame.gameData.isTutWrappedDone)
        {
            handClick.gameObject.SetActive(true);
            txtWrappedBB.gameObject.SetActive(true);
            StartCoroutine(MoveHand());
        }
    }

    IEnumerator MoveHand()
    {
        yield return new WaitForSeconds(0.5f);
        Vector3 pos = LogicGame.instance.listBBShuffle[0].transform.position;
        handClick.transform.DOMove(Camera.main.WorldToScreenPoint(pos), 0.4f);
    }

    public void HideHandWrapped()
    {
        handClick.gameObject.SetActive(false);
        txtWrappedBB.gameObject.SetActive(false);
        DataUseInGame.gameData.isTutWrappedDone = true;
        DataUseInGame.instance.SaveData();
    }
}
