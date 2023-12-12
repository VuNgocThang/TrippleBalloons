using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class ListIndexBB
{
    public List<int> listIndex = new List<int>();
}
public class NewTilesHandler : MonoBehaviour
{
    public List<ListIndexBB> listIndexAllGame = new List<ListIndexBB>();
    public List<Bubble> listNewBB = new List<Bubble>();
    public int currentIndex;
    public GameObject NLight;

    private void Start()
    {
        NLight.SetActive(true);
        if (PlayerPrefs.HasKey("CurrentIndexNewTile"))
        {
            currentIndex = PlayerPrefs.GetInt("CurrentIndexNewTile");
        }
        else
        {
            currentIndex = 0;
        }

        for (int i = 0; i < listNewBB.Count; i++)
        {
            int index = listIndexAllGame[currentIndex].listIndex[i];
            listNewBB[i].InitBBInUI(index);
        }

        foreach (int item in listIndexAllGame[currentIndex].listIndex)
        {
            if (!DataUseInGame.gameData.listIndex.Contains(item))
            {
               DataUseInGame.gameData.listIndex.Add(item);
            }
            DataUseInGame.instance.SaveData();
        }

        if (currentIndex < listIndexAllGame.Count - 1)
        {
            currentIndex++;
        }
        else
        {
            currentIndex = listIndexAllGame.Count - 1;
        }
        PlayerPrefs.SetInt("CurrentIndexNewTile", currentIndex);
        PlayerPrefs.Save();

    }
    private void Update()
    {
        for (int i = 0; i < listNewBB.Count; i++)
        {
            listNewBB[i].transform.Rotate(Vector3.up * 10f * Time.deltaTime);
        }
    }
}
