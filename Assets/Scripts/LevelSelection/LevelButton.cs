using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour {

    [SerializeField]
    private Text levelNumber;
    [SerializeField]
    private bool unlocked;

    public Text LevelNumber
    {
        get
        {
            return levelNumber;
        }

        set
        {
            levelNumber = value;
        }
    }

    public bool Unlocked
    {
        get
        {
            return unlocked;
        }

        set
        {
            unlocked = value;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
