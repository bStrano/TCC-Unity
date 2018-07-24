using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class MainPanel : MonoBehaviour {

  
    [SerializeField]
    private CodeOutputPanel codeOutputPanel;
    [SerializeField]
    private CommandsPanel commandsPanel;


    private GameObject playerGO;
    private Player player;


 


    public void StartPlayRoutine()
    {
        StartCoroutine(Play());
    }

    public IEnumerator Play()
    {
        foreach (Command comand in commandsPanel.Comands)
        {
            player.setActiveCommand(comand);
            yield return new WaitForSeconds(2);
        }
    }

    // Use this for initialization
    void Start () {
        playerGO = GameObject.Find("Player");
        player = playerGO.GetComponent<Player>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void HandleCommands(string commandString)
    {
        Command command = commandsPanel.ReciveComand(commandString);
        codeOutputPanel.TranslateCommandToCode(command, commandsPanel.Comands.Count);
    }
}
