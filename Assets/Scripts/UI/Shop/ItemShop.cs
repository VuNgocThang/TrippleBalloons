using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemShop : MonoBehaviour
{
    public int coin;
    public Button btnClaim;

    public TextMeshProUGUI txtName;
    public TextMeshProUGUI txtCoin;



    [Header("IconGridInShop")]
    public Transform grid;
    public int numItemGrid;
    public IconItemShop prefabIcon;
    public List<DataItemIconShop> dataItemGrid;

    private void Start()
    {
        btnClaim.onClick.AddListener(ClaimItemShop);
    }

    void ClaimItemShop()
    {
        Debug.Log("Claimed" + " -- " + coin);
    }

    public void InitItemGrid()
    {
        for (int i = 0; i < dataItemGrid.Count; i++)
        {
            IconItemShop icon = Instantiate(prefabIcon, grid);
            icon.imgIconItem.sprite = dataItemGrid[i].icon;
            icon.txtNumItem.text = numItemGrid.ToString();
        }
    }

}
