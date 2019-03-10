using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Variable
{
    [SerializeField] private string title;
    [SerializeField] private int intValue;
    [SerializeField] private bool boolValue;

 
    
    public dynamic GetValue()
    {
        if (intValue != -1) return intValue;
        return boolValue;

    }
    
    
    
    public string Title
    {
        get => title;
        set => title = value;
    }

    public int Value
    {
        get => intValue;
        set => this.intValue = value;
    }
}
