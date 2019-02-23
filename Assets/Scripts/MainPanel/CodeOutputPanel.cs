using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;


public class CodeOutputPanel : MonoBehaviour
{
    [SerializeField] private GameCanvas gameCanvas;
    [SerializeField] protected GameObject mainPanel;
    [SerializeField] protected CommandsPanel commands;
    [SerializeField] private GameObject button;
    [SerializeField] private RectTransform contentPanel;
    [SerializeField] private ScrollRect scrollView;

    protected List<GameObject> buttons = new List<GameObject>();
    private List<Loop> loops = new List<Loop>();

    public void reset()
    {
        StopAllCoroutines();
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

    public void HandleCommands(string commandString)
    {
        Debug.Log(commandString);
        switch (commandString)
        {
            case "Function1":
            {
                try
                {
                    if (gameCanvas.FunctionPanel1.CommandsPanel.Commands.Count == 0)
                    {
                        GameManager.instance.AlertDialog.SetupDialog("Mantenha pressionado o botão de função para abrir o painel de funções", "Entendi!");
                        GameManager.instance.AlertDialog.OpenDialog();
                        
                        return;
                      
                    }

                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    GameManager.instance.AlertDialog.SetupDialog("Mantenha pressionado o botão de função para abrir o painel de funções", "Entendi!");
                    GameManager.instance.AlertDialog.OpenDialog();
                    return;
                    throw;
                    
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
        
    }

    public void TranslateCommandToCode(Command command, string additional)
    {

        int lineNumber = buttons.Count + 1;
        // foreach (Command command in commandsPanel.Comands)
        //{
        if (command != Command.None)
        {
            GameObject newButton = GameObject.Instantiate(button, contentPanel, false);
            Buttons.Add(newButton);
            CodeButton codeButton = newButton.GetComponent<CodeButton>();
            codeButton.LineNumber.text = lineNumber.ToString();
            codeButton.DeleteButton.onClick.AddListener(() =>
            {
                Debug.Log(codeButton.gameObject.GetComponent<CodeButton>().LineNumber.text);

                for (int i = buttons.Count - 1; i >= 0; i--)
                {
                    if (buttons[i].gameObject.GetComponent<CodeButton>().LineNumber.text == codeButton.LineNumber.text)
                    {
                        Debug.Log("Index: " + i);
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
                                    Debug.Log("Exception");
                                    break;
                                }
                            }
                            break;
                        } else if (command == Command.EndLoop)
                        {
                            while (true)
                            {


                                Command commandAux = commands.Commands[i];
                                RemoveButton(i);
    
                                if (commandAux == Command.Loop)
                                {
                                    break;
                                } else
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
        }
        return null;
    }

    // Use this for initialization
    void Start()
    {
        //GameObject lineCode = Instantiate
    }



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

    public List<Loop> Loops
    {
        get
        {
            return Loops1;
        }

        set
        {
            Loops1 = value;
        }
    }

    public List<Loop> Loops1
    {
        get
        {
            return loops;
        }

        set
        {
            loops = value;
        }
    }
}


