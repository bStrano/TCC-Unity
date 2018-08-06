using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CodeOutputPanel : MonoBehaviour {
    [SerializeField] protected GameObject mainPanel;
    [SerializeField] protected CommandsPanel commands;
    [SerializeField] private GameObject button;
    [SerializeField] private RectTransform contentPanel;
    [SerializeField] private ScrollRect scrollView;
    protected List<GameObject> buttons = new List<GameObject>();


    public List<GameObject> Buttons
    {
        get
        {
            return buttons;
        }

        set
        {
            buttons = value;
        }
    }

    public ScrollRect ScrollView
    {
        get
        {
            return scrollView;
        }

        set
        {
            scrollView = value;
        }
    }

    public CommandsPanel CommandsPanel
    {
        get
        {
            return commands;
        }

        set
        {
            commands = value;
        }
    }

    public IEnumerator Play()
    {
       
        int position = 0;
        foreach (Command command in commands.Commands)
        {
            GameObject button = Buttons[position];
            button.GetComponent<Image>().color = Color.green;
            if (!GameManager.instance.SendCommandToPlayer(command))
            {
                button.GetComponent<Image>().color = Color.red;
            }
            yield return new WaitForSeconds(2);
            if (!(button.GetComponent<Image>().color == Color.red))
            {
                button.GetComponent<Image>().color = Color.white;
            }

            scrollView.velocity = new Vector2(0f, 30f);
            position++;
        }
    }

    public void HandleCommands(string commandString)
    {
        Command command = commands.ReciveComand(commandString);
        TranslateCommandToCode(command, commands.Commands.Count);
    }

    public void TranslateCommandToCode(Command command, int lineNumber)
    {

        // foreach (Command command in commandsPanel.Comands)
        //{
        if(command != Command.None )
        {
            GameObject newButton = GameObject.Instantiate(button);
            Buttons.Add(newButton);
            CodeButton codeButton = newButton.GetComponent<CodeButton>();
            codeButton.LineNumber.text = lineNumber.ToString();
            
            codeButton.CommandName.text = TranslateCommandToString(command);


            newButton.transform.SetParent(contentPanel,false);
            
        }
            
        //}
    }


    public string TranslateCommandToString(Command command)
    {
        switch (command)
        {
            case Command.Walk_Top:
                return "andarParaCima()";
            case Command.Walk_Right:
                return "andarParaDireita()";
            case Command.Walk_Bot:
                return "andarParaBaixo()";
            case Command.Walk_Left:
                return "andarParaEsquerda()";
            case Command.Collect_Coin:
                return "coletarMoeda()";
            case Command.Open_Chest:
                return "abrirTesouro()";
            case Command.Function1:
                return "função1()";
            case Command.Function2:
                return "função2()";
        }
        return null;
    }

	// Use this for initialization
	void Start () {
		//GameObject lineCode = Instantiate
	}
	

}
