using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipPanel : MonoBehaviour {
    [SerializeField] private Text titleText;
    [SerializeField] private Text messageText;

    public Text TitleText
    {
        get
        {
            return titleText;
        }

        set
        {
            titleText = value;
        }
    }

    public Text MessageText
    {
        get
        {
            return messageText;
        }

        set
        {
            messageText = value;
        }
    }
}
