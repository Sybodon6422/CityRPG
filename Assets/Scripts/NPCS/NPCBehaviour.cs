using UnityEngine;

[CreateAssetMenu(fileName = "NewNPCBehaviour", menuName = "GameStuff/NPCS/Behaviour", order = 1)]
public class NPCBehaviour : ScriptableObject
{
    public BehaviourType behaviourType;
    public enum BehaviourType
    {
        //says a phrase
        simple,

    }

    public void GetBehaviour()
    {

    }
}
