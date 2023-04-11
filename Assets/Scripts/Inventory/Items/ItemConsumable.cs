using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "GameStuff/Items/ConsumableItem", order = 1)]
public class ItemConsumable : Item
{
    public ItemEffect effect;

   [Serializable]
    public class ItemEffect
    {
        public int ammount;
        public EffectType type;
        public enum EffectType
        {
            StatIncrease,
            HealthRegen,
        }
    }
}
