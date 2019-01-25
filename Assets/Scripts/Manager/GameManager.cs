using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField] private Transform spawnpoint;
    [SerializeField] private Player player;
    private GameObject[] coins;
    [SerializeField] private CodeOutputPanel codeOutputPanel;
    public static GameManager instance;

    private bool loopMode = false;
    private bool functionMode = false;
    private bool varMode = false;

    public bool LoopMode
    {
        get
        {
            return loopMode;
        }

        set
        {
            loopMode = value;
        }
    }

    public bool FunctionMode
    {
        get
        {
            return functionMode;
        }

        set
        {
            functionMode = value;
        }
    }

    public bool VarMode
    {
        get
        {
            return varMode;
        }

        set
        {
            varMode = value;
        }
    }

    public void SetupCodeMode()
    {
        this.loopMode = false;
        this.varMode = false;
        this.functionMode = false;
    }

    public bool SendCommandToPlayer(Command command)
    {
        return player.setActiveCommand(command);
    }

    public void ResetGame()
    {
        
        Debug.Log("Rest Game: " + coins.Length);
        foreach (GameObject coin in coins)
        {
            Debug.Log("COIN");
            coin.SetActive(true);

        }
        player.StopAllCoroutines();
        player.stopWalking();
        codeOutputPanel.StopAllCoroutines();
        player.transform.position = spawnpoint.transform.position;
        StopAllCoroutines();
    }


    // Use this for initialization
    void Start () {
        instance = this;
        coins = GameObject.FindGameObjectsWithTag("Coin");
        Debug.Log("LENGHT" + coins.Length);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ExitGame()
    {
        LevelManager.instance.BackToMenu();
    }
}
