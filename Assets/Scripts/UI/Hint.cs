using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hint : MonoBehaviour {
    [SerializeField] private string title;
    [SerializeField] private string message;

    public string Title
    {
        get
        {
            return title;
        }

        set
        {
            title = value;
        }
    }

    public string Message
    {
        get
        {
            return message;
        }

        set
        {
            message = value;
        }
    }
}
