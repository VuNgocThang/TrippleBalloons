using DG.Tweening;
using PathCreation;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogicGame : MonoBehaviour
{
    public static LogicGame instance;
    public LogicGameUI logicUI;
    public UIGameManager uiGame;
    public LayerMask layerMask;
    public List<Bubble> listBB;
    public List<int> listIndex;
    public List<Bubble> listGOStored = new List<Bubble>();
    public List<Bubble> listBBShuffle;
    public List<Transform> listPoint = new List<Transform>();
    public List<Bubble> listBubbleUndo = new List<Bubble>();
    public List<LevelSetMap> listLevel;
    public List<LevelSetMap> listLevelDaily;
    public List<int> listIndexParent = new List<int>()
    {
        0,1,2,3,4,5,6,7,8
    };
    public ComboSystem comboSystem;
    public LineController lineController;
    public Timer timer;
    public LevelSetMap level;
    public PathCreator pathCreater;
    public TextMeshProUGUI txtCombo;
    public CameraResize cameraResize;
    public TutorialManager tutorialManager;
    public ButtonController buttonController;
    public ControllerIsInGame controller;

    public Transform targetHT;
    public Transform pathCreaterGift;
    public Transform pathHandRotate;
    public GameObject particleTest;
    public Vector3 defaultPosGift;
    public int indexLevel;
    public int count;
    int currentTotalBB;
    int winStreak;
    public bool canClick;
    public bool isDrag;
    public bool canShuffle;
    public bool checkLose;
    public bool checkWin;
    public bool canEat;
    public bool isInTut;
    public bool isUseBooster;

    private void Awake()
    {
        if (instance == null) instance = this;
        defaultPosGift = pathCreaterGift.position;
        cameraResize.InitSizeObject();
    }
    void Start()
    {
        Instantiate();

    }
   

    public void Instantiate()
    {
        checkWin = false;
        if (!DataUseInGame.gameData.isDaily)
        {
            indexLevel = DataUseInGame.gameData.indexLevel;
            level = Instantiate(listLevel[indexLevel], transform);
        }
        else
        {
            if (level == null)
            {
                int indexLevelText = DataUseInGame.gameData.indexLevel;
                level = Instantiate(listLevel[indexLevelText], transform);
            }
            level.gameObject.SetActive(false);

            indexLevel = DataUseInGame.gameData.indexDailyLV;
            level = Instantiate(listLevelDaily[indexLevel], transform);

        }
        InitBubbles();

        if (!DataUseInGame.gameData.isDaily)
        {
            controller.UpdateStateIsInGame();
        }
        else
        {
            controller.PanelInGame();
        }

        if (PlayerPrefs.GetInt("IsInGame") == 1)
        {
            InitAll();
        }
    }

    public void InitAll()
    {
        Application.targetFrameRate = 60;
        if (!DataUseInGame.gameData.isTutHintDone && DataUseInGame.gameData.indexLevel == 1
            ||
            !DataUseInGame.gameData.isTutUndoDone && DataUseInGame.gameData.indexLevel == 2
            )
        {
            canClick = false;
        }

        if (!PlayerPrefs.HasKey("WinStreak"))
        {
            winStreak = 0;
            PlayerPrefs.SetInt("WinStreak", winStreak);
            PlayerPrefs.Save();
        }
        else
        {
            winStreak = PlayerPrefs.GetInt("WinStreak");
        }

        count = 0;

        if (DataUseInGame.gameData.indexLevel > 0 || DataUseInGame.gameData.isDaily)
        {
            GameManager.Instance.canRotate = true;
        }
        lineController.CreateLine(listBBShuffle);
        currentTotalBB = listBB.Count;
        canShuffle = true;

        //số bóng *3 + 30 giây
        timer.timeLeft = currentTotalBB * 3 + 30f;

        if (!DataUseInGame.gameData.isTutHintDone && DataUseInGame.gameData.indexLevel == 1
            ||
            !DataUseInGame.gameData.isTutUndoDone && DataUseInGame.gameData.indexLevel == 2
            ||
            !DataUseInGame.gameData.isTutShuffleDone && DataUseInGame.gameData.indexLevel == 3
            ||
            !DataUseInGame.gameData.isTutFreezeDone && DataUseInGame.gameData.indexLevel == 4
            )
        {
            timer.stopTimer = true;
        }

        if (!DataUseInGame.gameData.isTutUndoDone)
        {
            InitBBForTut();
        }
        buttonController.InitButton();
    }

    private void InitBBForTut()
    {
        int count = 4;
        List<int> selectedIDs = new List<int>();

        if (DataUseInGame.gameData.indexLevel == 2 && !DataUseInGame.gameData.isTutUndoDone)
        {
            while (count > 0)
            {
                int a;
                do
                {
                    a = UnityEngine.Random.Range(0, listBB.Count);
                } while (selectedIDs.Contains(listBB[a].ID));

                selectedIDs.Add(listBB[a].ID);
                listBB[a].originalPos = listBB[a].transform.position;
                Move(listBB[a]);
                count--;
            }

            selectedIDs.Clear();
        }


    }

    void InitBubbles()
    {
        if (DataUseInGame.gameData.isDaily)
        {
            listIndex.Clear();
            listBB.Clear();
            listBBShuffle.Clear();

            foreach (int item in DataUseInGame.gameData.listIndexDaily)
            {
                if (!listIndex.Contains(item))
                {
                    listIndex.Add(item);
                }
            }
        }
        else
        {
            foreach (int item in DataUseInGame.gameData.listIndex)
            {
                if (!listIndex.Contains(item))
                {
                    listIndex.Add(item);
                }
            }
        }

        int count = listIndex.Count;
        int countAll = level.bubbles.Count;
        int max = level.maxEach;
        int countParent = 0;
        int numCountParent;

        for (int i = 0; i < level.bubbles.Count; i++)
        {
            if (level.bubbles[i].children.childCount > 0)
            {
                level.bubbles[i].hasChildren = true;
                countParent++;
            }
        }
        numCountParent = countParent;

        List<int> listRandom = new List<int>();
        int[] arr = new int[count];
        while (countAll > 0)
        {
            int i;
            int index;

            if (countParent > 0)
            {
                i = UnityEngine.Random.Range(0, 8);
                countParent--;

                index = listIndexParent[i];

                if (arr[i] < max)
                {
                    arr[i] += 3;
                    for (int y = 0; y < 3; y++)
                    {
                        listRandom.Add(index);
                        countAll--;
                    }
                }
            }
            else
            {
                i = UnityEngine.Random.Range(0, count);
                index = listIndex[i];

                if (arr[i] < max)
                {
                    arr[i] += 3;
                    for (int y = 0; y < 3; y++)
                    {
                        listRandom.Add(index);
                        countAll--;
                    }
                }
            }


        }

        List<int> listRandomParent = new List<int>();
        int c = numCountParent;
        for (int i = 0; i < numCountParent; i++)
        {
            int j = UnityEngine.Random.Range(0, c);
            listRandomParent.Add(listRandom[j]);
            listRandom.RemoveAt(j);
            c--;
        }

        List<int> list = new List<int>();
        int r;
        while (listRandom.Count > 0)
        {
            r = UnityEngine.Random.Range(0, listRandom.Count);
            list.Add(listRandom[r]);
            listRandom.RemoveAt(r);
        }
        listRandom.AddRange(list);

        List<Bubble> listBBInit = new List<Bubble>();

        for (int i = 0; i < level.bubbles.Count; i++)
        {
            listBBInit.Add(level.bubbles[i]);
        }
        int countBB = level.bubbles.Count;
        List<Bubble> tempB = new List<Bubble>();
        List<Bubble> bubbleParent = new List<Bubble>();

        for (int i = 0; i < countBB; i++)
        {
            tempB.Add(listBBInit[i]);
        }

        for (int i = 0; i < countBB; i++)
        {
            if (tempB[i].hasChildren)
            {
                bubbleParent.Add(tempB[i]);
                listBBInit.Remove(tempB[i]);
            }
        }

        //Tutorial
        if (DataUseInGame.gameData.indexLevel == 0 && !DataUseInGame.gameData.isDaily)
        {
            for (int i = 0; i < tutorialManager.listIndex.Count; i++)
            {
                listBBInit[i].Init(tutorialManager.listIndex[i]);
                listBBInit[i].originalScale = listBBInit[i].transform.localScale;

                listBB.Add(listBBInit[i]);
            }

            tutorialManager.ShowTutorial();
            //tutorialManager.AnimHandRotate();

        }
        else
        {
            tutorialManager.handClick.gameObject.SetActive(false);
            for (int i = 0; i < listRandomParent.Count; i++)
            {
                bubbleParent[i].CheckHasChild();
                bubbleParent[i].Init(listRandomParent[i]);
                bubbleParent[i].originalScale = bubbleParent[i].transform.localScale;

                listBB.Add(bubbleParent[i]);
            }


            for (int i = 0; i < listRandom.Count; i++)
            {
                listBBInit[i].Init(listRandom[i]);
                listBBInit[i].originalScale = listBBInit[i].transform.localScale;

                listBB.Add(listBBInit[i]);
            }
        }


        foreach (Bubble bubble in listBB)
        {
            if (!bubble.isChild)
            {
                listBBShuffle.Add(bubble);
            }
        }


    }
    public void UseBooster()
    {
        if (PlayerPrefs.GetInt(GameSave.BOOSTER_HINT) == 1 && DataUseInGame.gameData.numBoosterHint > 0)
        {
            //int countHint = PlayerPrefs.GetInt(GameSave.NUM_BOOSTER_HINT);
            int countHint = DataUseInGame.gameData.numBoosterHint;
            countHint--;
            DataUseInGame.gameData.numBoosterHint = countHint;
            DataUseInGame.instance.SaveData();
            //PlayerPrefs.SetInt(GameSave.NUM_BOOSTER_HINT, countHint);
            //PlayerPrefs.Save();
            UseBoosterHint();
        }

        if (PlayerPrefs.GetInt(GameSave.BOOSTER_TIMER) == 1 && DataUseInGame.gameData.numBoosterTimer > 0)
        {
            //int countTimer = PlayerPrefs.GetInt(GameSave.NUM_BOOSTER_TIMER);
            int countTimer = DataUseInGame.gameData.numBoosterTimer;
            countTimer--;
            DataUseInGame.gameData.numBoosterTimer = countTimer;
            DataUseInGame.instance.SaveData();
            //PlayerPrefs.SetInt(GameSave.NUM_BOOSTER_TIMER, countTimer);
            //PlayerPrefs.Save();
            UseBoosterTimer();
        }

        if (PlayerPrefs.GetInt(GameSave.BOOSTER_LIGHTNING) == 1 && DataUseInGame.gameData.numBoosterLightning > 0)
        {
            //int countLightning = PlayerPrefs.GetInt(GameSave.NUM_BOOSTER_LIGHTNING);
            int countLightning = DataUseInGame.gameData.numBoosterLightning;
            countLightning--;
            DataUseInGame.gameData.numBoosterLightning = countLightning;
            DataUseInGame.instance.SaveData();
            //PlayerPrefs.SetInt(GameSave.NUM_BOOSTER_LIGHTNING, countLightning);
            //PlayerPrefs.Save();
            UseBoosterLightning();
        }
    }
    

    int indexHint = -1;
    void UseBoosterHint()
    {
        bool foundPair = false;
        do
        {
            indexHint = UnityEngine.Random.Range(0, listBBShuffle.Count);
            for (int i = 0; i < listBBShuffle.Count; i++)
            {
                for (int j = i + 1; j < listBBShuffle.Count; j++)
                {
                    if (listBBShuffle[i].ID == listBBShuffle[indexHint].ID && listBBShuffle[j].ID == listBBShuffle[indexHint].ID
                        && i != indexHint && j != indexHint)
                    {
                        var g1 = listBBShuffle[i];
                        var g2 = listBBShuffle[j];
                        var g3 = listBBShuffle[indexHint];

                        g1.transform.DOScale(new Vector3(1.8f, 1.8f, 1.8f), 0.5f)
                            .SetEase(Ease.Flash);
                        g2.transform.DOScale(new Vector3(1.8f, 1.8f, 1.8f), 0.5f).SetEase(Ease.Flash);
                        g3.transform.DOScale(new Vector3(1.8f, 1.8f, 1.8f), 0.5f).SetEase(Ease.Flash);

                        foundPair = true;
                        return;
                    }
                }
            }
        } while (!foundPair);
    }
    void UseBoosterTimer()
    {
        timer.timeLeft += 30f;
    }
    void UseBoosterLightning()
    {
        bool foundPair = false;
        do
        {
            int index = UnityEngine.Random.Range(0, listBBShuffle.Count);
            if (indexHint != -1)
            {
                if (listBBShuffle[index].ID == listBBShuffle[indexHint].ID)
                {
                    index = UnityEngine.Random.Range(0, listBBShuffle.Count);
                }
            }

            for (int i = 0; i < listBBShuffle.Count; i++)
            {
                for (int j = i + 1; j < listBBShuffle.Count; j++)
                {
                    if (listBBShuffle[i].ID == listBBShuffle[index].ID && listBBShuffle[j].ID == listBBShuffle[index].ID
                        && i != index && j != index)
                    {
                        var g1 = listBBShuffle[i];
                        var g2 = listBBShuffle[j];
                        var g3 = listBBShuffle[index];

                        g1.transform.DOScale(new Vector3(1.3f, 1.3f, 1.3f), 1f)
                            .OnComplete(() =>
                            {
                                SolveChildOfBB(g1);
                                AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.pop);
                                StartCoroutine(AnimBBBooster(g1));
                                listBBShuffle.Remove(g1);
                                listBB.Remove(g1);
                            });

                        g2.transform.DOScale(new Vector3(1.3f, 1.3f, 1.3f), 1f)

                            .OnComplete(() =>
                            {
                                SolveChildOfBB(g2);
                                AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.pop);
                                StartCoroutine(AnimBBBooster(g2));

                                listBBShuffle.Remove(g2);
                                listBB.Remove(g2);
                            });

                        g3.transform.DOScale(new Vector3(1.3f, 1.3f, 1.3f), 1f)

                            .OnComplete(() =>
                            {
                                SolveChildOfBB(g3);
                                AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.pop);
                                StartCoroutine(AnimBBBooster(g3));

                                listBBShuffle.Remove(g3);
                                listBB.Remove(g3);
                            });

                        foundPair = true;
                        return;
                    }
                }
            }
        } while (!foundPair);

    }
    IEnumerator AnimBBBooster(Bubble bb)
    {
        bb.particleBoom.SetActive(true);
        yield return new WaitForSeconds(0.005f);
        foreach (var obj in bb.objs)
        {
            obj.SetActive(false);
        }
        bb.particlePP.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        bb.gameObject.SetActive(false);
    }
    void Update()
    {
        if (/*PlayerPrefs.GetInt("IsInGame") == 1 && */uiGame.gameObject.activeSelf)
        {
            OnClick();
        }
    }

    public void UpdateLine()
    {
        lineController.CreateLine(listBBShuffle);
    }

    //float timeCount;
    void OnClick()
    {
        if (Input.GetMouseButton(0))
        {
            if (isDrag)
            {
                canClick = false;
            }
            else
            {
                canClick = true;
            }
        }
        else
        {
            if (Input.GetMouseButtonUp(0))
            {
                isDrag = false;
                if (checkLose || timer.stopTimer || !canClick || listGOStored.Count > 6) return;
                if (PlayerPrefs.GetInt("IsInGame") != 1) return;

                RaycastHit raycastHit;
                bool isHit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycastHit, 1000f, layerMask);
                if (isHit)
                {
                    Bubble bubble = raycastHit.collider.GetComponent<Bubble>();

                    if (!bubble.click && indexLevel == 0 && !DataUseInGame.gameData.isDaily) return;

                    bubble.originalPos = bubble.transform.position;
                    bubble.particleEatt.Play();
                    AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.click);
                    if (indexLevel == 0 && !DataUseInGame.gameData.isDaily)
                    {
                        tutorialManager.OnClick(bubble);
                    }

                    Move(bubble);
                }
            }
        }

    }
    void Move(Bubble bubble)
    {
        if (listGOStored.Count > 6 && !isHint) return;

        SolveChildOfBB(bubble);

        listBB.Remove(bubble);
        listBBShuffle.Remove(bubble);
        listBubbleUndo.Add(bubble);

        if (listGOStored.Contains(bubble)) return;
        listGOStored.Add(bubble);

        List<Bubble> tempB = new List<Bubble>();
        int c = listGOStored.Count;
        while (tempB.Count < c)
        {
            tempB.Add(listGOStored[0]);
            listGOStored.RemoveAt(0);

            for (int i = 0; i < listGOStored.Count; ++i)
            {
                if (tempB[tempB.Count - 1].ID == listGOStored[i].ID)
                {
                    tempB.Add(listGOStored[i]);
                    listGOStored.RemoveAt(i);
                    --i;
                }
            }
        }
        listGOStored = tempB;

        CanEat();

        for (int i = 0; i < listGOStored.Count; ++i)
        {
            if (!listGOStored[i].IsDone)
            {
                listGOStored[i].Move(listPoint[i], -1, CheckEat);
            }
        }


    }
    private void SolveChildOfBB(Bubble bubble)
    {
        if (bubble.hasChildren)
        {
            for (int i = bubble.children.childCount - 1; i >= 0; i--)
            {
                Transform child = bubble.children.GetChild(i);
                child.SetParent(level.transform);
                child.transform.DOScale(new Vector3(1f, 1f, 1f), 0.3f);

                Bubble childBB = child.GetComponent<Bubble>();
                childBB.ResetState(childBB);
                listBBShuffle.Add(childBB);
            }

            bubble.hasChildren = false;
        }
    }

    void CanEat()
    {
        for (int i = 0; i < listGOStored.Count - 2; ++i)
        {
            if (listGOStored[i].ID == listGOStored[i + 1].ID && listGOStored[i + 1].ID == listGOStored[i + 2].ID)
            {
                canEat = true;
                Debug.Log(canEat);
            }
        }
    }

    public RectTransform img;
    public Transform endPosStar;
    void CheckEat()
    {
        Tweener tweener = null;
        for (int i = 0; i < listGOStored.Count - 2; ++i)
        {
            if (listGOStored[i].CanMoving && listGOStored[i + 1].CanMoving && listGOStored[i + 2].CanMoving
                && listGOStored[i].ID == listGOStored[i + 1].ID && listGOStored[i + 1].ID == listGOStored[i + 2].ID)
            {
                comboSystem.IncreaseCombo();
                //canEat = true;
                isHint = false;

                count -= 1;
                var g1 = listGOStored[i];
                var g2 = listGOStored[i + 1];
                var g3 = listGOStored[i + 2];
                listGOStored.Remove(g1);
                listGOStored.Remove(g2);
                listGOStored.Remove(g3);

                //Instantiate(particleTest);
                //particleTest.transform.position = Camera.main.WorldToScreenPoint(g2.transform.position);

                AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.eat);

                g1.particleBoom.SetActive(true);
                g2.particleBoom.SetActive(true);
                g3.particleBoom.SetActive(true);

                img.gameObject.SetActive(true);
                img.transform.position = Camera.main.WorldToScreenPoint(g2.transform.position);

                g1.Move(g2.transform.parent, 0.3f, () =>
                {
                    img.DOMove(endPosStar.position, 0.5f);

                    g1.gameObject.SetActive(false);
                    g2.gameObject.SetActive(false);
                    g3.gameObject.SetActive(false);

                    listBubbleUndo.Remove(g1);
                    listBubbleUndo.Remove(g2);
                    listBubbleUndo.Remove(g3);

                    canClick = true;
                });
                g2.Move(g2.transform.parent, 0.31f);
                g3.Move(g2.transform.parent, 0.31f);
                g1.IsDone = g2.IsDone = g3.IsDone = true;



                tweener = g3.tweenerMove;
                i += 2;
            }
        }
        if (tweener != null)
        {
            tweener.OnComplete(() =>
            {
                CheckDone();
            });
        }

        if (indexLevel == 5 && !DataUseInGame.gameData.isDaily)
        {
            tutorialManager.HideHandWrapped();
        }

        if (count == 0)
        {
            CheckLose();
        }
    }
    void CheckDone()
    {
        canEat = false;
        for (int i = 0; i < listGOStored.Count; ++i)
        {
            if (listGOStored[i].CanMoving)
            {
                listGOStored[i].Move(listPoint[i], 0.2f, CheckEat);
            }
        }
        hinting = false;
        CheckWin();

    }
    public IEnumerator AnimBoomBB(string str)
    {
        for (int i = 0; i < listBB.Count; ++i)
        {
            StartCoroutine(AnimBB(listBB[i]));
            SolveChildOfBB(listBB[i]);
            AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.pop);
            listBBShuffle.Remove(listBB[i]);
        }

        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(str);
        DOTween.KillAll();
    }
    IEnumerator AnimBB(Bubble bb)
    {
        bb.particleBoom.SetActive(true);
        yield return new WaitForSeconds(0.005f);
        foreach (var obj in bb.objs)
        {
            obj.SetActive(false);
        }
        bb.particlePP.SetActive(true);
        yield return new WaitForSeconds(4.0f);

        bb.gameObject.SetActive(false);
    }
    public void CheckLose()
    {
        if (checkLose || listGOStored.Count <= 6 || isHint || canEat) return;

        Debug.Log("you lose");
        checkLose = true;
        timer.stopTimer = true;

        logicUI.OpenLoseUI();
        logicUI.loseUI.OpenPanelOutOfMove();

    }
    public void Lose()
    {
        winStreak = 0;
        PlayerPrefs.SetInt("WinStreak", winStreak);
        PlayerPrefs.Save();
    }
    public void CheckWin()
    {
        if (!checkLose && !checkWin && listBB.Count <= 0)
        {
            timer.stopTimer = true;
            checkWin = true;
            logicUI.btnPause.interactable = false;
            tutorialManager.handRotate.gameObject.SetActive(false);
            tutorialManager.fingerRotate.SetActive(false);
            tutorialManager.imgRotate.SetActive(false);
            winStreak++;

            PlayerPrefs.SetInt("WinStreak", winStreak);
            PlayerPrefs.Save();
            AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.win);
            pathCreaterGift.DOMove(new Vector3(pathCreaterGift.position.x, -1.5f, pathCreaterGift.transform.position.z), 2f)
                .OnComplete(() =>
                {
                    logicUI.OpenWinUI();
                });
        }
    }

    public bool useByBtn;
    bool isShuffleing = false;
    List<Vector3> listNewPosShuffle = new List<Vector3>();
    public void Shuffle()
    {
        AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.clickMenu);
        int numShuffle = DataUseInGame.gameData.numShuffleItem;
        if (numShuffle <= 0) return;
        if (isShuffleing) return;
        if (checkWin) return;

        //if (timer.stopTimer) return;
        if (isUseBooster) return;

        if (useByBtn)
        {
            numShuffle--;
            if (DataUseInGame.gameData.indexLevel == 3 && !DataUseInGame.gameData.isTutShuffleDone)
            {
                logicUI.tutBtn.listHands[3].SetActive(false);
                logicUI.tutBtn.listTexts[3].SetActive(false);
                logicUI.tutBtn.ImageHint.SetActive(false);
                DataUseInGame.gameData.isTutShuffleDone = true;
                DataUseInGame.instance.SaveData();
                timer.stopTimer = false;
            }
        }

        DataUseInGame.gameData.numShuffleItem = numShuffle;
        DataUseInGame.instance.SaveData();

        listNewPosShuffle.Clear();

        List<Bubble> list = new List<Bubble>();
        int r;
        while (listBBShuffle.Count > 0)
        {
            r = UnityEngine.Random.Range(0, listBBShuffle.Count);
            list.Add(listBBShuffle[r]);
            listBBShuffle.RemoveAt(r);
        }

        listBBShuffle.AddRange(list);

        while (list.Count > 0)
        {
            int i = UnityEngine.Random.Range(0, list.Count);
            listNewPosShuffle.Add(list[i].transform.position);
            list.RemoveAt(i);
        }

        for (int i = 0; i < listBBShuffle.Count; i++)
        {
            int currentIndex = i;
            if (canShuffle)
            {
                Physics.autoSimulation = false;
                listBBShuffle[currentIndex].transform.DOMove(listNewPosShuffle[currentIndex], 1f)
                    .OnStart(() =>
                    {
                        canShuffle = false;
                        canClick = false;
                        isShuffleing = true;
                    })

                    .OnComplete(() =>
                    {
                        Physics.autoSimulation = true;
                        canShuffle = true;
                        canClick = true;
                        isShuffleing = false;
                        useByBtn = false;
                    });
            }
        }
    }

    public bool isHint;
    public bool hinting = false;
    public void Hint()
    {
        AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.clickMenu);
        int numHint = DataUseInGame.gameData.numHintItem;
        if (numHint <= 0) return;
        if (checkLose || canEat || listGOStored.Count > 6) return;
        if (isUseBooster) return;
        if (hinting) return;
        if (checkWin) return;
        isHint = true;

        if (DataUseInGame.gameData.indexLevel == 1 && !DataUseInGame.gameData.isTutHintDone)
        {
            timer.stopTimer = false;
            logicUI.tutBtn.ImageHint.SetActive(false);
            logicUI.tutBtn.listHands[0].SetActive(false);
            logicUI.tutBtn.listTexts[0].SetActive(false);
            DataUseInGame.gameData.isTutHintDone = true;
            DataUseInGame.instance.SaveData();
        }

        numHint--;


        canClick = false;
        DataUseInGame.gameData.numHintItem = numHint;
        DataUseInGame.instance.SaveData();

        if (listGOStored.Count > 0)
        {
            if (listGOStored.Count > 1)
            {
                if (listGOStored[0].ID == listGOStored[1].ID)
                {
                    for (int i = 0; i < listBB.Count; i++)
                    {
                        if (listGOStored[0].ID == listBB[i].ID)
                        {
                            //isHint = false;
                            Move(listBB[i]);
                            hinting = true;
                            return;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < listBB.Count; i++)
                    {
                        for (int j = i + 1; j < listBB.Count; j++)
                        {
                            if (listGOStored[0].ID == listBB[i].ID && listGOStored[0].ID == listBB[j].ID)
                            {
                                // isHint = false;
                                Move(listBB[i]);
                                Move(listBB[j - 1]);
                                hinting = true;

                                return;
                            }
                        }
                    }
                }
            }


            for (int i = 0; i < listBB.Count; i++)
            {
                for (int j = i + 1; j < listBB.Count; j++)
                {
                    if (listGOStored[0].ID == listBB[i].ID && listGOStored[0].ID == listBB[j].ID)
                    {
                        // isHint = false;
                        Move(listBB[i]);
                        Move(listBB[j - 1]);
                        hinting = true;

                        return;
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < listBB.Count; i++)
            {
                for (int j = i + 1; j < listBB.Count; j++)
                {
                    for (int k = j + 1; k < listBB.Count; k++)
                    {
                        if (listBB[i].ID == listBB[j].ID && listBB[j].ID == listBB[k].ID)
                        {
                            // isHint = false;
                            Move(listBB[i]);
                            Move(listBB[j - 1]);
                            Move(listBB[k - 2]);
                            hinting = true;

                            return;
                        }
                    }
                }
            }

        }
    }
    public void Undo()
    {
        AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.clickMenu);
        int numUndo = DataUseInGame.gameData.numUndoItem;

        if (numUndo <= 0) return;
        if (listBubbleUndo.Count <= 0) return;
        if (checkLose) return;
        if (canEat) return;
        if (hinting) return;
        if (checkWin) return;

        if (isUseBooster) return;

        if (DataUseInGame.gameData.indexLevel == 2 && !DataUseInGame.gameData.isTutUndoDone)
        {
            logicUI.tutBtn.listHands[1].SetActive(false);
            logicUI.tutBtn.listTexts[1].SetActive(false);
            logicUI.tutBtn.TutTrippleUndo();
        }

        numUndo--;
        DataUseInGame.gameData.numUndoItem = numUndo;
        DataUseInGame.instance.SaveData();

        int index = listBubbleUndo.Count - 1;
        Bubble bubble = listBubbleUndo[index];
        bubble.ResetStateIfUndo();
        bubble.transform.DOMove(bubble.originalPos, 0.3f).
            OnComplete(() =>
            {
                bubble.meshCollider.enabled = true;
            });
        bubble.transform.SetParent(level.transform);
        bubble.transform.DOScale(new Vector3(1f, 1f, 1f), 0.3f);

        listBB.Add(bubble);
        listBBShuffle.Add(bubble);
        listGOStored.Remove(bubble);
        listBubbleUndo.RemoveAt(index);
        for (int j = 0; j < listGOStored.Count; ++j)
        {
            listGOStored[j].Move(listPoint[j], -1, CheckDone);
        }
        return;
    }

    bool isUndoing = false;
    public void UndoTripple()
    {
        AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.clickMenu);
        int numTrippleUndo = DataUseInGame.gameData.numTrippleUndoItem;

        if (numTrippleUndo <= 0) return;
        if (listBubbleUndo.Count <= 0) return;
        if (checkLose) return;
        if (canEat) return;
        if (isUndoing) return;
        if (hinting) return;
        if (checkWin) return;
        if (isUseBooster) return;

        if (DataUseInGame.gameData.indexLevel == 2 && !DataUseInGame.gameData.isTutUndoDone)
        {
            isInTut = false;
            logicUI.tutBtn.listHands[2].SetActive(false);
            logicUI.tutBtn.listTexts[2].SetActive(false);
            logicUI.tutBtn.ImageHint.SetActive(false);
            DataUseInGame.gameData.isTutUndoDone = true;
            DataUseInGame.instance.SaveData();
            timer.stopTimer = false;
        }
        numTrippleUndo--;
        DataUseInGame.gameData.numTrippleUndoItem = numTrippleUndo;
        DataUseInGame.instance.SaveData();
        StartCoroutine(UndoTrippleCoroutine());
    }
    private IEnumerator UndoTrippleCoroutine()
    {
        isUndoing = true;
        int count = 0;

        for (int i = listBubbleUndo.Count - 1; i >= 0; --i)
        {
            if (count >= 3) break;
            count++;

            int index = listBubbleUndo.Count - 1;
            Bubble bubble = listBubbleUndo[index];
            bubble.ResetStateIfUndo();
            bubble.transform.DOMove(bubble.originalPos, 0.3f)
                .OnComplete(() =>
                {
                    bubble.meshCollider.enabled = true;
                });
            bubble.transform.SetParent(level.transform);
            bubble.transform.DOScale(new Vector3(1f, 1f, 1f), 0.3f);
            listBB.Add(bubble);
            listBBShuffle.Add(bubble);
            listGOStored.Remove(bubble);
            listBubbleUndo.RemoveAt(index);
            yield return new WaitForSeconds(0.2f);

            for (int j = 0; j < listGOStored.Count; ++j)
            {
                listGOStored[j].Move(listPoint[j], -1, CheckDone);
            }
        }

        isUndoing = false;
    }
    public void UndoAll()
    {
        if (listBubbleUndo.Count <= 0) return;
        if (checkLose) return;
        if (canEat) return;
        if (isUndoing) return;

        buttonController.SetStateIfContinue();

        StartCoroutine(UndoAllCoroutine());
    }
    private IEnumerator UndoAllCoroutine()
    {
        isUndoing = true;
        for (int i = listBubbleUndo.Count - 1; i >= 0; --i)
        {
            int index = listBubbleUndo.Count - 1;
            Bubble bubble = listBubbleUndo[index];
            bubble.ResetStateIfUndo();
            bubble.transform.DOMove(bubble.originalPos, 0.3f)
                .OnComplete(() =>
                {
                    bubble.meshCollider.enabled = true;
                });
            bubble.transform.SetParent(level.transform);
            bubble.transform.DOScale(new Vector3(1f, 1f, 1f), 0.3f);
            listBB.Add(bubble);
            listBBShuffle.Add(bubble);
            listGOStored.Remove(bubble);
            listBubbleUndo.RemoveAt(index);
            yield return new WaitForSeconds(0.2f);
        }

        Shuffle();
        canClick = true;

        buttonController.SetStateAfterContinueDone();

        isUndoing = false;
        timer.stopTimer = false;
        timer.isFreeze = false;
    }

    bool isFreezeing = false;
    public void Freeze()
    {
        canClick = true;
        AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.clickMenu);
        int numFreeze = DataUseInGame.gameData.numFreezeTimeItem;
        if (numFreeze <= 0) return;
        if (isFreezeing) return;
        if (checkWin) return;

        if (isUseBooster) return;

        if (DataUseInGame.gameData.indexLevel == 4 && !DataUseInGame.gameData.isTutFreezeDone)
        {
            logicUI.tutBtn.listHands[4].SetActive(false);
            logicUI.tutBtn.listTexts[4].SetActive(false);
            logicUI.tutBtn.ImageHint.SetActive(false);
            DataUseInGame.gameData.isTutFreezeDone = true;
            DataUseInGame.instance.SaveData();
            timer.stopTimer = false;

        }

        numFreeze--;
        DataUseInGame.gameData.numFreezeTimeItem = numFreeze;
        DataUseInGame.instance.SaveData();

        timer.isFreeze = true;
        StartCoroutine(StopFreeze());
    }
    IEnumerator StopFreeze()
    {
        isFreezeing = true;
        yield return new WaitForSeconds(5f);
        timer.isFreeze = false;
        isFreezeing = false;
    }
    public IEnumerator CanClickAgain()
    {
        yield return new WaitForSeconds(0.4f);
        if (listGOStored.Count > 6)
        {
            UndoAll();
        }
        timer.stopTimer = false;
        timer.isFreeze = false;
        canClick = true;
    }
}
