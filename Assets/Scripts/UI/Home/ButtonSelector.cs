using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelector : MonoBehaviour
{
    public int id;
    public int idBought;
    public string stringName;
    public int cost;
    public int value;
    public TextMeshProUGUI txtCost;
    public TextMeshProUGUI txtValue;
    public Image icon;
    public Image imgButton;
    public Image imgBtnBuy;
    public GameObject lockObject;
    public Button btnBuy;

    public bool interac;
    private void Update()
    {
        interac = btnBuy.interactable;
    }
    private void Awake()
    {
        imgButton = GetComponent<Image>();
    }
    public void Init(int id, int cost, int value, Sprite icon, Sprite imgBtn, string name)
    {
        this.id = id;
        this.cost = cost;
        this.value = value;
        this.icon.sprite = icon;
        imgButton.sprite = imgBtn;
        txtCost.text = cost.ToString();
        txtValue.text = $"x{value}";
        stringName = name;
    }
    
}
