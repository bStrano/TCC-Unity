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
    Fireball,
    Ice,
    Lightning,
    Collect_Coin,
    Open_Chest,
    Function1,
    Function2,
    Loop,
    EndLoop,
    Variable,
}

public class CommandsPanel : MonoBehaviour
{
    
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



    public void RemoveCommand(int commandIndex)
    {
        Commands.RemoveAt(commandIndex);
    }


    public Command ReciveComand(string commandString)
    {
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
}