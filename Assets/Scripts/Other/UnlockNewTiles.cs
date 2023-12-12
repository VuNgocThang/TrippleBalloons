
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnlockNewTiles : MonoBehaviour
{
    public int currentTile;
    public int rewardTile;
    public int max;
    public TextMeshProUGUI text;
    public Button oke;

    private void Start()
    {
        oke.onClick.AddListener(IncreaseMax);
        if (!PlayerPrefs.HasKey("MaxReward"))
        {
            max = 4;
            PlayerPrefs.SetInt("MaxReward", max);
            PlayerPrefs.Save();
        }
        else
        {
            max = PlayerPrefs.GetInt("MaxReward");
        }

        rewardTile = PlayerPrefs.GetInt("RewardTile");
        rewardTile += 1;
        currentTile = rewardTile;

        PlayerPrefs.SetInt("RewardTile", rewardTile);
        PlayerPrefs.Save();

        if (currentTile > max) currentTile = max;
        text.text = $"{currentTile}/{max}";
    }

    public void IncreaseMax()
    {
        if (rewardTile >= max)
        {
            max += 1;
            Debug.Log(max);
            PlayerPrefs.SetInt("MaxReward", max);

            rewardTile = 0;
            text.text = $"{rewardTile}/{max}";

            PlayerPrefs.SetInt("RewardTile", rewardTile);
            PlayerPrefs.Save();
        }
    }

}
