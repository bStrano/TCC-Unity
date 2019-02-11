using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;



public class MainPanel : MonoBehaviour {
    [SerializeField] private GameObject mainPanel;  
    [SerializeField] private CodeOutputPanel codeOutputPanel;
    [SerializeField] private CommandsPanel commandsPanel;
  
    private GameObject playerGO;
    private Player player;

    public CodeOutputPanel CodeOutputPanel
    {
        get
        {
            return codeOutputPanel;
        }

        set
        {
            codeOutputPanel = value;
        }
    }

    public CommandsPanel CommandsPanel
    {
        get
        {
            return commandsPanel;
        }

        set
        {
            commandsPanel = value;
        }
    }

    public void AddFunction(Function function)
    {

    }
    // Use this for initialization
    void Start () {
        playerGO = GameObject.Find("Player");
        // player = playerGO.GetComponent<Player>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}



}
