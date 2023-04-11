using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApartmentDoor : Door
{
    [SerializeField] private int apartmentID;
    public override bool Unlock()
    {
        return PlayerMaster.I.stats.apartmentsOwned[apartmentID];
    }
}
