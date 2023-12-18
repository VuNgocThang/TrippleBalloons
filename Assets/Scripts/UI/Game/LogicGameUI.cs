using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogicGameUI : MonoBehaviour
{
    [SerializeField] Timer timer;

    [Header("Pause")]
    [SerializeField] Button btnHome;
    public Button btnPause;
    [SerializeField] Button btnResume;
    [SerializeField] Button btnClosePanelPause;
    [SerializeField] Button btnReplay;
    [SerializeField] GameObject panelPause;
    [SerializeField] CanvasGroup panelPauseCG;

    [Header("Persident")]
    public Button btnClosePersident;
    public Button btnRetry;
    public Button btnHomePersident;
    public GameObject bgBlackPersident;
    public GameObject panelPersident;
    public CanvasGroup panelPersidentCG;

    [Header("NoAds")]
    [SerializeField] Button btnRemoveAds;
    [SerializeField] Button btnClosePanelRemoveAds;
    [SerializeField] GameObject panelRemoveAds;
    [SerializeField] CanvasGroup panelRemoveAdsCG;

    [Header("LoseManager")]
    public LoseManager loseUI;

    [Header("WinUI")]
    [SerializeField] WinUI winUI;
    [SerializeField] GameObject newTile;
    public GameObject bgBlackWinUI;
    [SerializeField] GameObject panel;
    [SerializeField] Canvas canvas;
    [SerializeField] Camera camerUI;
    [SerializeField] Button btnClaim;
    [SerializeField] CanvasGroup winUICG;
    [SerializeField] GameObject particleWin;
    public Button btnClaimStar;
    public Button btnClaimStarNoAds;

    [Header("ButtonInGame")]
    public TutorialButtonInGame tutBtn;
    public ControllerIsInGame controller;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
    }

    private void Start()
    {

        btnHome.onClick.AddListener(BackHome);
        btnPause.onClick.AddListener(OpenPanelPause);
        btnResume.onClick.AddListener(ClosePanelPause);
        btnClosePanelPause.onClick.AddListener(ClosePanelPause);
        btnReplay.onClick.AddListener(ReplayGame);

        btnClosePersident.onClick.AddListener(ClosePersident);
        //btnRetry.onClick.AddListener(Retry);
        //btnHomePersident.onClick.AddListener(BackHomePersident);

        btnRemoveAds.onClick.AddListener(OpenPanelRemoveAds);
        btnClosePanelRemoveAds.onClick.AddListener(ClosePanelRemoveAds);

        btnClaim.onClick.AddListener(CloseWinUI);
        btnClaimStar.onClick.AddListener(ClaimStar);
        btnClaimStarNoAds.onClick.AddListener(ClaimStarNoAds);
    }

    void BackHome()
    {
        AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.clickMenu);
        DataUseInGame.gameData.isDaily = false;
        DataUseInGame.instance.SaveData();

        DOTween.KillAll();
        PlayerPrefs.SetInt("IsInGame", 0);
        PlayerPrefs.Save();
        SceneManager.LoadScene("SceneGame");
    }
    void OpenPanelPause()
    {
        AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.clickMenu);
        timer.stopTimer = true;
        panelPause.SetActive(true);

        //if (DataUseInGame.gameData.heart <= 0)
        //{
        //    btnRetry.interactable = false;
        //}
        //else
        //{
        //    btnRetry.interactable = true;
        //}

        AnimationPopup.instance.DoTween_Button(panelPauseCG.gameObject, 0, 200, 0.5f);
        panelPauseCG.DOFade(1f, 0.5f);
    }

    void ClosePanelPause()
    {
        AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.clickMenu);
        timer.stopTimer = false;
        AnimationPopup.instance.FadeWhileMoveUp(panelPauseCG.gameObject, 0.5f);
        panelPauseCG.DOFade(0f, 0.5f)
            .OnStart(() =>
            {
                LogicGame.instance.canClick = false;
            })
            .OnComplete(() =>
            {
                LogicGame.instance.canClick = true;
                panelPause.SetActive(false);
            });
    }
    void ReplayGame()
    {
        OpenPanelPersident();
    }
    public void OpenPanelPersident()
    {
        AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.clickMenu);
        bgBlackPersident.SetActive(true);
        btnClosePersident.gameObject.SetActive(true);
        LogicGame.instance.canClick = false;

        AnimationPopup.instance.FadeWhileMoveUp(panelPauseCG.gameObject, 0.5f);
        panelPauseCG.DOFade(0f, 0.5f)
            .OnComplete(() =>
            {
                panelPause.SetActive(false);
            });

        panelPersident.SetActive(true);
        AnimationPopup.instance.DoTween_Button(panelPersidentCG.gameObject, 0, 200, 0.5f);
        panelPersidentCG.DOFade(1f, 0.5f);
    }
    public void ClosePersident()
    {
        AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.clickMenu);
        AnimationPopup.instance.FadeWhileMoveUp(panelPersidentCG.gameObject, 0.5f);
        panelPersidentCG.DOFade(0f, 0.5f)
            .OnComplete(() =>
            {
                bgBlackPersident.SetActive(false);
                panelPersident.SetActive(false);
                StartCoroutine(CanClickAgain());
            });
    }
    //public void Retry()
    //{
    //    AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.clickMenu);
    //    GameManager.Instance.SubHeart();
    //    btnRetry.interactable = false;
    //    btnHome.interactable = false;
    //    AnimationPopup.instance.FadeWhileMoveUp(panelPersidentCG.gameObject, 0.5f);
    //    panelPersidentCG.DOFade(0f, 0.5f);

    //    StartCoroutine(LogicGame.instance.AnimBoomBB("SceneGame"));
    //}
    //public void BackHomePersident()
    //{
    //    AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.clickMenu);
    //    GameManager.Instance.SubHeart();
    //    btnRetry.interactable = false;
    //    btnHome.interactable = false;
    //    AnimationPopup.instance.FadeWhileMoveUp(panelPersidentCG.gameObject, 0.5f);
    //    panelPersidentCG.DOFade(0f, 0.5f)
    //        .OnComplete(() =>
    //        {
    //            DataUseInGame.gameData.isDaily = false;
    //            DataUseInGame.instance.SaveData();
    //        });
    //    PlayerPrefs.SetInt("IsInGame", 0);
    //    PlayerPrefs.Save();
    //    StartCoroutine(LogicGame.instance.AnimBoomBB("SceneGame"));
    //}

    void OpenPanelRemoveAds()
    {
        AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.clickMenu);
        timer.stopTimer = true;
        LogicGame.instance.canClick = false;
        panelRemoveAds.SetActive(true);
        AnimationPopup.instance.DoTween_Button(panelRemoveAdsCG.gameObject, 0, 200, 0.5f);
        panelRemoveAdsCG.DOFade(1f, 0.5f);
    }
    void ClosePanelRemoveAds()
    {
        AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.clickMenu);
        AnimationPopup.instance.FadeWhileMoveUp(panelRemoveAdsCG.gameObject, 0.5f);
        panelRemoveAdsCG.DOFade(0f, 0.5f)
            .OnComplete(() =>
            {
                panelRemoveAds.gameObject.SetActive(false);
                StartCoroutine(CanClickAgain());
            });
    }

    public void OpenLoseUI()
    {
        loseUI.gameObject.SetActive(true);
    }
    public void OpenWinUI()
    {
        LogicGame.instance.pathCreaterGift.position = LogicGame.instance.defaultPosGift;
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        camerUI.gameObject.SetActive(true);
        panel.SetActive(true);

        bgBlackWinUI.SetActive(true);
        winUI.gameObject.SetActive(true);
        //winUI.InitWinUIStart();
        particleWin.SetActive(true);
        AnimationPopup.instance.DoTween_Button(winUICG.gameObject, 0, 200, 0.5f);
        winUICG.DOFade(1f, 0.5f);
    }

    public void ClaimStar()
    {
        btnClaimStarNoAds.interactable = false;
        AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.clickMenu);
        GameManager.Instance.AddStar(winUI.Multi());
        DOTween.KillAll();
        StartCoroutine(LoadSceneHome());

    }

    public void ClaimStarNoAds()
    {
        btnClaimStar.interactable = false;
        btnClaimStarNoAds.interactable = false;
        AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.clickMenu);
        GameManager.Instance.AddStar(1);
        DOTween.KillAll();
        StartCoroutine(LoadSceneHome());
    }

    public void CloseWinUI()
    {
        AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.clickMenu);
        ChangeRenderMode();
        PlayerPrefs.SetInt("IsInGame", 0);
        PlayerPrefs.Save();
        SceneManager.LoadScene("SceneGame");
    }

    private void ChangeRenderMode()
    {
        winUI.gameObject.SetActive(false);
        panel.SetActive(false);
        camerUI.gameObject.SetActive(false);
        bgBlackWinUI.gameObject.SetActive(false);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
    }

    IEnumerator CanClickAgain()
    {
        yield return new WaitForSeconds(0.2f);
        timer.stopTimer = false;
        LogicGame.instance.canClick = true;
    }
    List<int> listIndexNewTiles = new List<int>()
    {
        4,9,14,19,24,29
    };
    IEnumerator LoadSceneHome()
    {
        yield return new WaitForSeconds(3f);
        AnimationPopup.instance.FadeWhileMoveUp(winUICG.gameObject, 0.5f);
        winUICG.DOFade(0f, 0.5f)
            .OnComplete(() =>
            {
                winUI.gameObject.SetActive(false);
                if (!DataUseInGame.gameData.isDaily)
                {
                    if (listIndexNewTiles.Contains(DataUseInGame.gameData.indexLevel))
                    {
                        newTile.SetActive(true);
                        int index = DataUseInGame.gameData.indexLevel;
                        if (index < LogicGame.instance.listLevel.Count - 1)
                        {
                            index++;
                        }
                        DataUseInGame.gameData.indexLevel = index;
                        DataUseInGame.instance.SaveData();
                    }
                    else
                    {
                        int index = DataUseInGame.gameData.indexLevel;
                        if (index < LogicGame.instance.listLevel.Count - 1)
                        {
                            index++;
                        }
                        DataUseInGame.gameData.indexLevel = index;
                        DataUseInGame.instance.SaveData();

                        //ChangeRenderMode();
                        //controller.UpdateStateIsInGame();
                        //LogicGame.instance.Instantiate();
                        //tutBtn.InitTutBtnInGame();

                        SceneManager.LoadScene("SceneGame");
                    }
                }
                else
                {
                    DataUseInGame.gameData.isDaily = false;

                    //if (DataUseInGame.gameData.currentRewardDaily < DataUseInGame.gameData.maxRewardDaily)
                    //{
                    DataUseInGame.gameData.currentRewardDaily++;
                    //}

                    DataUseInGame.gameData.dailyData.Add(new DailyData()
                    {
                        year = 2023,
                        month = DataUseInGame.gameData.month,
                        day = DataUseInGame.gameData.day,
                    });
                    DataUseInGame.instance.SaveData();

                    PlayerPrefs.SetInt("IsInGame", 0);
                    PlayerPrefs.Save();
                    SceneManager.LoadScene("SceneGame");
                }

            });
    }

    private void Update()
    {
        Debug.Log(DataUseInGame.gameData.heart);
        if (DataUseInGame.gameData.heart <= 0)
        {
            btnRetry.interactable = false;
        }
        else
        {
            btnRetry.interactable = true;
        }
    }
}
