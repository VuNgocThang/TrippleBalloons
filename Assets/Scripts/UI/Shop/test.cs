using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class test : MonoBehaviour
{
    public ItemShop itemShop;
    public List<ListDataShopReward> dataItemShop;
    public GameObject none;
    private void Start()
    {
        InitShopPanel();
    }

    private void InitShopPanel()
    {
        for (int i = 0; i < dataItemShop.Count; i++)
        {
            ItemShop item = Instantiate(itemShop, transform);
            item.coin = dataItemShop[i].coin;
            item.txtCoin.text = dataItemShop[i].coin.ToString();
            item.txtName.text = dataItemShop[i].text;
            item.numItemGrid = dataItemShop[i].numItem;
            item.InitItemGrid();
        }

        Instantiate(none, transform);
    }
}
