using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AlertDialog alertDialog;

    [SerializeField] private int commandsAvailable;
    [SerializeField] private Transform spawnpoint;
    [SerializeField] private Player player;
    private GameObject[] coins;
    [SerializeField] private GameObject parentPanel;
    [SerializeField] private CodeOutputPanel codeOutputPanel;
    [SerializeField] private List<CEnemy> enemies;
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
        if (command == Command.Open_Chest)
        {
            if (player.setActiveCommand(command))
            {
                LevelManager.instance.NextLevel();
                return true;
            }
        }

        return player.setActiveCommand(command);
    }

    public void NotifyEnemies(Command command)
    {
        Debug.Log("Notify Enemies");

        Vector2 nextPlayerPosition = Vector2.zero;
        Vector2 playerPosition = player.transform.position;
        switch (command)
        {
            case Command.Walk_Top:
                nextPlayerPosition = new Vector2(playerPosition.x, playerPosition.y + 1);
                break;
            case Command.Walk_Bot:
                nextPlayerPosition = new Vector2(playerPosition.x, playerPosition.y -1);
                break;
            case Command.Walk_Right:
                nextPlayerPosition = new Vector2(playerPosition.x + 1, playerPosition.y);
                break;
            case Command.Walk_Left:
                nextPlayerPosition = new Vector2(playerPosition.x -1, playerPosition.y);
                break;
        }


        foreach (var enemy in enemies)
        {
            Debug.Log("Enemy");
            enemy.AttackPlayer(player,nextPlayerPosition);
        }
    }

    public void NextCommandTutoredGameplay()
    {
        if (tutoredGameplayMode)
        {
            tutoredGameplayMode = parentPanel.GetComponent<TutoredGameplay>().NextButton();
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


    public void FinishTutorial()
    {
        parentPanel.GetComponent<TutoredGameplay>().ShowLabelPanel();
    }

    void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start()
    {
        coins = GameObject.FindGameObjectsWithTag("Coin");
        Debug.Log("LENGHT" + coins.Length);

        if (parentPanel.GetComponent<TutoredGameplay>().Buttons.Count > 0)
        {
            tutoredGameplayMode = true;
            //Image image = parentPanel.GetComponent<Image>();
            //var tempColor = image.color;
            //tempColor.a = 0.6f;
            //image.color = tempColor;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ExitGame()
    {
        LevelManager.instance.BackToMenu();
    }


    public bool LoopMode
    {
        get { return loopMode; }

        set { loopMode = value; }
    }

    public bool FunctionMode
    {
        get { return functionMode; }

        set { functionMode = value; }
    }

    public bool VarMode
    {
        get { return varMode; }

        set { varMode = value; }
    }

    public bool TutoredGameplayMode
    {
        get { return tutoredGameplayMode; }

        set { tutoredGameplayMode = value; }
    }

    public int CommandsAvailable
    {
        get { return commandsAvailable; }
        set { commandsAvailable = value; }
    }

    public AlertDialog AlertDialog
    {
        get { return alertDialog; }
        set { alertDialog = value; }
    }
}