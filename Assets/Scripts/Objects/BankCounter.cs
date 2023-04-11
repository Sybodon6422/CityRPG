using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankCounter : MonoBehaviour,IEnteractable
{
    public void AttemptEnteract(PlayerMaster player)
    {
        HUDManager.I.GetBankInterface().OpenBank();
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
