using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour {

    public GameObject levelManager;

	// Use this for initialization
	void Awake () {
		if(LevelManager.instance == null)
        {
            Instantiate(levelManager);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
