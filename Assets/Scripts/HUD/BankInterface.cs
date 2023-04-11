using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BankInterface : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI moneyField;

    public void OpenBank()
    {
        PlayerMaster.I.PlayerEnteractedWithShop();
        gameObject.SetActive(true);
        UpdateBankScreen();
    }


    public void StoreMoney(int ammount)
    {
        if (PlayerMaster.I.stats.TakeCash(ammount))
        {
            GameManager.I.StoreMoney(ammount);
        }
        UpdateBankScreen();
    }
    public void TakeMoney(int ammount)
    {
        if (GameManager.I.TakeMoney(ammount))
        {
            PlayerMaster.I.stats.AddCash(ammount);
        }
        UpdateBankScreen();
    }
    public void LeaveShop()
    {
        gameObject.SetActive(false);
        PlayerMaster.I.PlayerLeftShop();
    }

    void UpdateBankScreen()
    {
        int bankedMoney = GameManager.I.GetStoredMoney();
        moneyField.text = (bankedMoney / 100m).ToString();
        PlayerMaster.I.UpdateMoneyText();
    }
}
