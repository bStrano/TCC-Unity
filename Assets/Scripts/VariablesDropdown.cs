using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VariablesDropdown : MonoBehaviour
{
    private TMP_Dropdown dropdown;
    // Start is called before the first frame update
    void Start()
    {
         dropdown = gameObject.GetComponent<TMP_Dropdown>();

         List<string> options = new List<string>();
        foreach (var variable in GameManager.instance.Variables)
        {
           options.Add(variable.Title);
            
        }

        dropdown.AddOptions(options);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
