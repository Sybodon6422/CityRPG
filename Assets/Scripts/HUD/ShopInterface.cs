using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using TMPro;
using Unity.Jobs;
using UnityEngine;

public class ShopInterface : MonoBehaviour
{
    private PlayerMaster player;

    #region refs

    [SerializeField] private TextMeshProUGUI workButtonText, jobTitleSection;
    [SerializeField] private GameObject workMenu, promotionButton,askForJobButton;

    [SerializeField] private GameObject serviceMenu, itemsMenu;

    [SerializeField] private GameObject menuItemFAB, menuServiceFAB;

    private Service[] servicesOffered;
    private ItemInShop[] itemsForSale;
    private int hourlyRate;
    private Job currentJob;

    #endregion

    #region setupInterface

    public void ShopOpened(Job _job, Service[] _servicesOffered, ItemInShop[] _itemsForSale)
    {
        player = GameManager.I.GetPlayer();

        servicesOffered = null;
        currentJob = null;
        itemsForSale = null;

        //setupWorkRegion
        if (_job != null)
        {
            currentJob = _job;
            int jobIntLevel = player.stats.jobLevels[_job.GetJobID()];
            if(jobIntLevel - 1 == -1)
            {
                workMenu.SetActive(false);
                askForJobButton.SetActive(true);
            }
            else
            {
                askForJobButton.SetActive(false);
                var joblevel = _job.jobLevels[jobIntLevel - 1];
                SetupWorkButton(joblevel);
            }
        }
        else { workMenu.SetActive(false); askForJobButton.SetActive(false); }
        if (_servicesOffered != null)
        {
            servicesOffered = _servicesOffered;
            PopulateServicesMenu();
        }
        else { serviceMenu.SetActive(false); }
        if (_itemsForSale != null)
        {
            itemsForSale = _itemsForSale;
            PopulateItemsMenu();
        }
        else { itemsMenu.SetActive(false); }
    }

    private void ShopReload()
    {
        ShopOpened(currentJob, servicesOffered, itemsForSale);
    }

    public void SetupWorkButton(Job.JobLevel job)
    {
        workMenu.SetActive(true);
        jobTitleSection.text = job.jobTitle;
        hourlyRate = job.jobWage;
        workButtonText.text = (job.jobWage/100m).ToString("C") + "/h";

        int jobID = currentJob.GetJobID();
        int playerJoblevelCurrent = player.stats.jobLevels[jobID];

        if (playerJoblevelCurrent >= currentJob.jobLevels.Length)
        {
            promotionButton.SetActive(false);
            return;
        }
        else
        {
            promotionButton.SetActive(true);
        }
    }

    public void PopulateItemsMenu()
    {
        for (int i = 0; i < itemsMenu.transform.childCount; i++)
        {
            Destroy(itemsMenu.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < itemsForSale.Length; i++)
        {
            var go = Instantiate(menuItemFAB, itemsMenu.transform);
            go.GetComponent<MenuItemButton>().Setup(itemsForSale[i]);
        }

        itemsMenu.SetActive(true);
    }

    public void PopulateServicesMenu() 
    {
        for (int i = 0; i < serviceMenu.transform.childCount; i++)
        {
            Destroy(serviceMenu.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < servicesOffered.Length; i++)
        {
            var go = Instantiate(menuServiceFAB, serviceMenu.transform);
            go.GetComponent<MenuServiceButton>().Setup(servicesOffered[i]);
        }

        serviceMenu.SetActive(true);
    }

    #endregion

    public void LeaveShop()
    {
        gameObject.SetActive(false);
        player.PlayerLeftShop();
    }

    public void Work()
    {
        int workMinutes = currentJob.jobTimeTaken;

        if (GameManager.I.CanSkipTime(workMinutes))
        {
            GameManager.I.SkipTime(workMinutes);
            player.stats.jobWorkTimes[currentJob.GetJobID()] += 1;
            int workCash = (hourlyRate * (workMinutes / 60));
            //int tipAmmount = UnityEngine.Random.Range(0 + player.stats.GetCharisma(), 1500 + player.stats.GetCharisma());
            player.stats.AddCash(workCash);

            NotifcationManager.I.NotifyPlayer("You Worked for 2 hours and recieved $" + (workCash/100m).ToString("C"));

            player.UpdateMoneyText();
        }
    }

    public void AskForPromotion()
    {
        int jobID = currentJob.GetJobID();
        int playerJoblevelCurrent = player.stats.jobLevels[jobID];

        if(playerJoblevelCurrent >= currentJob.jobLevels.Length)
        {
            promotionButton.SetActive(false);
            return;
        }
        else
        {
            promotionButton.SetActive(true);
        }

        if (currentJob.jobLevels[playerJoblevelCurrent].MeetsRequirements(player.stats, player.stats.jobWorkTimes[currentJob.GetJobID()]))
        {
            player.stats.jobLevels[jobID] += 1;
            ShopReload();
            NotifcationManager.I.NotifyPlayer("You've been promoted!");
        }

        playerJoblevelCurrent = player.stats.jobLevels[jobID];

        if (playerJoblevelCurrent >= currentJob.jobLevels.Length)
        {
            promotionButton.SetActive(false);
            return;
        }
    }

    public void BuyItem(ItemInShop item)
    {
        if (player.stats.TakeCash(item.cost))
        {
            NotifcationManager.I.NotifyPlayer("Player bought item for " + (item.cost/100m).ToString("C"));
            player.inventory.AddItemToInventory(item.item);
            player.UpdateMoneyText();
        }
        else
        {
            NotifcationManager.I.NotifyPlayer("Not enough money");
        }
    }

    public void BuyService(Service _service)
    {
        if (GameManager.I.CanSkipTime(_service.serviceTime))
        {
            if (player.stats.TakeCash(_service.cost))
            {
                GameManager.I.SkipTime(_service.serviceTime);
                if(_service.increaseIntelligence != 0)player.stats.IncreaseIntelligence(_service.increaseIntelligence);
                if (_service.increaseAgility != 0) player.stats.IncreaseAgility(_service.increaseAgility);
                if (_service.increaseCharisma != 0) player.stats.IncreaseCharisma(_service.increaseCharisma);
                if (_service.increaseStrength != 0) player.stats.IncreaseStrength(_service.increaseStrength);
                player.UpdateMoneyText();
            }
            else
            {
                NotifcationManager.I.NotifyPlayer("Not enough money");
            }
        }
    }
}
