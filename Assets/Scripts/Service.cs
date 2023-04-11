using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Service
{
    public string serviceText;
    public int cost = 1000;
    public int serviceTime = 120;

    public Sprite serviceIcon;

    public int increaseStrength = 0;
    public int increaseCharisma = 0;
    public int increaseAgility = 0;
    public int increaseIntelligence = 0;
}
