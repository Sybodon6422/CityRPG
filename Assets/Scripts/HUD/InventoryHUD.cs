using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryHUD : MonoBehaviour
{
    [SerializeField] Transform inventoryHolder;
    [SerializeField] GameObject inventoryItemFab;

    //stats
    [SerializeField] TextMeshProUGUI strStatText, agiStatText, intStatText, chaStatText;

    public void OpenInventory(List<InventoryItem> inventory)
    {
        PlayerMaster.I.PlayerEnteractedWithShop();
        gameObject.SetActive(true);
        UpdateInventory();
        UpdateStats();
    }

    public void CloseInventory()
    {
        PlayerMaster.I.PlayerLeftShop();
        gameObject.SetActive(false);
    }

    public void UpdateInventory()
    {
        var inventory = PlayerMaster.I.inventory.GetInventory();
        for (int i = 0; i < inventoryHolder.childCount; i++)
        {
            Destroy(inventoryHolder.GetChild(i).gameObject);
        }
        foreach(InventoryItem item in inventory)
        {
            if (!item.refItem.inventoryHide)
            {
                if(item.refItem.GetType() == typeof(ItemFurniture)) { continue; }
                var go = Instantiate(inventoryItemFab, inventoryHolder);
                go.GetComponent<MenuInventoryItem>().Setup(item);
            }
        }

        UpdateStats();
    }

    public void ItemClicked(MenuInventoryItem clickedItem)
    {
        //if item is of class itemFurniture do nothing
        if (clickedItem.thisItem.refItem.GetType() == typeof(ItemFurniture)) 
        { 
            return; 
        }
        if (clickedItem.thisItem.refItem.GetType() == typeof(ItemConsumable)) 
        { 
            clickedItem.RemoveThis();
            return; 
        }
    }

    public void UpdateStats()
    {
        strStatText.text = "Strength: " + PlayerMaster.I.stats.GetStrength().ToString();
        agiStatText.text = "Agility: " + PlayerMaster.I.stats.GetAgility().ToString();
        intStatText.text = "Intelligence: " + PlayerMaster.I.stats.GetIntelligence().ToString();
        chaStatText.text = "Charisma: " + PlayerMaster.I.stats.GetCharisma().ToString();
    }
}
