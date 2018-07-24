using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level  {

    [SerializeField]
    private int comandsAvaiable;
    [SerializeField]
    private bool unlocked;

    public int ComandsAvaiable
    {
        get
        {
            return comandsAvaiable;
        }

        set
        {
            comandsAvaiable = value;
        }
    }
}
