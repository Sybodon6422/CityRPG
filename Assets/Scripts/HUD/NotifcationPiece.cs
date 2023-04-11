using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotifcationPiece : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText,timeText;
    
    public void SetupPiece(string _nameText, string _timeText)
    {
        nameText.text = _nameText;
        timeText.text = _timeText;
    }
}
