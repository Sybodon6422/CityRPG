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

        public int workTimes;

        public bool MeetsRequirements(PlayerStats stats, int currentWorkTimes)
        {
            bool promoted = true;
            string printText = "You need ";
            if (stats.GetStrength() < strlevel) {
                printText += strlevel + " strength ";
                promoted = false;
            }
            if (stats.GetCharisma() < chrlevel) {
                printText += chrlevel + " charisma ";
                promoted = false;
            }
            if (stats.GetAgility() < agllevel) {
                printText += agllevel + " agility ";
                promoted = false;
            }
            if (stats.GetIntelligence() < intlevel) {
                printText += intlevel + " intelligence ";
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
}