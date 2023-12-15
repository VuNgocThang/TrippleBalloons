using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPlusScript : MonoBehaviour
{
    public Button btnPlus;
    public LogicUITest logicUI;

    private void Start()
    {
        btnPlus.onClick.AddListener(logicUI.OpenPanelShop);
    }
}
