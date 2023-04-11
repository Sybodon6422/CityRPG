using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    [SerializeField] Light2D sunLight;
    private PlayerMaster player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMaster>();

        StartNewDay(480);
        SetSunIntensity();
    }

    void FixedUpdate()
    {
        TimeKeeping();
    }

    public PlayerMaster GetPlayer() { return player; }

    #region Singleton
    private static GameManager _instance;

    public static GameManager I { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

#endregion

    #region TimeKeeping
    public float timeScale = 1;

    private float day;
    private float currentTimeSeconds = 5;
    private float lastMinute = -1;
    private void TimeKeeping()
    {
        currentTimeSeconds += Time.deltaTime * timeScale;

        currentHour = Mathf.FloorToInt(currentTimeSeconds / 60);
        minuteCounter = Mathf.FloorToInt((currentTimeSeconds - (currentHour * 60)));
        if(minuteCounter != lastMinute)
        {
            lastMinute = minuteCounter;
            UpdateTimeText();
        }
        if (currentTimeSeconds >= 1000)
        {
            if (currentTimeSeconds >= 1440)
            {
                if(currentTimeSeconds >= 1620)
                {
                    currentTimeSeconds = 0;
                    EndDay(true,null);
                    return;
                }
            }
        }
        
    }
    [SerializeField] TextMeshProUGUI timeText, dayText;
    private float currentHour;
    private float lastHour;
    float minuteCounter;
    void UpdateTimeText()
    {
        currentHour = Mathf.FloorToInt(currentTimeSeconds / 60);
        minuteCounter = Mathf.FloorToInt((currentTimeSeconds - (currentHour * 60)));

        if(currentHour != lastHour)
        {
            lastHour = currentHour;
            HourTrigger();
        }
        if(currentHour > 24) { timeText.color = Color.red; } else { timeText.color = Color.black; }
        timeText.text = GetTime();
    }

    public string GetTime()
    {
        string currentTimeText;
        string minuteCounterString = minuteCounter.ToString();

        if (minuteCounterString.Length < 2) { minuteCounterString = "0" + minuteCounterString; }

        if (currentHour < 1)
        {
            currentTimeText = 12 + ":" + minuteCounterString + " PM";
            return currentTimeText;
        }
        if (currentHour < 13)
        {
            currentTimeText = (currentHour) + ":" + minuteCounterString + " AM";
            return currentTimeText;
        }
        if(currentHour<25)
        {
            currentTimeText = (currentHour - 12).ToString() + ":" + minuteCounterString + " PM";
            return currentTimeText;
        }

        currentTimeText = (currentHour - 24).ToString() + ":" + minuteCounterString + " AM";
        return currentTimeText;
    }
 
    public void EndDay(bool failed, ApartmentCell apartment)
    {
        if (failed)
        {
            player.stats.TakeCashDebtable(5000);
            player.UpdateMoneyText();
            HUDManager.I.OpenEndofDayMenu(630);
        }
        else
        {
            HUDManager.I.OpenEndofDayMenu(480-apartment.SleepMod());
        }
    }

    public void StartNewDay(float startTime)
    {
        CalculateInterest();
        currentTimeSeconds = startTime;
        day += 1;
        SetSunIntensity();
        dayText.text = "Day " + day;
    }

    private bool playerInside = false;
    public void SetSunIntensity()
    {
        UpdateTimeText();
        if (playerInside) { return; }
        sunLight.intensity = sunlightCurve.Evaluate(currentHour);
    }
    public float DayTimeLeft()
    {
        float timeLeft = 1440 - currentTimeSeconds;
        return timeLeft;
    }

    public bool CanSkipTime(float skipMinutes)
    {
        if (DayTimeLeft() >= skipMinutes) return true;
        return false;
    }

    public void SkipTime(float skipMinutes)
    {
        if (CanSkipTime(skipMinutes))
        {
            currentTimeSeconds += skipMinutes;
            SetSunIntensity();
            UpdateTimeText();
        }
    }
    [SerializeField] AnimationCurve sunlightCurve;
    private void HourTrigger()
    {
        SetSunIntensity();
    }

    #endregion

    #region teleporting
    public void EnterCell(IndependentCell cell)
    {

        cell.gameObject.SetActive(true);
        player.TeleportPlayer(cell.cellPoint.GetPosition() + cell.cellPoint.doorModFix, Quaternion.identity);
    }

    public void ExitCell(IndependentCell cell, bool toWorld)
    {
        if (toWorld)
        {
            playerInside = false;
            SetSunIntensity();
        }
        cell.gameObject.SetActive(false);
        player.TeleportPlayer(cell.worldPoint.GetPosition() + cell.worldPoint.doorModFix, Quaternion.identity);
    }
    #endregion

    #region savehandling

    public void SaveGame()
    {
        
    }

    #endregion

    #region banking

    int moneyStored = 0;
    [SerializeField] float dailyInterestPercent;
    public void CalculateInterest()
    {
        moneyStored = Mathf.FloorToInt(moneyStored * dailyInterestPercent);
    }
    public int GetStoredMoney() { return moneyStored; }
    public void StoreMoney(int _money)
    {
        moneyStored += _money;
    }

    public bool TakeMoney(int _money)
    {
        if(moneyStored < _money) { return false; }
        moneyStored -= _money;
        return true;
    }

    #endregion
}
