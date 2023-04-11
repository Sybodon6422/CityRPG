using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "GameStuff/Items/Furniture Item", order = 1)]
public class ItemFurniture : Item
{
    public int wakeupMod;
    public int furnitureLevelMod;

    public Furniture.FurnitureType type;
    public GameObject furnitureFab;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
