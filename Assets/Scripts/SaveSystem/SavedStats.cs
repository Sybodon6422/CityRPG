using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SavedStats
{
    public string saveName;

    public int cash = 10000;

    public int strength = 1;
    public int charisma = 1;
    public int agility = 1;
    public int intelligence = 1;

    public bool blackMarketUnlocked = false;

    public bool[] apartmentsOwned = new bool[] { true, false, false, false, false, false, false, false, false };
    public int[] jobLevels = new int[] { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};

    public SavedStats(PlayerStats _stats, string _saveName)
    {
        this.saveName = _saveName;
        this.cash = _stats.GetCash();

        this.strength = _stats.GetStrength();
        this.charisma = _stats.GetCharisma();
        this.agility = _stats.GetAgility();
        this.intelligence = _stats.GetIntelligence();

        this.blackMarketUnlocked = _stats.blackMarketUnlocked;

        this.apartmentsOwned = _stats.apartmentsOwned;
        this.jobLevels = _stats.jobLevels;
    }
}
