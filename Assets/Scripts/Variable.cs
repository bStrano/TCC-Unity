using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Variable
{
    [SerializeField] private string title;
    [SerializeField] private int value;

    public string Title
    {
        get { return title; }
        set { title = value; }
    }

    public int Value
    {
        get { return value; }
        set { this.value = value; }
    }
}
