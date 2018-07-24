using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour {

    public static LevelManager instance = null;

    public int ActiveLevel
    {
        get
        {
            return activeLevel;
        }

        set
        {
            activeLevel = value;
        }
    }

    private int activeLevel;
     
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        
        
    }

    [SerializeField]
    private List<Level> levelList;


    public Level GetActiveLevel()
    {
        int activeIndex = SceneManager.GetActiveScene().buildIndex;
        return levelList[activeIndex];
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
