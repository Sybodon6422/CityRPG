using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopCounter : MonoBehaviour,IEnteractable
{
    public Job job;

    public Service[] servicesOffered;
    public ItemInShop[] itemsForSale;

    public void AttemptEnteract(PlayerMaster player)
    {
        HUDManager.I.OpenShopMenu(job,servicesOffered,itemsForSale);
        player.PlayerEnteractedWithShop();
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
