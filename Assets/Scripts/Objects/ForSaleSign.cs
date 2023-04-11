using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForSaleSign : MonoBehaviour,IEnteractable
{
    public string nameText;
    public int houseCost;
    public int houseID;

    public void AttemptEnteract(PlayerMaster player)
    {
        HUDManager.I.OpenBuyHouseMenu(nameText, houseCost, houseID);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
