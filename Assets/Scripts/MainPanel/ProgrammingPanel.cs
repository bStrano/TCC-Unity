using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgrammingPanel : MonoBehaviour {
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private Button functionButton;
    [SerializeField] private GameObject functionPanel;
    [SerializeField] private Button loopButton;
    [SerializeField] private Button condictionButton;

    public GameObject MainPanel
    {
        get
        {
            return mainPanel;
        }

        set
        {
            mainPanel = value;
        }
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OpenFunctionPanel()
    {
        functionPanel.SetActive(true);
    }
}
