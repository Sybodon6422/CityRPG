using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHUD : MonoBehaviour
{
    [SerializeField] Transform inventoryHolder;
    [SerializeField] GameObject inventoryItemFab;
    public void OpenInventory(List<InventoryItem> inventory)
    {
        PlayerMaster.I.PlayerEnteractedWithShop();
        gameObject.SetActive(true);
        for (int i = 0; i < inventoryHolder.childCount; i++)
        {
            Destroy(inventoryHolder.GetChild(i).gameObject);
        }
        foreach(InventoryItem item in inventory)
        {
            if (!item.refItem.inventoryHide)
            {
                var go = Instantiate(inventoryItemFab, inventoryHolder);
                go.GetComponent<MenuInventoryItem>().Setup(item);
            }
        }
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
        foreach (InventoryItem item in inventory)
        {
            var go = Instantiate(inventoryItemFab, inventoryHolder);
            go.GetComponent<MenuInventoryItem>().Setup(item);
        }
    }

    public void ItemClicked(MenuInventoryItem clickedItem)
    {
        clickedItem.RemoveThis();
    }
}
