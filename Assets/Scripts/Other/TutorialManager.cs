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

    private void Update()
    {
        if (PlayerPrefs.GetInt("IsInGame") == 1 && DataUseInGame.gameData.indexLevel == 0 && !DataUseInGame.gameData.isDaily)
        {
            if (handClick != null)
            {
                MoveHandClick();

            }
        }


        if (PlayerPrefs.GetInt("IsInGame") == 1 && DataUseInGame.gameData.indexLevel == 5)
        {
            ShowTutWrappedBB();
        }
    }
    public void ShowTutorial()
    {
        InitTutorial();
        //MoveHandClick();
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
        if (DataUseInGame.gameData.indexLevel == 0 && !DataUseInGame.gameData.isDaily)
        {
            StartCoroutine(WaitForMoveClick());
        }
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

        if (LogicGame.instance.listBB.Count > 6)
        {
            foreach (Bubble bb in LogicGame.instance.listBB)
            {
                bb.click = false;
                LogicGame.instance.listBB[0].click = true;
            }
        }
        else
        {
            foreach (Bubble bb in LogicGame.instance.listBB)
            {
                bb.click = true;
            }
        }
        if (handClick.gameObject.activeSelf)
        {
            handClick.transform.DOMove(Camera.main.WorldToScreenPoint(targetPos), 0.4f);
        }

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
        if (!DataUseInGame.gameData.isTutWrappedDone)
        {
            if (LogicGame.instance.listBBShuffle.Count > 0)
            {
                Vector3 pos = LogicGame.instance.listBBShuffle[0].transform.position;
                handClick.transform.DOMove(Camera.main.WorldToScreenPoint(pos), 0.4f);
            }
        }
    }

    public void HideHandWrapped()
    {
        handClick.gameObject.SetActive(false);
        txtWrappedBB.gameObject.SetActive(false);
        DataUseInGame.gameData.isTutWrappedDone = true;
        DataUseInGame.instance.SaveData();
    }
}
