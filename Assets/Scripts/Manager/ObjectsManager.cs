using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectsManager : MonoBehaviour {
    public static ObjectsManager instance;

    [SerializeField] private List<GameObject> coins;
    [SerializeField] private GameObject chestObject;



    public bool RequestCoinCollect(Transform transform)
    {
        for (int i = 0; i < coins.Count; i++) {
            GameObject coinObject = coins[i];
            Debug.Log(coinObject);
            Coin coin = coinObject.GetComponent<Coin>();

            if (coin.RemoveCoin(transform))
            {
                return true;
            } 
        }
        return false;
    }

    public bool HasCollectedCoins()
    {
        foreach (GameObject coin in coins)
        {
            SpriteRenderer spriteRenderer = coin.GetComponent<SpriteRenderer>();
            if (spriteRenderer.enabled)
            {
                return false;
            }
        }

        return true;
    }
    
    public bool HasChest(Vector2 pos)
    {
        var position = chestObject.transform.position;
        var chestIntTransform = new Vector2((float) Math.Floor(position.x),(float) Math.Floor(position.y));
       
        var transformInt = new Vector2((float) Math.Floor(pos.x), (float) Math.Floor(pos.y));

        return chestIntTransform.Equals(transformInt);
    }
    
    public bool HasTrap(Transform transform)
    {
        foreach (GameObject gameObject in coins)
        {
            Coin coin = gameObject.GetComponent<Coin>();
            if (coin.HasCoin(transform))
            {
                return coin.IsTrap;
            }
        }

        return false;
    }
    
    
    public Direction RequestOpenChest(Transform transform)
    {
        Chest chest = chestObject.GetComponent<Chest>();
        return chest.OpenChest(transform);
    }


    public void ExitGame()
    {
        LevelManager.instance.BackToMenu();
    }

    // Use this for initialization
    void Awake () {
        instance = this;
    }
	
	// Update is called once per frame
	void Update () {
		
	}


}
