using UnityEngine;
using System;
using System.Collections.Generic;

public class TutorialButtonInGame : MonoBehaviour
{
    public List<GameObject> listItems;
    public List<GameObject> listHands;
    public List<GameObject> listTexts;

    public GameObject ImageHint;

    private void Start()
    {
        InitTutBtnInGame();
    }

    public void InitTutBtnInGame()
    {
        TutHintBtn();
        TutUndo();
        TutShuffle();
        TutFreeze();
    }

    public void Arrange(int index)
    {
        ImageHint.SetActive(true);
        listHands[index].SetActive(true);
        listTexts[index].SetActive(true);
        var temp = listItems[index];
        temp.transform.SetSiblingIndex(5);
    }
    public void TutHintBtn()
    {
        if (DataUseInGame.gameData.indexLevel == 1 && !DataUseInGame.gameData.isDaily && !DataUseInGame.gameData.isTutHintDone)
        {
            Arrange(0);
        }
    }

    public void TutUndo()
    {
        if (DataUseInGame.gameData.indexLevel == 2 && !DataUseInGame.gameData.isDaily && !DataUseInGame.gameData.isTutUndoDone)
        {
            LogicGame.instance.isInTut = true;
            Arrange(1);
        }
    }

    public void TutTrippleUndo()
    {
        var temp = listItems[1];
        temp.transform.SetSiblingIndex(1);

        Arrange(2);
    }

    public void TutShuffle()
    {
        if (DataUseInGame.gameData.indexLevel == 3 && !DataUseInGame.gameData.isDaily && !DataUseInGame.gameData.isTutShuffleDone)
        {
            Arrange(3);
        }
    }

    public void TutFreeze()
    {
        if (DataUseInGame.gameData.indexLevel == 4 && !DataUseInGame.gameData.isDaily && !DataUseInGame.gameData.isTutFreezeDone)
        {
            Arrange(4);
        }
        
    }


}
