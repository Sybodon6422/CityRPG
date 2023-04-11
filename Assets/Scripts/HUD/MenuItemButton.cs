using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuItemButton : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameText, costText;
    [SerializeField] Image itemIcon;
    private ItemInShop thisItem;
    public void Setup(ItemInShop item)
    {
        thisItem = item;

        costText.text = (item.cost/100m).ToString("C");
        itemIcon.sprite = item.item.itemIcon;
        nameText.text = item.item.itemName;
    }
    public void ButtonPressed()
    {
        HUDManager.I.GetShopInterface().BuyItem(thisItem);
    }
}
