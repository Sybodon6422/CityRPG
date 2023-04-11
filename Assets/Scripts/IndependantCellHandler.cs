using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class IndependantCellHandler : MonoBehaviour
{
    #region singleton

    private static IndependantCellHandler _instance;
    public static IndependantCellHandler I { get { return _instance; } }

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

    public GameObject LoadCell(GameObject instance)
    {
        Vector3 buildPos = new Vector3(1000, 1000, 0);
        var go = Instantiate(instance, buildPos, Quaternion.identity, transform);
        return go;
    }
}
