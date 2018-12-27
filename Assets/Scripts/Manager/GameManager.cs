using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField] private Transform spawnpoint;
    [SerializeField] private Player player;
    private GameObject[] coins;
    [SerializeField] private CodeOutputPanel codeOutputPanel;
    public static GameManager instance;

    public bool SendCommandToPlayer(Command command)
    {
        return player.setActiveCommand(command);
    }

    public void ResetGame()
    {
        coins = GameObject.FindGameObjectsWithTag("Coin");
        foreach (GameObject coin in coins)
        {
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
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ExitGame()
    {
        LevelManager.instance.BackToMenu();
    }
}
