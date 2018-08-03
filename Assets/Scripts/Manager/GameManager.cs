using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour {
    [SerializeField] private Transform spawnpoint;
    public static GameManager instance;

    [SerializeField] private List<GameObject> coins;
    [SerializeField] private GameObject chestObject;



    public bool RequestCoinCollect(Transform transform)
    {
        for (int i = 0; i < coins.Count; i++) {
            GameObject coinObject = coins[i];
            Coin coin = coinObject.GetComponent<Coin>();

            if (coin.RemoveCoin(transform))
            {
                return true;
            } 
        }
        return false;
    }

    public Direction RequestOpenChest(Transform transform)
    {
        Chest chest = chestObject.GetComponent<Chest>();
        return chest.OpenChest(transform);
    }


    // Use this for initialization
    void Awake () {
        instance = this;
    }
	
	// Update is called once per frame
	void Update () {
		
	}


}
