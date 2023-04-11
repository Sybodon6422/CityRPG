using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furniture : MonoBehaviour
{
    public ItemFurniture furnitureItem;

    public void Replace(ItemFurniture newFurniture)
    {
        furnitureItem = newFurniture;
        Destroy(transform.GetChild(0).gameObject);
        var go = Instantiate(furnitureItem.furnitureFab,transform.localPosition,transform.localRotation,transform);
        go.transform.localPosition = Vector3.zero;
        go.name = "FurnitureBody: " + newFurniture.itemName;
    }

    public enum FurnitureType
    {
        Bed,
        Couch,
        AlarmClock,
        Dresser,
        TV,
        Computer
    }
}
