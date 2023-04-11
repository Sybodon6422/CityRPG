using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : Furniture,IEnteractable
{
    public void AttemptEnteract(PlayerMaster player)
    {
        GameManager.I.EndDay(false, GetComponentInParent<ApartmentCell>());
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
