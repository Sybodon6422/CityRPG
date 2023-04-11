using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuRealtyButton : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameText, costText;
    [SerializeField] Image itemIcon;
    [SerializeField] int propertyInt;
    public void ButtonPressed()
    {
        //HUDManager.I.GetBankInterface().BuyProperty(propertyInt);
    }
}
