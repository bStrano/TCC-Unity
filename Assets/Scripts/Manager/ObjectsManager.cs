using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectsManager : MonoBehaviour {
    public static ObjectsManager instance;

    [SerializeField] private List<GameObject> coins;
    [SerializeField] private GameObject chestObject;


    public Coin getCoinAtTransform(Transform transform)
    {
        Vector2 pos = new Vector2((float)Math.Truncate(transform.position.x), (float) Math.Truncate(transform.position.y));
        Vector2 coinsPos;
        foreach (var coinObj in coins)
        {
            coinsPos = new Vector2( (float) Math.Truncate(coinObj.transform.position.x), (float) Math.Truncate(transform.position.y));
            if (coinsPos == pos) return coinObj.GetComponent<Coin>();
        }

        return null;
    }

    
    
    public bool RequestCoinCollect(Transform transform)
    {
        Vector2 pos = new Vector2((float) Math.Truncate(transform.position.x), (float) Math.Truncate(transform.position.y));
        for (int i = 0; i < coins.Count; i++) {
            GameObject coinObject = coins[i];
            if(pos != new Vector2((float) Math.Truncate(coinObject.transform.position.x), (float) Math.Truncate(coinObject.transform.position.y))) continue;
            
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

    public Coin GetCoin(Transform trannsform)
    {
        Coin coin;
        foreach (GameObject coinObj in coins)
        {

            coin = coinObj.GetComponent<Coin>();
            if (coin.HasCoin(trannsform))
            {
                return coin;
            }
        }

        return null;
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
