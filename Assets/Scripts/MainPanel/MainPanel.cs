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
