using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoseManager : MonoBehaviour
{
    public GameObject loseUI;
    public GameObject bg;
    public Timer timer;

    [Header("Time Up")]
    public GameObject panelTimeUp;
    public Button btnContinueTimeUp;
    public Button btnGiveUpTimeUp;
    public CanvasGroup panelTimeUpCG;

    [Header("Out of Move")]
    public GameObject panelOutOfMove;
    public Button btnContinueOutOfMove;
    public Button btnGiveUpOutOfMove;
    public CanvasGroup panelOutOfMoveCG;


    [Header("Persident")]
    public GameObject panelPersident;
    public Button btnRetry;
    public Button btnHome;
    public CanvasGroup panelPersidentCG;

    private void Start()
    {
        btnContinueTimeUp.onClick.AddListener(ContinueTimeUp);
        btnGiveUpTimeUp.onClick.AddListener(OpenPanelPersident);

        btnContinueOutOfMove.onClick.AddListener(ContinueOutOfMove);
        btnGiveUpOutOfMove.onClick.AddListener(OpenPanelPersident);

        btnRetry.onClick.AddListener(Retry);
        btnHome.onClick.AddListener(BackHome);
    }

    // Logic TimeUp
    public void OpenPanelTimeUp()
    {
        AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.fail);
        panelTimeUp.SetActive(true);
        AnimationPopup.instance.DoTween_Button(panelTimeUpCG.gameObject, 0, 200, 0.5f);
        panelTimeUpCG.DOFade(1f, 0.5f);
    }
    public void ContinueTimeUp()
    {
        AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.clickMenu);

        //if (DataUseInGame.gameData.gold >= 200)
        //{
        btnContinueTimeUp.interactable = false;
        btnGiveUpTimeUp.interactable = false;

        AnimationPopup.instance.FadeWhileMoveUp(panelTimeUpCG.gameObject, 0.5f);
        panelTimeUpCG.DOFade(0f, 0.5f)
            .OnComplete(() =>
            {
                btnContinueTimeUp.interactable = true;
                btnGiveUpTimeUp.interactable = true;

                panelTimeUp.SetActive(false);
                loseUI.gameObject.SetActive(false);
            });
        //GameManager.Instance.SubGold(200);
        timer.timeLeft += 60f;

        timer.OnGUI();
        timer.timeOut = false;
        LogicGame.instance.checkLose = false;
        StartCoroutine(LogicGame.instance.CanClickAgain());
        //}
        //else
        //{
        //    Debug.Log("Not Enough Gold");
        //}
    }

    //Logic OutOfMove
    public void OpenPanelOutOfMove()
    {
        AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.fail);
        panelOutOfMove.SetActive(true);
        panelOutOfMoveCG.DOFade(1f, 0.5f);
        AnimationPopup.instance.DoTween_Button(panelOutOfMoveCG.gameObject, 0, 200, 0.55f);
    }
    public void ContinueOutOfMove()
    {
        AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.clickMenu);

        //if (DataUseInGame.gameData.gold >= 200)
        //{
        btnContinueOutOfMove.interactable = false;
        btnGiveUpOutOfMove.interactable = false;
        AnimationPopup.instance.FadeWhileMoveUp(panelOutOfMoveCG.gameObject, 0.5f);
        panelOutOfMoveCG.DOFade(0f, 0.5f)
            .OnComplete(() =>
            {
                btnContinueOutOfMove.interactable = true;
                btnGiveUpOutOfMove.interactable = true;

                panelOutOfMove.SetActive(false);
                loseUI.gameObject.SetActive(false);
            });

        //GameManager.Instance.SubGold(200);
        LogicGame.instance.canClick = false;
        LogicGame.instance.checkLose = false;
        timer.stopTimer = true;
        timer.timeOut = false;
        LogicGame.instance.UndoAll();
        //}
        //else
        //{
        //    Debug.Log("Not Enough Gold");
        //}
    }

    //Logic Persident
    public void OpenPanelPersident()
    {
        AnimationPopup.instance.FadeWhileMoveUp(panelOutOfMoveCG.gameObject, 0.5f);
        panelOutOfMoveCG.DOFade(0f, 0.5f)
            .OnComplete(() =>
            {
                panelOutOfMove.SetActive(false);
            });

        AnimationPopup.instance.FadeWhileMoveUp(panelTimeUpCG.gameObject, 0.5f);
        panelTimeUpCG.DOFade(0f, 0.5f)
               .OnComplete(() =>
               {
                   panelTimeUp.SetActive(false);
               });

        panelPersident.SetActive(true);
        AnimationPopup.instance.DoTween_Button(panelPersidentCG.gameObject, 0, 200, 0.5f);
        panelPersidentCG.DOFade(1f, 0.5f);
    }
    public void Retry()
    {
        btnRetry.interactable = false;
        btnHome.interactable = false;
        btnRetry.gameObject.SetActive(false);
        btnHome.gameObject.SetActive(false);
        Debug.Log("subHeart" + DataUseInGame.gameData.heart);

        AnimationPopup.instance.FadeWhileMoveUp(panelPersidentCG.gameObject, 0.5f);
        panelPersidentCG.DOFade(0f, 0.5f);
        bg.SetActive(false);
        StartCoroutine(LogicGame.instance.AnimBoomBB("SceneGame"));
        GameManager.Instance.SubHeart();
        PlayerPrefs.SetInt("IsInGame", 1);
        PlayerPrefs.Save();
    }
    public void BackHome()
    {
        btnRetry.interactable = false;
        btnHome.interactable = false;
        btnRetry.gameObject.SetActive(false);
        btnHome.gameObject.SetActive(false);
        Debug.Log("subHeart" + DataUseInGame.gameData.heart);

        AnimationPopup.instance.FadeWhileMoveUp(panelPersidentCG.gameObject, 0.5f);
        panelPersidentCG.DOFade(0f, 0.5f);
        PlayerPrefs.SetInt("IsInGame", 0);
        PlayerPrefs.Save();
        DataUseInGame.gameData.isDaily = false;
        DataUseInGame.instance.SaveData();
        GameManager.Instance.SubHeart();
        StartCoroutine(LogicGame.instance.AnimBoomBB("SceneGame"));
        bg.SetActive(false);
    }



}
