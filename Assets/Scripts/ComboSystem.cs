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
    public Color colorCombo3;
    public Color colorCombo6;
    public Color colorCombo9;
    public Color colorCombo12;
    public Color colorCombo15;
    public Color colorComboOver;


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
        imgFillCombo.color = colorCombo3;
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
        //Debug.Log("Combo: " + comboCount);  
    }

    private void UpdateComboColor()
    {
        if (comboCount >= 3 && comboCount < 6)
        {
            imgFillCombo.color = colorCombo6;
            comboTimer = 3f;
        }
        else if (comboCount >= 6 && comboCount < 9)
        {
            imgFillCombo.color = colorCombo9;
            comboTimer = 2f;
        }
        else if (comboCount >= 9 && comboCount < 12)
        {
            imgFillCombo.color = colorCombo12;
        }
        else if (comboCount >= 12 && comboCount < 15)
        {
            imgFillCombo.color = colorCombo15;
        }
        else if (comboCount >= 15)
        {
            imgFillCombo.color = colorComboOver;
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
            progress.SetActive(true);
            txtCombo.text = $"Combo x {comboCount}";
        }
        else
        {
            progress.SetActive(false);
        }
    }
}
