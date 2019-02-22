using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameCanvas : MonoBehaviour
{
    [SerializeField] private CodeOutputPanel codeOutputPanel;
    [SerializeField] private MainPanel codePanel;
    [SerializeField] private MainPanel functionPanel1;
    [SerializeField] private MainPanel functionPanel2;
    [SerializeField] private MainPanel functionPanel3;
    [SerializeField] private GameObject loopPanel;
    private GameObject activeCodePanel;
    private bool alreadyPlayed = false;
    private bool isPlaying = false;

    public MainPanel FunctionPanel1
    {
        get { return functionPanel1; }
        set { functionPanel1 = value; }
    }

    public MainPanel FunctionPanel2
    {
        get { return functionPanel2; }
        set { functionPanel2 = value; }
    }

    public void SwitchToFunctionPanel(int functionNumber)
    {
        codePanel.gameObject.SetActive(false);
        switch (functionNumber)
        {
            case 1:
                activeCodePanel = FunctionPanel1.gameObject;
                break;
            case 2:
                activeCodePanel = FunctionPanel2.gameObject;
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
        bool isLooping = false;
        int loopIndex = 0;
        int iterationLoopNumber = 1;
        CodeOutputPanel activeCodeOutputPanel = GetActiveCodePanel();
        Debug.Log(activeCodeOutputPanel.CommandsPanel.Commands.Count);
        while (position < activeCodeOutputPanel.CommandsPanel.Commands.Count)
        {
            Command command = activeCodeOutputPanel.CommandsPanel.Commands[position];

            activeCodeOutputPanel = GetActiveCodePanel();
            GameObject button = activeCodeOutputPanel.Buttons[position];
            button.GetComponent<CodeButton>().ChangeBackgroundColor(Color.green);
            if (command == Command.Loop)
            {
                isLooping = true;
            }
            else if (command == Command.EndLoop)
            {
                isLooping = false;
                loopIndex++;
                iterationLoopNumber = 1;
                position++;
                button.GetComponent<CodeButton>().ChangeBackgroundColor(Color.white);
                continue;
            }
            else if (command != Command.Function1 && command != Command.Function2 && !(command == Command.Loop || command == Command.EndLoop))
            {

                if (!GameManager.instance.SendCommandToPlayer(command))
                {
                    button.GetComponent<CodeButton>().ChangeBackgroundColor(Color.red);
                }
            }




            yield return new WaitForSeconds(2);

            if (!(button.GetComponent<CodeButton>().BackgroundColor == Color.red))
            {
                button.GetComponent<CodeButton>().ChangeBackgroundColor(Color.white);
            }


            position++;

            if (command == Command.Function1)
            {
                SwitchToFunctionPanel(1);
                yield return GetActiveCodePanel().StartCoroutine(Play());
                SwitchToCodePanel();
                yield return new WaitForSeconds(2);
            }
            else if (command == Command.Function2)
            {
                SwitchToFunctionPanel(2);
                yield return GetActiveCodePanel().StartCoroutine(Play());
                SwitchToCodePanel();
                yield return new WaitForSeconds(2);
            }


            if (isLooping)
            {
                if (activeCodeOutputPanel.Loops[loopIndex].FinalIndex == position)
                {
                   
                    if (iterationLoopNumber < activeCodeOutputPanel.Loops[loopIndex].NumberIterations)
                    {
                        
                      
                        position = activeCodeOutputPanel.Loops[loopIndex].InitialIndex;
                        
                    }
                    Debug.Log("iterationLoopNumber");
                    Debug.Log(iterationLoopNumber);
                    iterationLoopNumber++;
                }
            } else
            {
                activeCodeOutputPanel.ScrollView.velocity = new Vector2(0f, 35f);
            }
        }
        isPlaying = false;
        alreadyPlayed = true;


    }


    public void StartPlayRoutine()
    {
        EventSystem.current.SetSelectedGameObject(null);
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


    public void DesactivateLoopPanel()
    {
        loopPanel.SetActive(false);
    }

    public void ToogleLoopPanel()
    {

        GameManager.instance.NextCommandTutoredGameplay();
        if (!GameManager.instance.LoopMode)
        {
            loopPanel.SetActive(!loopPanel.activeSelf);
        }
        else
        {
            codeOutputPanel.EndLoopCommand();
            GameManager.instance.LoopMode = false;
        }

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
    void Start()
    {
        activeCodePanel = codePanel.gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
