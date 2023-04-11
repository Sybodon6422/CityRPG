using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuServiceButton : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameText, costText;
    [SerializeField] Image itemIcon;
    private Service thisService;
    public void Setup(Service _service)
    {
        thisService = _service;

        costText.text = (_service.cost/100m).ToString();
        itemIcon.sprite = _service.serviceIcon;
        nameText.text = _service.serviceText;
    }
    public void ButtonPressed()
    {
        HUDManager.I.GetShopInterface().BuyService(thisService);
    }
}
