using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI label;

    [SerializeField] public Button button;

    [SerializeField] public GameObject mark;

    public int index;
    public bool isDone;
    public bool isSelected;

    public void Initialize(string label)
    {
        this.label.text = label;
        index = int.Parse(label);
    }

    private void Awake()
    {
        button = GetComponent<Button>();
    }

   
}
