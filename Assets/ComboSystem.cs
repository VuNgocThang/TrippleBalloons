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

    private void Start()
    {
        comboTimer = defaultTimerCombo;
        currentComboTimer = comboTimer;
    }

    private void Update()
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
        if (comboCount > 3)
        {
            comboTimer = 3f;
        }

        if(comboCount > 5)
        {
            comboTimer = 2f;
        }
        currentComboTimer = comboTimer;
        //Debug.Log("Combo: " + comboCount);
    }

    public void ResetCombo()
    {
        comboCount = 0;
        comboTimer = defaultTimerCombo;
        currentComboTimer = comboTimer;
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
