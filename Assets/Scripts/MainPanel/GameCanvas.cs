using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCanvas : MonoBehaviour {
    [SerializeField] private MainPanel codePanel;
    [SerializeField] private MainPanel functionPanel1;
    [SerializeField] private MainPanel functionPanel2;
    [SerializeField] private MainPanel functionPanel3;
    [SerializeField] private MainPanel loopPanel;
    private GameObject activeCodePanel;
    private bool alreadyPlayed = false;
    private bool isPlaying = false;

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
            case 3:
                activeCodePanel = functionPanel3.gameObject;
                break;
            case 4:
                GameManager.instance.LoopMode = true;
                activeCodePanel = loopPanel.gameObject;
                break;
        }
        activeCodePanel.SetActive(true);
        GameManager.instance.NextCommandTutoredGameplay();
    }

    public void RestartLevel()
    {
        Debug.Log("Restart");
        LevelManager.instance.RestartLevel();
    }


    public IEnumerator Play()
    {
        int position = 0;
            isPlaying = true;
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
            isPlaying = false;
            alreadyPlayed = true;
        
        
    }


    public void StartPlayRoutine()
    {
        //CodeOutputPanel codeOutput = codePanel.GetComponentInChildren<CodeOutputPanel>();
        if (!alreadyPlayed && !isPlaying)
        {
            isPlaying = true;
            StartCoroutine(Play());
        }
        else
        {
            foreach (GameObject image in GetActiveCodePanel().Buttons)
            {
                image.GetComponent<Image>().color = Color.white;
            }

            GameManager.instance.ResetGame();

            alreadyPlayed = false;
            Restart();
        }
    }

    private void Restart()
    {
        StopAllCoroutines();
        StartCoroutine(Play());
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
        GameManager.instance.SetupCodeMode();
        activeCodePanel.SetActive(false);
        activeCodePanel = codePanel.gameObject;
        activeCodePanel.SetActive(true);
        GameManager.instance.NextCommandTutoredGameplay();
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
