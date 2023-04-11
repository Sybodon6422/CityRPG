using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    #region singleton

    private static HUDManager _instance;
    public static HUDManager I { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    #endregion

    [SerializeField] private ShopInterface shopInterface;
    [SerializeField] private BankInterface bankInterface;
    [SerializeField] private BuyHouseMenu buyHouseMenu;
    [SerializeField] private NPCInteractionMenu nPCInteractionMenu;
    [SerializeField] private InventoryHUD inventoryHUD;

    [SerializeField] GameObject mainHud, newDayMenu, ShopHud;
    void Start()
    {
        mainHud.SetActive(true);
    }

    [SerializeField] HealthBar healthBar;
    public void UpdateHealthBar(int curHealth, int maxHealth) { healthBar.UpdateHealthBarText(curHealth, maxHealth); }

    public void OpenShopMenu(Job _job, Service[] _servicesOffered, ItemInShop[] _itemsForSale)
    {
        shopInterface.gameObject.SetActive(true);
        shopInterface.ShopOpened(_job,_servicesOffered,_itemsForSale);
    }

    public void OpenNPCMenu(string baseText, NonPlayerCharacter npc, ItemInShop soldItem)
    {

    }
    public void OpenInventory(List<InventoryItem> _inventory)
    {
        inventoryHUD.OpenInventory(_inventory);
    }
    private float wakeTime;
    public void OpenEndofDayMenu(float timeToWake)
    {
        wakeTime = timeToWake;
        mainHud.SetActive(false);
        newDayMenu.SetActive(true);
    }

    public void StartNewDay()
    {
        mainHud.SetActive(true);
        newDayMenu.SetActive(false);

        GameManager.I.StartNewDay(wakeTime);
    }

    public void OpenBuyHouseMenu(string _nameT, int _houseCost, int _houseID)
    {
        buyHouseMenu.OpenMenu(_nameT, _houseCost, _houseID);
        buyHouseMenu.gameObject.SetActive(true);
    }
    public NPCInteractionMenu GetNPCInteractionMenu() { return nPCInteractionMenu; }
    public ShopInterface GetShopInterface() { return shopInterface; }
    public BankInterface GetBankInterface() { return bankInterface; }
    public InventoryHUD GetInventoryHUD() { return inventoryHUD; }
}
