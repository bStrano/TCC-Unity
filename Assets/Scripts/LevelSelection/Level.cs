using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level  {

    [SerializeField] private int sceneNumber;
    [SerializeField] private int levelNumber;
    [SerializeField] private int comandsAvaiable;
    [SerializeField] private bool unlocked;

    
    
    public int ComandsAvaiable
    {
        get
        {
            return comandsAvaiable;
        }

        set
        {
            comandsAvaiable = value;
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

    public int ComandsAvaiable1
    {
        get
        {
            return comandsAvaiable;
        }

        set
        {
            comandsAvaiable = value;
        }
    }

    public int LevelNumber
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

    public int SceneNumber
    {
        get { return sceneNumber; }
        set { sceneNumber = value; }
    }
}
