using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


public enum Command
{
    None,
    Walk_Top,
    Walk_Right,
    Walk_Bot,
    Walk_Left,
    Attack,
    Spell1,
    Spell2,
    Spell3,
    Collect_Coin,
    Open_Chest,
    Function1,
    Function2,
    Loop,
    EndLoop,
}

public class CommandsPanel : MonoBehaviour
{
    [SerializeField] private Text entries;
    [SerializeField] private Button loopButton;
    private int maxComands;
    private int programmingCommands = 0;


    public List<Command> Commands { get; private set; }


    public Button LoopButton
    {
        get { return loopButton; }

        set { loopButton = value; }
    }


    public void Awake()
    {
        Commands = new List<Command>();
    }

    public void Start()
    {
        Debug.Log(GameManager.instance.CommandsAvailable);
        Debug.Log("Teste");

        maxComands = GameManager.instance.CommandsAvailable;
        Debug.Log(maxComands);


        UpdateEntries();
    }


    public bool AddComand(Command command)
    {
        if (Commands.Count < maxComands)
        {
            Commands.Add(command);
            return true;
        }
        else
        {
            return false;
            // Not implemented yet
        }
    }

    public void UpdateEntries()
    {
        entries.text = Commands.Count + "/" + maxComands;
        if (Commands.Count == maxComands)
        {
            entries.color = Color.red;
        }
        else
        {
            entries.color = Color.black;
        }
    }

    public void RemoveCommand(int commandIndex)
    {
        Commands.RemoveAt(commandIndex);
        UpdateEntries();
    }


    public Command ReciveComand(string commandString)
    {
        Command command = ((Command) Enum.Parse(typeof(Command), commandString));
        if (AddComand(command))
        {
            UpdateEntries();
            return command;
        }
        else
        {
            return Command.None;
        }
    }
}