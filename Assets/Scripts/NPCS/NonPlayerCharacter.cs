using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerCharacter : MonoBehaviour, IEnteractable
{
    [SerializeField] string baseText;
    [SerializeField] ItemInShop soldItem;


    public void AttemptEnteract(PlayerMaster player)
    {
        HUDManager.I.GetNPCInteractionMenu().OpenMenuShop(baseText, this, soldItem);
    }
}
