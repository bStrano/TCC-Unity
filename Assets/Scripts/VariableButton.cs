using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VariableButton : MonoBehaviour
{
    [SerializeField] private Text titleText;

    public Text TitleText
    {
        get { return titleText; }
        set { titleText = value; }
    }


    // Use this for initialization
    void Start ()
    {
      
    }




}
