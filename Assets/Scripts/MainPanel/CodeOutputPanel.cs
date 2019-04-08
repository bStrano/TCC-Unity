using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using System.Linq;
using Assets.Scripts;
using TMPro;


public class CodeOutputPanel : MonoBehaviour
{
    [SerializeField] private Text entries;
    private int commandsCount;
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
         GameManager.instance.NextCommandTutoredGameplay();
//        var numberOfRepetitions = GameManager.instance.Variables.ElementAt(dropdown.value - 1).GetValue();
        Variable variable = GameManager.instance.Variables.ElementAt(dropdown.value - 1);
        if (variable.GetValue() != null) HandleLoopCommand(variable);
        Invoke (nameof(CallDeactivateLoopPanelLoopPanel), 0.2f);
        
        dropdown.value = 0;
    }

    private void CallDeactivateLoopPanelLoopPanel()
    {
        GameCanvas.DeactivateLoopPanel();
    }

    public void HandleLoopCommand(Variable variable)
    {
        Loop loop = new Loop(this.buttons.Count, variable);
        HandleLoop(loop);
    }

    public void HandleLoopCommand(int numberOfRepetitions)
    {
        HandleLoop( new Loop(this.buttons.Count, numberOfRepetitions));
    }

    private void HandleLoop(Loop loop)
    {
        Loops.Add(loop);
        Command command = ReciveComand(Command.Loop.ToString());
        TranslateCommandToCode(Command.Loop, loop.NumberIterations.ToString());
        GameManager.instance.NextCommandTutoredGameplay();
        GameManager.instance.LoopMode = true;
        LoopButton loopButton = mainPanel.GetComponent<MainPanel>().CommandsPanel.LoopButton.GetComponent<LoopButton>();
        loopButton.ActivateLoopMode();
    }

    public void RemoveUnfinishedLoopPanel()
    {
        GameManager.instance.LoopMode = false;
        Loops.RemoveAt(Loops.Count -1);
        LoopButton loopButton = mainPanel.GetComponent<MainPanel>().CommandsPanel.LoopButton.GetComponent<LoopButton>();
        loopButton.DisableLoopMode();
    }
    public void EndLoopCommand()
    {
        Loop loop = Loops[Loops.Count - 1];
        loop.FinalIndex = buttons.Count - 1;
               
        if( loop.InitialIndex == loop.FinalIndex)
        {
            GameManager.instance.AlertDialog.SetupDialog(
                "Não é possivel finalizar o loop sem comandos. Adicione comandos ao Loop e tente novamente.", "Ok");
            GameManager.instance.AlertDialog.OpenDialog();
            return;
        } 
        Command command = ReciveComand(Command.EndLoop.ToString());
        GameManager.instance.LoopMode = false;
        TranslateCommandToCode(Command.EndLoop, null);
        Loops[Loops.Count - 1].FinalIndex = buttons.Count - 1;
        LoopButton loopButton = mainPanel.GetComponent<MainPanel>().CommandsPanel.LoopButton.GetComponent<LoopButton>();
        loopButton.DisableLoopMode();
    }


    public void HandleCommands(Command command, string additional)
    {
     
        ReciveComand(command.ToString());
        TranslateCommandToCode(command, additional);
        GameManager.instance.NextCommandTutoredGameplay();
    }
    
    public void HandleCommands(string commandString)
    {
        ScrollView.velocity = new Vector2(0f, 4500f);
        switch (commandString)
        {
            case "Function1":
            {
                try
                {
                    if (GameCanvas.FunctionPanel1.CommandsPanel.Commands.Count == 0)
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
                if (GameCanvas.FunctionPanel2.CommandsPanel.Commands.Count == 0)
                {
                    GameManager.instance.AlertDialog.OpenDialog();
                    return;
                }

                break;
            }
        }

        Command command = ReciveComand(commandString);
        TranslateCommandToCode(command, null);
        GameManager.instance.NextCommandTutoredGameplay();
    }
    
    
    public void UpdateEntries(bool increment)
    {
        if (increment)
        {
            commandsCount++;
        }
        else
        {
            commandsCount--;
        }
        entries.text = commandsCount + "/" + GameManager.instance.CommandsAvailable;
        if (commandsCount == GameManager.instance.CommandsAvailable)
        {
            entries.color = Color.red;
        }
        else
        {
            entries.color = Color.black;
        }
    }

    public void RemoveButton(int i)
    {
        Destroy(buttons[i].gameObject);
        buttons.RemoveAt(i);
        RemoveCommand(i);
        UpdateEntries(false);
    }

    
    
    public void RemoveCommand(int commandIndex)
    {
        foreach (var loop in loops)
        {
            if (commandIndex < loop.FinalIndex)
            {
                loop.FinalIndex--;
            }

            if (commandIndex < loop.InitialIndex)
            {
                loop.InitialIndex--;
            }
        }

        commands.Commands.RemoveAt(commandIndex);
    }
    
    public void TranslateCommandToCode(Command command, string additional)
    {
 
        int lineNumber = buttons.Count + 1;
        // foreach (Command command in commandsPanel.Comands)
        //{
        if (command != Command.None)
        {
            GameObject newButton = GameObject.Instantiate(button, contentPanel, false);
            
            //command != Command.EndLoop && command != Command.Loop
            if ( command != Command.Variable)
            {
                UpdateEntries(true);
            }

            Buttons.Add(newButton);
            CodeButton codeButton = newButton.GetComponent<CodeButton>();
            codeButton.LineNumber.text = lineNumber.ToString();

            if (GameManager.instance.TutoredGameplayMode || command == Command.EndLoop)
            {
                codeButton.DeleteButton.gameObject.SetActive(false);
            }
            codeButton.DeleteButton.onClick.AddListener(() =>
            {
                if (gameCanvas.IsPlaying) GameManager.instance.ResetGame();
               
                
                for (int i = buttons.Count - 1; i >= 0; i--)
                {
                    if (gameCanvas.IsPlaying) GameManager.instance.ResetGame();
                    if (buttons[i].gameObject.GetComponent<CodeButton>().LineNumber.text == codeButton.LineNumber.text)
                    {
                        if (command == Command.Loop)
                        {
                                RemoveUnfinishedLoopPanel();  
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
                            RemoveCommand(i);
                            Destroy(codeButton.gameObject);
                            UpdateEntries(false);
                            break;

                        }
                    }
                }

                for (int i = buttons.Count - 1; i >= 0; i--)
                {
                    CodeButton codeButtonTemp = buttons[i].gameObject.GetComponent<CodeButton>();
                    codeButtonTemp.LineNumber.text = (i + 1).ToString();
                }

            });
            codeButton.CommandName.text = TranslateCommandToString(command, additional);

            
            
            if (command != Command.If && command != Command.Else && command != Command.EndIf)
            {
                if (GameManager.instance.ConditionalMode == 1 || GameManager.instance.ConditionalMode == 2)
                {
                    codeButton.MainPanel.GetComponent<RectTransform>().offsetMin = new Vector2(10,0);
                }
                    
            }


            if (command == Command.If)
            {
                codeButton.CommandName.GetComponent<Text>().fontSize = 12;
                GameManager.instance.ConditionalMode = 1;
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
            case Command.Shield:
                return "escudoMagico()";
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
    
    public bool AddComand(Command command)
    {
        if (commandsCount < GameManager.instance.CommandsAvailable)
        {
            CommandsPanel.Commands.Add(command);
            return true;
        }
        else
        {
            return false;
            // Not implemented yet
        }
    }
    
    public Command ReciveComand(string commandString)
    {
        if (gameCanvas.IsPlaying) GameManager.instance.ResetGame();
        Command command = ((Command) Enum.Parse(typeof(Command), commandString));
        if (AddComand(command))
        {
            return command;
        }
        else
        {
            return Command.None;
        }
    }

    // Use this for initialization
    void Start()
    {
        SetupEntries();
        foreach (var variable in GameManager.instance.Variables)
        {
            if (variableButton != null)
            {
                GameObject newButton = GameObject.Instantiate(variableButton, contentPanel, false);
                newButton.GetComponent<VariableButton>().TitleText.text = variable.Title;

            }
            }

        //GameObject lineCode = Instantiate
    }

    public void SetupEntries()
    {
        commandsCount = 0;
        entries.text = commandsCount + "/" + GameManager.instance.CommandsAvailable;
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

    public GameCanvas GameCanvas
    {
        get { return gameCanvas; }
        set { gameCanvas = value; }
    }
}