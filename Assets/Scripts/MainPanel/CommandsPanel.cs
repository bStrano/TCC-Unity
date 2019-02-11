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
    private List<Command> commands;
    [SerializeField]private Text entries;
    [SerializeField] private Button loopButton;
    private int maxComands;


    public List<Command> Commands
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



    public Button LoopButton
    {
        get
        {
            return loopButton;
        }

        set
        {
            loopButton = value;
        }
    }

    public void Start()
    {
        try
        {
            maxComands = LevelManager.instance.ActiveLevel.ComandsAvaiable;
        }catch(NullReferenceException ex)
        {
            // Debug only
            maxComands = 1000;
            Debug.LogWarning(ex);
        }
        
        Debug.Log("Level Manages commands Avaiable: " + maxComands );
        commands = new List<Command>();
        UpdateEntries();
    }
  


    public bool AddComand(Command command)
    {
        if (commands.Count < maxComands)
        {
            commands.Add(command);
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
        entries.text = commands.Count + "/" + maxComands;
        if(commands.Count == maxComands)
        {
            entries.color = Color.red;
        }
    }

    public void RemoveCommand(int commandIndex)
    {
        commands.RemoveAt(commandIndex);
        UpdateEntries();
    }


 

    public Command ReciveComand(string commandString)
    {
        Command command = ((Command)Enum.Parse(typeof(Command), commandString));
        if (AddComand(command))
        {
            UpdateEntries();
            return command;
        } else
        {
           return Command.None;
        }


    }

   

}


