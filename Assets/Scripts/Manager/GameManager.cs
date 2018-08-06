using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField] private Transform spawnpoint;
    [SerializeField] private Player player;
    public static GameManager instance;

    public bool SendCommandToPlayer(Command command)
    {
        return player.setActiveCommand(command);
    }

   // public void ResetGame()
    //{
     //   foreach (GameObject coin in coins)
      //  {
       //     coin.SetActive(true);

///        }
   //     player.transform.position = spawnpoint.transform.position;
    //}


    // Use this for initialization
    void Start () {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
