using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuyHouseMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameText, priceText;
    [SerializeField] GameObject buyHouseButton;
    private int houseID;
    private int cost;

    public void OpenMenu(string _nameT,int _houseCost, int _houseID)
    {
        houseID = _houseID;
        if (PlayerMaster.I.stats.apartmentsOwned[houseID])
        {
            nameText.text = "SOLD";
            cost = 0;
            priceText.text = (_houseCost / 100m).ToString();
            return;
        }
        else
        {
            nameText.text = _nameT;
            priceText.text = "$"+(_houseCost/100m).ToString();
            cost = _houseCost;
        }
    }

    public void BuyHouse()
    {
        if(cost == 0) { return; }

        if (PlayerMaster.I.stats.TakeCash(cost))
        {
            PlayerMaster.I.stats.apartmentsOwned[houseID] = true;
            Leave();
        }
    }

    public void Leave()
    {
        PlayerMaster.I.UpdateMoneyText();
        gameObject.SetActive(false);
    }
}
