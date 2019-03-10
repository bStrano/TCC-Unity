using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConditionalPanel : MonoBehaviour
{
    private bool conditionMode;
    private List<string> variables = new List<string>();
    [SerializeField] private CodeOutputPanel codeOutputPanel;
    [SerializeField] private TMP_InputField valuesInput1;
    [SerializeField] private TMP_InputField valuesInput2;
    [SerializeField] private TMP_Dropdown valuesDropdown1;
    [SerializeField] private TMP_Dropdown valuesDropdown2;
    [SerializeField] private TMP_Dropdown conditionsDropdown;


    private readonly List<string> conditions = new List<string>()
    {
        "Diferente ( != )",
        "Igual ( == )",
        "Maior ( > )",
        "Menor ( < )"
    };

    // Start is called before the first frame update
    void Start()
    {
        foreach (var variable in GameManager.instance.Variables)
        {
            variables.Add(variable.Title);
        }

        variables.Add("Digite um valor");
        valuesDropdown1.AddOptions(variables);
        valuesDropdown2.AddOptions(variables);
        conditionsDropdown.AddOptions(conditions);
        valuesInput1.keyboardType = TouchScreenKeyboardType.NumberPad;
        valuesInput2.keyboardType = TouchScreenKeyboardType.NumberPad;
    }

    public void OnDropdownValuesChanged(int dropdownNumber)
    {
        TMP_Dropdown dropdown;
        TMP_InputField inputField;
        if (dropdownNumber == 0)
        {
            dropdown = valuesDropdown1;
            inputField = valuesInput1;
        }
        else if (dropdownNumber == 1)
        {
            dropdown = valuesDropdown2;
            inputField = valuesInput2;
        }
        else
        {
            dropdown = conditionsDropdown;
            inputField = null;
        }

        if (dropdown.value != 0)
        {
            if (dropdownNumber == 0 || dropdownNumber == 1)
            {
                dropdown.GetComponent<Image>().color = new Color32(212, 216, 236, 255);
            }
            else
            {
                dropdown.GetComponent<Image>().color = new Color32(254, 255, 165, 255);
            }
        }

        //var val = GameManager.instance.Variables.ElementAt(valuesDropdown1.value - 1).GetValue();
        if (inputField == null) return;
        if (CheckDropdownIsCustomMessage(dropdown))
        {
            inputField.gameObject.SetActive(true);
        }
        else
        {
            inputField.text = "";
            inputField.gameObject.SetActive(false);
        }

        //Debug.Log(val);
    }

    private bool CheckIsValid()
    {
        reset();
        bool valuesDropdown1IsValid = CheckDropdownValueWasPicked(valuesDropdown1);
        bool valuesDropdown2IsValid = CheckDropdownValueWasPicked(valuesDropdown2);
        bool conditionsDropdownIsValid = CheckDropdownValueWasPicked(conditionsDropdown);
        return (valuesDropdown1IsValid && valuesDropdown2IsValid && conditionsDropdownIsValid);
    }


    private bool CheckDropdownValueWasPicked(TMP_Dropdown dropdown)
    {
        if (dropdown.value == 0)
        {
            SetErrorColor(dropdown.GetComponent<Image>());
            return false;
        }

        return true;
    }

    private bool CheckIfTextInputIsEmpty()
    {
        bool isValid = true;
        if (CheckDropdownIsCustomMessage(valuesDropdown1) && valuesInput1.text == "")
        {
            SetErrorColor(valuesInput1.GetComponent<Image>());
            isValid = false;
        }

        if (CheckDropdownIsCustomMessage(valuesDropdown2) && valuesInput2.text == "")
        {
            SetErrorColor(valuesInput2.GetComponent<Image>());
            isValid = false;
        }

        return isValid;
    }

    private void reset()
    {
        valuesDropdown1.GetComponent<Image>().color = new Color32(212, 216, 236, 255);
        valuesDropdown2.GetComponent<Image>().color = new Color32(212, 216, 236, 255);
        conditionsDropdown.GetComponent<Image>().color = new Color32(254, 255, 165, 255);
    }


    private void SetErrorColor(Image image)
    {
        image.color = new Color32(219, 60, 56, 100);
    }


    private bool CheckDropdownIsCustomMessage(TMP_Dropdown dropdown)
    {
        return dropdown.value == GameManager.instance.Variables.Count + 1;
    }


    public void OnSave()
    {
        if (CheckIsValid())
        {
            string condition = conditionsDropdown.options[conditionsDropdown.value].text;
            condition = condition.Substring(condition.LastIndexOf('(') + 1);
            condition = condition.Substring(0, condition.Length - 1);
            string statement = valuesDropdown1.options[valuesDropdown1.value].text
                               + condition
                               + valuesDropdown2.options[valuesDropdown2.value].text;
            Debug.Log(statement);
            codeOutputPanel.HandleCommands(Command.If, statement);
            gameObject.SetActive(false);
            GameManager.instance.ConditionalMode = 1;
        }
    }


    public bool CheckStatement(string statement)
    {
        Debug.Log("CheckStatement");

        statement = statement.Substring(statement.LastIndexOf('(') + 1);
        int lastIndex = statement.LastIndexOf(')');
        statement = statement.Substring(0, lastIndex);
        var firstArgument = statement.Split(' ')[0];
        var condition = statement.Split(' ')[1];
        var secondArgument = statement.Substring(statement.LastIndexOf(' ') + 1);


        var firstArgumentValue = GameManager.instance.Variables.Find(item => item.Title == firstArgument).GetValue();
        var secondArgumentValue = GameManager.instance.Variables.Find(item => item.Title == secondArgument).GetValue();
        Debug.Log("ARGUMENTVALUE: " + firstArgumentValue );
        Debug.Log("ARGUMENTVALUE: " + secondArgumentValue );
        switch (condition)
        {
            case "!=":
                return firstArgumentValue != secondArgumentValue;
            case "==":
                return firstArgumentValue == secondArgumentValue;
            case ">":
                return firstArgumentValue > secondArgumentValue;
            case "<":
                return firstArgumentValue < secondArgumentValue;
            default:
                return false;
        }

      
    }

    // Update is called once per frame
    void Update()
    {
    }
}