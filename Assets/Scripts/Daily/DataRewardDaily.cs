using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ListDataRewardDaily
{
    public List<DataRewardDaily> listDataRewardDaily = new List<DataRewardDaily>();
}

[Serializable]
public class DataRewardDaily
{
    public GameObject obj;
    public TextMeshProUGUI txtNum;
    public Image img;

    [Header("Data")]
    public int value;
    public Sprite icon;
}