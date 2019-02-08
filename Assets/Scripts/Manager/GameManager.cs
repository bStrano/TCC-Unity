using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    [SerializeField] private Transform spawnpoint;
    [SerializeField] private Player player;
    private GameObject[] coins;
    [SerializeField] private GameObject parentPanel;
    [SerializeField] private CodeOutputPanel codeOutputPanel;
    public static GameManager instance;


    private bool tutoredGameplayMode = false;
    private bool loopMode = false;
    private bool functionMode = false;
    private bool varMode = false;

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

    public void NextCommandTutoredGameplay()
    {
        Debug.Log("Next Command Tutored Gameplay");
        if (tutoredGameplayMode)
        {
            tutoredGameplayMode = parentPanel.GetComponent<TutoredGameplay>().NextButton();

            Debug.Log("Next Command Tutored Gameplay");
            Debug.Log(tutoredGameplayMode);
        }
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

        if(parentPanel.GetComponent<TutoredGameplay>().Buttons.Count > 0)
        {
            tutoredGameplayMode = true;
            //Image image = parentPanel.GetComponent<Image>();
            //var tempColor = image.color;
            //tempColor.a = 0.6f;
            //image.color = tempColor;

        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ExitGame()
    {
        LevelManager.instance.BackToMenu();
    }



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

    public bool TutoredGameplayMode
    {
        get
        {
            return tutoredGameplayMode;
        }

        set
        {
            tutoredGameplayMode = value;
        }
    }
}
