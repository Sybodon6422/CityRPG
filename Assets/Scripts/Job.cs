using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(fileName = "NewJob", menuName = "GameStuff/New Job", order = 1)]
public class Job : ScriptableObject
{
    public JobLevel[] jobLevels;
    public string jobBaseName;
    [SerializeField]private int jobID;
    public int jobTimeTaken = 120;

    public int GetJobID() { return jobID; }

    public void GetLevelRequirements(int levelCur)
    {

    }

    [Serializable]
    public class JobLevel
    {
        public string jobTitle;
        public int jobWage;

        public int strlevel;
        public int chrlevel;
        public int agllevel;
        public int intlevel;

        public string[] jobEventText;
        public RandomJobEvent[] randomJobEvents;
        public int workTimes;

        public String Work(PlayerMaster player)
        {
        string textToReturn = jobEventText[UnityEngine.Random.Range(0, jobEventText.Length)];
        
        for (int i = 0; i < randomJobEvents.Length; i++)
        {
            if(UnityEngine.Random.Range(0, 100) <= randomJobEvents[i].jobEventChance)
            {
                textToReturn = randomJobEvents[i].jobEventText;
                switch (randomJobEvents[i].jobEventType)
                {
                    case RandomJobEvent.JobEventType.statChange:
                        switch (randomJobEvents[i].changeBy)
                        {
                            case 0:
                                player.stats.IncreaseStrength(1);
                                break;
                            case 1:
                                player.stats.IncreaseCharisma(1);
                                break;
                            case 2:
                                player.stats.IncreaseAgility(1);
                                break;
                            case 3:
                                player.stats.IncreaseIntelligence(1);
                                break;
                            default:
                                break;
                        }
                        break;
                    case RandomJobEvent.JobEventType.healthChange:
                        player.stats.TakeCashDebtable(randomJobEvents[i].changeBy);
                        break;
                    case RandomJobEvent.JobEventType.cashChange:
                        player.stats.AddCash(randomJobEvents[i].changeBy);
                        break;
                    default:
                        break;

                }
                return textToReturn;
            }
        }
        return textToReturn;
        }

        public bool MeetsRequirements(PlayerStats stats, int currentWorkTimes)
        {
            bool promoted = true;
            string printText = "You need ";
            if (stats.GetStrength() < strlevel) {
                printText += strlevel + " strength, you only have: " + stats.GetStrength();
                promoted = false;
            }
            if (stats.GetCharisma() < chrlevel) {
                printText += chrlevel + " charisma, you only have: " + stats.GetCharisma();
                promoted = false;
            }
            if (stats.GetAgility() < agllevel) {
                printText += agllevel + " agility, you only have: " + stats.GetAgility();
                promoted = false;
            }
            if (stats.GetIntelligence() < intlevel) {
                printText += intlevel + " intelligence, you only have: " + stats.GetIntelligence();
                promoted = false;
            }
            if(workTimes == 0)
            {

            }
            else
            {
                if (workTimes > currentWorkTimes)
                {
                    printText = "You could stand to work some more before asking for a promotion";
                    NotifcationManager.I.NotifyPlayer(printText);
                    return false;
                }
            }
            if (promoted)
            {
                printText += "to be promoted";
            }
            NotifcationManager.I.NotifyPlayer(printText);
            return promoted;
        }
    }

    [Serializable]
    public class RandomJobEvent
    {
        public string jobEventText;
        public float jobEventChance; // 000.00 - 100.00
        public JobEventType jobEventType;
        public int changeBy;
        public enum JobEventType
        {
            statChange,
            healthChange,
            cashChange,

        }
    }
}