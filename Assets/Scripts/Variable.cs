using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Variable
{
    [SerializeField] private CEnemy enemy;
    [SerializeField] private string title;
    [SerializeField] private int intValue;
    [SerializeField] private bool boolValue;


    [SerializeField] private bool isInt;
    [SerializeField] private bool isBool;
    [SerializeField] private bool isEnemyHealth;

 
    
    public dynamic GetValue()
    {
        if (isInt)
        {
            return intValue;
        }

        if (isBool)
        {
            return boolValue;
        }

        if (isEnemyHealth)
        {
            return enemy.MaxHealth;
        }

        return null;
//        if (intValue != -1) return intValue;
//        return boolValue;

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
