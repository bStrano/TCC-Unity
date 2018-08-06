using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



[System.Serializable]
public class Function : CodeOutputPanel {

    private string name;

    public void Cancel()
    {
        this.commands.Commands.Clear();
        foreach( GameObject button in buttons)
        {
            Destroy(button);
        }
    }


       
}
