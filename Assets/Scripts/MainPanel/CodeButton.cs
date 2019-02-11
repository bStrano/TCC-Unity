using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodeButton : MonoBehaviour {

    [SerializeField] GameObject mainPanel;
    [SerializeField]
    private Button button;
    [SerializeField]
    private Text lineNumber;
    [SerializeField]
    private Text commandName;
    [SerializeField]
    private Button deleteButton;


    public void AddSpaceLeft()
    {
        mainPanel.transform.position = new Vector2(mainPanel.transform.position.x + 10, mainPanel.transform.position.y);
        deleteButton.transform.position = new Vector2(deleteButton.transform.position.x - 10, mainPanel.transform.position.y);
    }

    public void ChangeBackgroundColor(Color color)
    {
        mainPanel.GetComponent<Image>().color = color;
    }

    public Color BackgroundColor
    {
        get
        {
            return mainPanel.GetComponent<Image>().color;
        }
    }
    public Text CommandName
    {
        get
        {
            return commandName;
        }

        set
        {
            commandName = value;
        }
    }

    public Text LineNumber
    {
        get
        {
            return lineNumber;
        }

        set
        {
            lineNumber = value;
        }
    }

    public Button DeleteButton
    {
        get
        {
            return deleteButton;
        }

        set
        {
            deleteButton = value;
        }
    }

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




}
