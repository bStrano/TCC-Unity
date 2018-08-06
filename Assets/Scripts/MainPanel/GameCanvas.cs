using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCanvas : MonoBehaviour {
    [SerializeField] private MainPanel codePanel;
    [SerializeField] private MainPanel functionPanel1;
    [SerializeField] private MainPanel functionPanel2;
    private GameObject activeCodePanel;

    public void SwitchToFunctionPanel(int functionNumber)
    {
        codePanel.gameObject.SetActive(false);
        switch (functionNumber)
        {
            case 1:
                activeCodePanel = functionPanel1.gameObject;
                break;
            case 2:
                activeCodePanel = functionPanel2.gameObject;
                break;
        }
        activeCodePanel.SetActive(true);
    }


    public void StartPlayRoutine()
    {
        //CodeOutputPanel codeOutput = codePanel.GetComponentInChildren<CodeOutputPanel>();
        StartCoroutine(Play());
    }

    public IEnumerator Play()
    {

        int position = 0;

        CodeOutputPanel activeCodeOutputPanel = GetActiveCodePanel();
        foreach (Command command in activeCodeOutputPanel.CommandsPanel.Commands)
        {
            activeCodeOutputPanel = GetActiveCodePanel();
            GameObject button = activeCodeOutputPanel.Buttons[position];
            button.GetComponent<Image>().color = Color.green;

            if (command != Command.Function1 && command != Command.Function2)
            {

                if (!GameManager.instance.SendCommandToPlayer(command))
                {
                    button.GetComponent<Image>().color = Color.red;
                }
            }
            yield return new WaitForSeconds(2);
            if (!(button.GetComponent<Image>().color == Color.red))
            {
                button.GetComponent<Image>().color = Color.white;
            }

            activeCodeOutputPanel.ScrollView.velocity = new Vector2(0f, 30f);

            position++;

            if ( command == Command.Function1)
            {
                SwitchToFunctionPanel(1);
                 yield return GetActiveCodePanel().StartCoroutine(Play());
                SwitchToCodePanel();
                yield return new WaitForSeconds(2);
            } else if (command == Command.Function2)
            {
                SwitchToFunctionPanel(2);
                yield return GetActiveCodePanel().StartCoroutine(Play());
                SwitchToCodePanel();
                yield return new WaitForSeconds(2);
            } 
            
             
  
             
        }
    }
    public IEnumerator PlayFunction()
    {
        SwitchToFunctionPanel(1);
        yield return GetActiveCodePanel().StartCoroutine(Play());
        SwitchToCodePanel();
        yield return new WaitForSeconds(2);
    }



    public CodeOutputPanel GetActiveCodePanel()
    {
        return activeCodePanel.GetComponentInChildren<CodeOutputPanel>();
    }

    public void SwitchToCodePanel()
    {
        activeCodePanel.SetActive(false);
        activeCodePanel = codePanel.gameObject;
        activeCodePanel.SetActive(true);
    }

    public void CancelFunctionCreation()
    {
        SwitchToCodePanel();
        Function function = activeCodePanel.GetComponent<Function>();
        if (function != null)
        {
            function.Cancel();
        }
        else
        {
            Debug.Log("Active object isnt a function");
        }

    }
    // Use this for initialization
    void Start () {
        activeCodePanel = codePanel.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
