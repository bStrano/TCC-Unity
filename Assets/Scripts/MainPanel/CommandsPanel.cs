using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


public enum Command
{
    None,
    Shield,
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
    If,
    Else,
    EndIf,
}

public class CommandsPanel : MonoBehaviour
{

    [SerializeField] private Button conditionalButton;
    [SerializeField] private Button loopButton;
    private int maxComands;
    private int programmingCommands = 0;


    public List<Command> Commands { get; set; }


    public Button LoopButton
    {
        get { return loopButton; }

        set { loopButton = value; }
    }

    public Button ConditionalButton
    {
        get => conditionalButton;
        set => conditionalButton = value;
    }


    public void Awake()
    {
        Commands = new List<Command>();
    }

    public void Start()
    {
        maxComands = GameManager.instance.CommandsAvailable;
        Debug.Log(maxComands);

    }






    public void RemoveCommand(int commandIndex)
    {
        Commands.RemoveAt(commandIndex);
    }


    
    
  
}