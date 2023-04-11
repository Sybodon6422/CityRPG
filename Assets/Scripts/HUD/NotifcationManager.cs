using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class NotifcationManager : MonoBehaviour
{
    #region singleton
    private static NotifcationManager _instance;

    public static NotifcationManager I { get { return _instance; } }
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


    [SerializeField] GameObject popUp;
    [SerializeField] Transform notifcationHolder;
    [SerializeField] TextMeshProUGUI mainNotifcationText;
    [SerializeField] GameObject mainNoticationPiece;

    public void PopUpText(string _text)
    {
        mainNoticationPiece.SetActive(true);
        mainNotifcationText.text = _text;
        PopUpTextHideAfter(5);
    }
    private GameObject notifcationSpawned;
    public void NotifyPlayer(string _text)
    {
        if (notifcationSpawned) { Destroy(notifcationSpawned); }
        notifcationSpawned = Instantiate(popUp, notifcationHolder);
        notifcationSpawned.GetComponent<NotifcationPiece>().SetupPiece(_text, GameManager.I.GetTime()
            );
    }

    #region timekeeping

    private void PopUpTextHideAfter(float seconds)
    {
        waitSeconds = seconds;
        currentSeconds = 0;
        waiting = true;
    }

    private float currentSeconds;
    private float waitSeconds;
    private bool waiting = false;

    private void Update()
    {
        if (waiting)
        {
            currentSeconds += Time.deltaTime;
            if(currentSeconds >= waitSeconds)
            {
                waiting = false;
                waitSeconds = 0;
                currentSeconds = 0;
                HidePopUpText();
            }
        }
    }

    private void HidePopUpText()
    {
        mainNoticationPiece.SetActive(false);
    }

    #endregion

}
