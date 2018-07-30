using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPersistence : MonoBehaviour {


    public bool isUnlocked(string levelNumber)
    {
        if (string.Compare(levelNumber,"1") == 0)
        {
            return true;
        }
       
        if (PlayerPrefs.GetInt("Level_" + levelNumber) == 1)
        {
            return true;
        }

        return false;
    }

    public void saveToUnlockedList(string levelNumber)
    {
        PlayerPrefs.SetInt("Level_" + levelNumber,1);
    }




	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
