using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

    public static LevelManager instance = null;


    public Level ActiveLevel { get; set; }

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



    public void SwitchScene(string sceneName, Level level)
    {
        this.ActiveLevel = level;
        SceneManager.LoadScene(sceneName);
       
    }


    


    public Level GetActiveLevel()
    {
        return ActiveLevel;
        //int activeIndex = SceneManager.GetActiveScene().buildIndex;
        
       // return levelList[activeIndex];
    }

	// Use this for initialization
	void Start () {


    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
