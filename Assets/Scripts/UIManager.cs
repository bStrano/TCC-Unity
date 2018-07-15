using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public static UIManager instance;
    private Button btnMoveRight, btnMoveLeft, btnMoveUp, btnMoveDown; 


    private void Awake()
    {
        if ( instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }


    void LoadButtons()
    {
        btnMoveRight = GameObject.Find("Right").GetComponent<Button>();
        btnMoveLeft = GameObject.Find("Left").GetComponent<Button>();
        btnMoveUp = GameObject.Find("Up").GetComponent<Button>();
        btnMoveDown = GameObject.Find("Down").GetComponent<Button>();

    }

    void Start () {
		//btnMoveDown.OnPointerDown()
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
