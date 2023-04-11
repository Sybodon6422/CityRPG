using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApartmentCell : IndependentCell
{
    public List<Furniture> furniture;
    
    void Start()
    {
        
    }

    public void AddFurniture(List<InventoryItem> inventoryItems)
    {
        foreach (var item in inventoryItems)
        {
            if (item.refItem is ItemFurniture _furnItem)
            {
                furniture[((int)_furnItem.type)]?.Replace(_furnItem);
            }
        }
    }

    public int SleepMod()
    {
        int totalSleepMod = 0;
        foreach (Furniture item in furniture)
        {
            totalSleepMod += item.furnitureItem.wakeupMod;
        }
        return totalSleepMod;
    }

    private void OnEnable()
    {
        AddFurniture(PlayerMaster.I.inventory.GetInventory());
    }
}
