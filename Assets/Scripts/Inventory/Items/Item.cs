using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "GameStuff/Items/New Item", order = 1)]
public class Item : ScriptableObject
{
    public Sprite itemIcon;
    public string itemName;

    public bool inventoryHide = false;
    public virtual void ItemClicked()
    {

    }
}
