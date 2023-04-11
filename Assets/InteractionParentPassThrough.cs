using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionParentPassThrough : MonoBehaviour,IEnteractable
{
    public void AttemptEnteract(PlayerMaster player)
    {
        transform.parent.GetComponent<IEnteractable>()?.AttemptEnteract(player);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
