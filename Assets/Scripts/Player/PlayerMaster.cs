using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMaster : MonoBehaviour
{
    #region singleton
    private static PlayerMaster _instance;

    public static PlayerMaster I { get { return _instance; } }
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


    public PlayerMovement mover;
    public PlayerStats stats;
    public PlayerInventory inventory;
    public TextMeshProUGUI moneyText;

    private int currentHealth;
    private int maxHealth = 10;

    private void Start()
    {
        stats ??= new PlayerStats();
        currentHealth = maxHealth;
        mover.WakeUp();
        UpdateMoneyText();
        UpdateStats();
        HUDManager.I.UpdateHealthBar(currentHealth,maxHealth);
    }

    public void UpdateStats()
    {
        mover.IncreaseStats();
        maxHealth = 10 + Mathf.FloorToInt(stats.GetStrength() * .5f);
    }

    public void TeleportPlayer(Vector3 worldPos, Quaternion rotation)
    {
        mover.SetPosition(worldPos, rotation);
    }

    public void PlayerEnteractedWithShop()
    {
        mover.LockMovement(false);
        Time.timeScale = 0;
        UpdateMoneyText();
    }
    public void PlayerLeftShop()
    {
        mover.LockMovement(true);
        Time.timeScale = 1;
        UpdateMoneyText();
    }

    public void EnterVehicle()
    {
        mover.enabled = false;
    }

    public void UpdateMoneyText()
    {
        var money = stats.GetCash();
        if(money < 0)
        {
            moneyText.color = Color.red;
        }
        else
        {
            moneyText.color = Color.black;
        }
        moneyText.text = ((stats.GetCash()/100m).ToString("C"));
    }
}
