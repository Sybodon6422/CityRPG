using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private List<InventoryItem> inventory;

    private void Start()
    {
        inventory ??= new List<InventoryItem>();
    }
    public void UpdateInventory()
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i] == null) { inventory.RemoveAt(i); }
        }
    }

    public List<InventoryItem> GetInventory() { return inventory; }
    public void AddItemToInventory(Item _item) { inventory.Add(new InventoryItem(_item)); }
    public void RemoveInventoryItem(InventoryItem _itm) { inventory.Remove(_itm); }

}
