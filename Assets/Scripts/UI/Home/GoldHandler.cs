using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GoldHandler : MonoBehaviour
{
    public int gold;
    public TextMeshProUGUI txtGold;

    private void OnGUI()
    {
        gold = DataUseInGame.gameData.gold;
        txtGold.text = gold.ToString();
    }
}
