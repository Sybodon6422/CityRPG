using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using TMPro;
using UnityEngine;

public class NPCInteractionMenu : MonoBehaviour
{
    [SerializeField] GameObject leaveButton, simpleShopOption;
    [SerializeField] private TextMeshProUGUI dialougeText;

    [SerializeField] MenuItemButton itemForSale;

    public void OpenMenuShop(string baseText, NonPlayerCharacter npc, ItemInShop soldItem)
    {
        gameObject.SetActive(true);
        dialougeText.text = baseText;
        PlayerMaster.I.PlayerEnteractedWithShop();
        itemForSale.Setup(soldItem);
    }


    public void LeaveMenu()
    {
        PlayerMaster.I.PlayerLeftShop();
        gameObject.SetActive(false);
    }

    [Serializable]
    public class NPCShopOption
    {

    }
}
