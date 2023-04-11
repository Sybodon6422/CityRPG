using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private RectTransform healthBarColour;
    [SerializeField] private TextMeshProUGUI hbarText;
    void Start()
    {
        
    }

    public void UpdateHealthBarText(int curHealth, int MaxHealth)
    {
        hbarText.text = curHealth + "/" + MaxHealth;
        healthBarColour.sizeDelta = new Vector2((curHealth / MaxHealth) * 200,20);
    }
}
