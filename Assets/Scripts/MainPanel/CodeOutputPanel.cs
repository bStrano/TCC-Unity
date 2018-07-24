using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CodeOutputPanel : MonoBehaviour {

    [SerializeField]
    private GameObject button;
    [SerializeField]
    private Transform contentPanel;


  

    public void TranslateCommandToCode(Command command, int lineNumber)
    {
        
       // foreach (Command command in commandsPanel.Comands)
        //{
        if(command != Command.None)
        {
            GameObject newButton = GameObject.Instantiate(button);
            CodeButton codeButton = newButton.GetComponent<CodeButton>();
            codeButton.LineNumber.text = lineNumber.ToString();
            codeButton.CommandName.text = command.ToString();


            newButton.transform.SetParent(contentPanel);
            //
            
        }
            
        //}
    }

	// Use this for initialization
	void Start () {
		//GameObject lineCode = Instantiate
	}
	

}
