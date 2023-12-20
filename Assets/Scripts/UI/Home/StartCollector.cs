using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class StartCollector : MonoBehaviour
{
    public static StartCollector ins;
    public List<DataButton> listDataBtn = new List<DataButton>();
    public List<DataReward> listDataRw = new List<DataReward>();
    public List<ButtonSelector> listBtnSelector = new List<ButtonSelector>();
    public List<Sprite> listSpriteBtn = new List<Sprite>();
    public ButtonSelector prefab;
    public ListUnlockReward unlockReward = new ListUnlockReward();
    public int currentIndex;
    public Transform parent;
    public int star;
    public TextMeshProUGUI txtTimer;

    private void Awake()
    {
        ins = this;
    }
    private void Start()
    {
        Init();
        LoadDataItems();
        UpdateUnlockBtn();
        UnlockNewBtnSelector();
    }

    private void OnGUI()
    {
        float timerStarCollector = DataUseInGame.gameData.timeStarCollector;
        float hours = Mathf.Floor(timerStarCollector / 3600);

        float timePerHour = timerStarCollector - hours * 3600;
        float minutes = Mathf.Floor(timePerHour / 60);
        float seconds = Mathf.RoundToInt(timePerHour % 60);
        txtTimer.text = hours.ToString("00") + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    public void Init()
    {
        currentIndex = DataUseInGame.gameData.currentIndexStarCollector;
        Debug.Log(currentIndex);
        star = DataUseInGame.gameData.star;

        for (int i = 0; i < listDataRw.Count; i++)
        {
            ButtonSelector btn = Instantiate(prefab, parent);
            btn.Init(listDataRw[i].id, listDataBtn[i].cost, listDataRw[i].value, listDataRw[i].icon, listDataRw[i].imgBtn, listDataRw[i].nameRW);
            listBtnSelector.Add(btn);

            DataUnlockReward data = new DataUnlockReward();
            unlockReward.listUnlockReward.Add(data);
        }
    }

    int GetButtonByIndex(int index)
    {
        foreach (var btn in listDataRw)
        {
            if (btn.id == index)
            {
                return listDataRw.IndexOf(btn);
            }
        }
        return -1;
    }

    public void UnlockNewBtnSelector()
    {
        for (int i = 0; i < listBtnSelector.Count; i++)
        {
            int a = i;

            listBtnSelector[a].btnBuy.onClick.AddListener(() =>
            {
                if (DataUseInGame.gameData.star < listBtnSelector[a].cost) return;

                AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.clickMenu);
                GameManager.Instance.SubStar(listBtnSelector[a].cost);

                listBtnSelector[a].btnBuy.interactable = false;
                SwitchAddReward(listBtnSelector[a].stringName, listBtnSelector[a].value);

                currentIndex++;
                DataUseInGame.gameData.currentIndexStarCollector = currentIndex;
                DataUseInGame.instance.SaveData();

                listBtnSelector[a].idBought = 1;

                SaveDataItemsJson(a);
                PlayerPrefs.SetInt("CurrentIndex", currentIndex);
                PlayerPrefs.Save();

                UpdateUnlockBtn();

                if (currentIndex >= listBtnSelector.Count) return;

                listBtnSelector[GetButtonByIndex(currentIndex)].lockObject.SetActive(false);
            });
        }
    }
    // 1 : lock
    // 0 : unlock

    public void UpdateUnlockBtn()
    {
        star = DataUseInGame.gameData.star;

        foreach (ButtonSelector buttonSelector in listBtnSelector)
        {
            if (buttonSelector.id == currentIndex)
            {
                //buttonSelector.btnBuy.interactable = true;
                buttonSelector.imgBtnBuy.sprite = listSpriteBtn[0];
                buttonSelector.lockObject.SetActive(false);
            }

            //if (star < buttonSelector.cost)
            //{
            //    buttonSelector.btnBuy.interactable = false;
            //}

            if (buttonSelector.id > currentIndex)
            {
                //buttonSelector.btnBuy.interactable = false;
                buttonSelector.imgBtnBuy.sprite = listSpriteBtn[1];

                buttonSelector.lockObject.SetActive(true);
            }
        }
    }

    public void SaveDataItemsJson(int i)
    {
        unlockReward.listUnlockReward[i].id = listBtnSelector[i].idBought;
        SaveDataItemsJson();
    }
    public void SaveDataItemsJson()
    {
        string json = JsonUtility.ToJson(unlockReward, true);
        PlayerPrefs.SetString("DataStarCollector", json);
    }
    public void LoadDataItemsJson()
    {
        for (int i = 0; i < listBtnSelector.Count; i++)
        {
            //int a = i;
            listBtnSelector[i].idBought = unlockReward.listUnlockReward[i].id;
            if (listBtnSelector[i].idBought == 1)
            {
                listBtnSelector[i].btnBuy.interactable = false;
                listBtnSelector[i].imgBtnBuy.sprite = listSpriteBtn[0];
                listBtnSelector[i].lockObject.SetActive(false);
            }
            else
            {
                //listBtnSelector[i].btnBuy.interactable = true;
                listBtnSelector[i].imgBtnBuy.sprite = listSpriteBtn[0];
                listBtnSelector[i].lockObject.SetActive(true);
            }
        }
    }

    public void LoadDataItems()
    {
        if (PlayerPrefs.GetString("DataStarCollector").Equals(""))
        {
            SaveDataItemsJson(0);
            unlockReward = JsonUtility.FromJson<ListUnlockReward>(PlayerPrefs.GetString("DataStarCollector"));
            LoadDataItemsJson();
        }
        else
        {
            unlockReward = JsonUtility.FromJson<ListUnlockReward>(PlayerPrefs.GetString("DataStarCollector"));
            LoadDataItemsJson();
        }
    }

    public void SwitchAddReward(string str, int value)
    {
        switch (str)
        {
            case "gold":
                GameManager.Instance.AddGold(value);
                break;
            case "star":
                // do something;
                break;
            case "hint":
                int countHint;
                if (PlayerPrefs.HasKey("NumHint"))
                {
                    countHint = PlayerPrefs.GetInt("NumHint");
                }
                else
                {
                    countHint = 0;
                }
                countHint++;
                PlayerPrefs.SetInt("NumHint", countHint);
                PlayerPrefs.Save();
                break;

            case "timer":
                int countTimer;
                if (PlayerPrefs.HasKey("NumTimer"))
                {
                    countTimer = PlayerPrefs.GetInt("NumTimer");
                }
                else
                {
                    countTimer = 0;
                }
                countTimer++;
                PlayerPrefs.SetInt("NumTimer", countTimer);
                PlayerPrefs.Save();
                break;
            case "lightning":
                int countLightning;
                if (PlayerPrefs.HasKey("NumLightning"))
                {
                    countLightning = PlayerPrefs.GetInt("NumLightning");
                }
                else
                {
                    countLightning = 0;
                }
                countLightning++;
                PlayerPrefs.SetInt("NumLightning", countLightning);
                PlayerPrefs.Save();
                //do something;
                break;
            default:
                break;
        }
    }

}
