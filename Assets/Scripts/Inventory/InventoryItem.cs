using System;


[Serializable]
public class InventoryItem
{
    public Item refItem;

    public InventoryItem(Item _item)
    {
        refItem = _item;
    }
}
