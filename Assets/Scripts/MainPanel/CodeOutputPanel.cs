using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using System.Linq;
using TMPro;


public class CodeOutputPanel : MonoBehaviour
{
    [SerializeField] private GameCanvas gameCanvas;
    [SerializeField] protected GameObject mainPanel;
    [SerializeField] protected CommandsPanel commands;
    [SerializeField] private GameObject button;
    [SerializeField] private GameObject variableButton;
    [SerializeField] private RectTransform contentPanel;
    [SerializeField] private ScrollRect scrollView;
    [SerializeField] private TMP_Dropdown dropdown;
    
    protected List<GameObject> buttons = new List<GameObject>();
    private List<Loop> loops = new List<Loop>();

    public void reset()
    {
        StopAllCoroutines();
    }


    public void HandleVariableCommand()
    {
        dropdown.Hide();
        if (dropdown.value == 0) return;
        
        var numberOfRepetitions = GameManager.instance.Variables.ElementAt(dropdown.value - 1).Value;
        if (numberOfRepetitions != null) HandleLoopCommand((int) numberOfRepetitions);
        Invoke (nameof(CallDeactivateLoopPanelLoopPanel), 0.2f);
        
        dropdown.value = 0;
    }

    private void CallDeactivateLoopPanelLoopPanel()
    {
        gameCanvas.DeactivateLoopPanel();
    }

    

    public void HandleLoopCommand(int numberOfRepetitions)
    {
        Command command = commands.ReciveComand(Command.Loop.ToString());
        Loops.Add(new Loop(this.buttons.Count, numberOfRepetitions));
        TranslateCommandToCode(Command.Loop, numberOfRepetitions.ToString());
        GameManager.instance.NextCommandTutoredGameplay();
        GameManager.instance.LoopMode = true;
        LoopButton loopButton = mainPanel.GetComponent<MainPanel>().CommandsPanel.LoopButton.GetComponent<LoopButton>();
        loopButton.ActivateLoopMode();
    }


    public void EndLoopCommand()
    {
        Command command = commands.ReciveComand(Command.EndLoop.ToString());


        GameManager.instance.LoopMode = false;
        TranslateCommandToCode(Command.EndLoop, null);
        Loops[Loops.Count - 1].FinalIndex = buttons.Count - 1;
        LoopButton loopButton = mainPanel.GetComponent<MainPanel>().CommandsPanel.LoopButton.GetComponent<LoopButton>();
        loopButton.DisableLoopMode();
    }


    public void HandleCommands(Command command, string additional)
    {
        commands.ReciveComand(command.ToString());
        TranslateCommandToCode(command, additional);
        GameManager.instance.NextCommandTutoredGameplay();
    }
    
    public void HandleCommands(string commandString)
    {
        switch (commandString)
        {
            case "Function1":
            {
                try
                {
                    if (gameCanvas.FunctionPanel1.CommandsPanel.Commands.Count == 0)
                    {
                        GameManager.instance.AlertDialog.SetupDialog(
                            "Mantenha pressionado o botão de função para abrir o painel de funções", "Entendi!");
                        GameManager.instance.AlertDialog.OpenDialog();

                        return;
                    }

                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    GameManager.instance.AlertDialog.SetupDialog(
                        "Mantenha pressionado o botão de função para abrir o painel de funções", "Entendi!");
                    GameManager.instance.AlertDialog.OpenDialog();
                    return;
                    
                }
            }
            case "Function2":
            {
                if (gameCanvas.FunctionPanel2.CommandsPanel.Commands.Count == 0)
                {
                    GameManager.instance.AlertDialog.OpenDialog();
                    return;
                }

                break;
            }
        }

        Command command = commands.ReciveComand(commandString);
        TranslateCommandToCode(command, null);
        GameManager.instance.NextCommandTutoredGameplay();
    }

    private void RemoveButton(int i)
    {
        Destroy(buttons[i].gameObject);
        buttons.RemoveAt(i);
        commands.RemoveCommand(i);
        gameCanvas.UpdateEntries(false);
    }

    public void TranslateCommandToCode(Command command, string additional)
    {
        int lineNumber = buttons.Count + 1;
        // foreach (Command command in commandsPanel.Comands)
        //{
        if (command != Command.None)
        {
            GameObject newButton = GameObject.Instantiate(button, contentPanel, false);
            if (command != Command.EndLoop && command != Command.Loop && command != Command.Variable)
            {
                gameCanvas.UpdateEntries(true);
            }

            Buttons.Add(newButton);
            CodeButton codeButton = newButton.GetComponent<CodeButton>();
            codeButton.LineNumber.text = lineNumber.ToString();
            codeButton.DeleteButton.onClick.AddListener(() =>
            {
                for (int i = buttons.Count - 1; i >= 0; i--)
                {
                    if (buttons[i].gameObject.GetComponent<CodeButton>().LineNumber.text == codeButton.LineNumber.text)
                    {
                        if (command == Command.Loop)
                        {
                            while (true)
                            {
                                try
                                {
                                    Command commandAux = commands.Commands[i];

                                    RemoveButton(i);

                                    if (commandAux == Command.EndLoop)
                                    {
                                        break;
                                    }
                                }
                                catch (ArgumentOutOfRangeException e)
                                {
                                    Debug.Log(e);
                                    break;
                                }
                            }

                            break;
                        }
                        else if (command == Command.EndLoop)
                        {
                            while (true)
                            {
                                Command commandAux = commands.Commands[i];
                                RemoveButton(i);

                                if (commandAux == Command.Loop)
                                {
                                    break;
                                }
                                else
                                {
                                    i--;
                                }
                            }
                        }
                        else
                        {
                            buttons.RemoveAt(i);
                            commands.RemoveCommand(i);
                            Destroy(codeButton.gameObject);
                            break;
                        }
                    }
                }

                for (int i = buttons.Count - 1; i >= 0; i--)
                {
                    CodeButton codeButtonTemp = buttons[i].gameObject.GetComponent<CodeButton>();
                    codeButtonTemp.LineNumber.text = (i + 1).ToString();
                }

                Debug.Log(buttons.Count);
            });

            codeButton.CommandName.text = TranslateCommandToString(command, additional);

            
            
            if (command != Command.If && command != Command.Else && command != Command.EndIf)
            {
                Debug.Log(command);
                if (GameManager.instance.ConditionalMode == 1 || GameManager.instance.ConditionalMode == 2)
                {
                    codeButton.MainPanel.GetComponent<RectTransform>().offsetMin = new Vector2(10,0);
                }
                    
            }


            if (command == Command.If)
            {
                codeButton.CommandName.GetComponent<Text>().fontSize = 12;
                GameManager.instance.ConditionalMode = 1;
                Debug.Log(commands);
                Debug.Log(commands.ConditionalButton);
                commands.ConditionalButton.GetComponentInChildren<Text>().text = "Senão";
            } else if (command == Command.Else)
            {
                GameManager.instance.ConditionalMode = 2;
                commands.ConditionalButton.GetComponentInChildren<Text>().text = "Fimse";
            } else if (command == Command.EndIf)
            {
                GameManager.instance.ConditionalMode = 0;
                commands.ConditionalButton.GetComponentInChildren<Text>().text = "Se";
            } 

            

           
            
            if (GameManager.instance.LoopMode)
            {
                codeButton.AddSpaceLeft();
            }
        }

        //}
    }


    public string TranslateCommandToString(Command command, string additional)
    {
        switch (command)
        {
            case Command.Walk_Top:
                return "andarCima()";
            case Command.Walk_Right:
                return "andarDireita()";
            case Command.Walk_Bot:
                return "andarBaixo()";
            case Command.Walk_Left:
                return "andarEsquerda()";
            case Command.Collect_Coin:
                return "coletarMoeda()";
            case Command.Open_Chest:
                return "abrirTesouro()";
            case Command.Function1:
                return "função1()";
            case Command.Function2:
                return "função2()";
            case Command.Loop:
                return "Repetir " + additional + " vezes";
            case Command.EndLoop:
                return "Fim repetir";
            case Command.Fireball:
                return "bolaDeFogo()";
            case Command.Ice:
                return "gelo()";
            case Command.Lightning:
                return "raio()";
            case Command.If:
                return "se (" + additional + ") então";
            case Command.Else:
                return "senão";
            case Command.EndIf:
                return "fimse";
        }

        return null;
    }

    // Use this for initialization
    void Start()
    {
        foreach (var variable in GameManager.instance.Variables)
        {
            GameObject newButton = GameObject.Instantiate(variableButton, contentPanel, false);
            newButton.GetComponent<VariableButton>().TitleText.text = variable.Title;
        }

        //GameObject lineCode = Instantiate
    }


    public List<GameObject> Buttons
    {
        get { return buttons; }

        set { buttons = value; }
    }

    public ScrollRect ScrollView
    {
        get { return scrollView; }

        set { scrollView = value; }
    }

    public CommandsPanel CommandsPanel
    {
        get { return commands; }

        set { commands = value; }
    }

    public List<Loop> Loops
    {
        get { return Loops1; }

        set { Loops1 = value; }
    }

    public List<Loop> Loops1
    {
        get { return loops; }

        set { loops = value; }
    }
}