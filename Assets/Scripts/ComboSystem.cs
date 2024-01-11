using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComboSystem : MonoBehaviour
{
    public int comboCount = 0;
    public float defaultTimerCombo = 4f;
    public float comboTimer;
    private float currentComboTimer;

    public Image imgFillCombo;
    public TextMeshProUGUI txtCombo;
    public GameObject progress;
    public GameObject comboBaseObj;
    public List<Sprite> listSpriteColor;

    private void Start()
    {
        InitializeCombo();
    }
    private void Update()
    {
        UpdateComboTimer();
        
    }
    private void InitializeCombo()
    {
        comboTimer = defaultTimerCombo;
        currentComboTimer = comboTimer;
        imgFillCombo.sprite = listSpriteColor[0];
    }
    private void UpdateComboTimer()
    {
        if (comboCount > 0)
        {
            currentComboTimer -= Time.deltaTime;
            imgFillCombo.fillAmount = currentComboTimer / comboTimer;

            if (currentComboTimer <= 0f)
            {
                ResetCombo();
            }
        }
    }
    public void IncreaseCombo()
    {
        comboCount++;
        UpdateComboColor();
        currentComboTimer = comboTimer;
        Debug.Log("Combo: " + comboCount);  
    }
    private void UpdateComboColor()
    {
        if (comboCount >= 3 && comboCount < 6)
        {
            imgFillCombo.sprite = listSpriteColor[1];
            comboTimer = 3f;
        }
        else if (comboCount >= 6 && comboCount < 9)
        {
            imgFillCombo.sprite = listSpriteColor[2];
            comboTimer = 2f;
        }
        else if (comboCount >= 9 && comboCount < 12)
        {
            imgFillCombo.sprite = listSpriteColor[3];
        }
        else if (comboCount >= 12 && comboCount < 15)
        {
            imgFillCombo.sprite = listSpriteColor[4];
        }
        else if (comboCount >= 15)
        {
            imgFillCombo.sprite = listSpriteColor[5];
        }
    }
    public void ResetCombo()
    {
        comboCount = 0;
        InitializeCombo();
        //Debug.Log("Combo Reset!");
    }
    private void OnGUI()
    {
        if (comboCount > 0)
        {
            comboBaseObj.SetActive(true);
            progress.SetActive(true);
            txtCombo.text = $"Combo x {comboCount}";

            Material newMaterial = new Material(txtCombo.fontSharedMaterial);
            txtCombo.fontSharedMaterial = newMaterial; 

            newMaterial.EnableKeyword("GLOW_ON"); 
        }
        else
        {
            comboBaseObj.SetActive(false);
            progress.SetActive(false);
        }

    }
}
