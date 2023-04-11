using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour,IEnteractable
{
    [SerializeField]private IndependentCell master;
    public bool leave = false;
    public bool toWorld = true;

    public Vector2 doorModFix = Vector2.zero;

    public Vector2 GetPosition()
    {
        return transform.position;
    }

    public virtual bool Unlock()
    {
        return true;
    }

    public void AttemptEnteract(PlayerMaster player)
    {
        if (leave) 
        { 
            GameManager.I.ExitCell(master, toWorld); 
            return;
        }
        if (Unlock())
        {
            GameManager.I.EnterCell(master);
        }
    }
}
