using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuInventoryItem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] Image itemIcon;
    public InventoryItem thisItem;
    public void Setup(InventoryItem item)
    {
        thisItem = item;

        itemIcon.sprite = item.refItem.itemIcon;
        nameText.text = item.refItem.itemName;
    }
    public void ButtonPressed()
    {
        HUDManager.I.GetInventoryHUD().ItemClicked(this);
    }

    public void RemoveThis()
    {
        PlayerMaster.I.inventory.RemoveInventoryItem(thisItem);
        HUDManager.I.GetInventoryHUD().UpdateInventory();
    }
}
