using System;

[Serializable]
public class PlayerStats
{
    //cash ends with cents IE 100 is $1.00
    private int cash = 10000;

    private int strength = 1;
    private int charisma = 1;
    private int agility = 1;
    private int intelligence = 1;

    public bool blackMarketUnlocked = false;

    public bool[] apartmentsOwned = new bool[] { true, false, false, false, false, false, false, false, false };

    public int[] jobLevels = new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public int[] jobWorkTimes = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

    public int GetCash() { return cash; }
    public bool TakeCash(int ammount) { if(ammount > cash) { return false; } else { cash -= ammount; return true; } }
    public void TakeCashDebtable(int ammount) { cash -= ammount; }
    public void AddCash(int ammount) { cash += ammount; }

    public int GetJobLevel(int jobID) { return jobLevels[jobID]; }

    public int GetStrength() { return strength; }
    public int GetCharisma() { return charisma; }
    public int GetAgility() { return agility; }
    public int GetIntelligence() { return intelligence; }

    public void IncreaseStrength(int ammount) { strength += ammount; NotifcationManager.I.NotifyPlayer("Strength increased by: " + ammount); }
    public void IncreaseCharisma(int ammount) { charisma += ammount; NotifcationManager.I.NotifyPlayer("Charisma increased by: " + ammount); }
    public void IncreaseAgility(int ammount) { agility += ammount; NotifcationManager.I.NotifyPlayer("Agility increased by: " + ammount); }
    public void IncreaseIntelligence(int ammount) { intelligence += ammount; NotifcationManager.I.NotifyPlayer("Intelligence increased by: " + ammount); }

    public bool GetApartmentUnlock(int apartmentNum)
    {
        return apartmentsOwned[apartmentNum];
    }

    public PlayerStats(int cash, int strength, int charisma, int agility, int intelligence, bool blackMarketUnlocked, bool[] apartmentsOwned, int[] jobLevels)
    {
        this.cash = cash;
        this.strength = strength;
        this.charisma = charisma;
        this.agility = agility;
        this.intelligence = intelligence;
        this.blackMarketUnlocked = blackMarketUnlocked;
        this.apartmentsOwned = apartmentsOwned;
        this.jobLevels = jobLevels;
    }

    public PlayerStats()
    {
        this.cash = 10000;
        this.strength = 1;
        this.charisma = 1;
        this.agility = 1;
        this.intelligence = 1;

        this.blackMarketUnlocked = false;
        this.apartmentsOwned = new bool[] { true, false, false, false, false, false, false, false, false };
        this.jobLevels = new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; ;
    }
}
