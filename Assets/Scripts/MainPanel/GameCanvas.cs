using System;
using System.Collections;
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
    [SerializeField] private ConditionalPanel conditionalPanel;
    private Button conditionalButton;
    private Text conditionalButtonText;
    private GameObject activeCodePanel;
    private bool alreadyPlayed;
    private bool isPlaying;
    private bool runningConditional;
    private bool isLooping;


    public void Reset()
    {
        isPlaying = false;
        alreadyPlayed = false;
        isLooping = false;
        runningConditional = false;
        StopAllCoroutines();
    }

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

    public bool IsPlaying
    {
        get { return isPlaying; }
        set { isPlaying = value; }
    }

    public bool IsLooping
    {
        get => isLooping;
        set => isLooping = value;
    }


    public void SwitchToFunctionPanel(int functionNumber)
    {
        codePanel.gameObject.SetActive(false);
        switch (functionNumber)
        {
            case 1:
                activeCodePanel = FunctionPanel1.gameObject;
                if (GameManager.instance.TutoredGameplayMode &&
                    functionPanel1.CodeOutputPanel.Buttons.Count > 0) return;
                ;
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

        codePanel.gameObject.SetActive(false);
        activeCodePanel.SetActive(true);

        GameManager.instance.NextCommandTutoredGameplay();
    }

    public void RestartLevel()
    {
        StopAllCoroutines();
        if (GameManager.instance.TutoredGameplayMode)
        {
            GameManager.instance.ResetGame();
            GameManager.instance.NextCommandTutoredGameplay();
            foreach (GameObject button in codeOutputPanel.Buttons)
            {
                Destroy(button);
            }

            codeOutputPanel.Buttons.Clear();
            codeOutputPanel.CommandsPanel.Commands.Clear();
            codeOutputPanel.SetupEntries();
        }
        else
        {
            LevelManager.instance.RestartLevel();
        }
    }


    public IEnumerator Play()
    {
        GameManager.instance.NextCommandTutoredGameplay();
        int position = 0;
        IsPlaying = true;
        IsLooping = false;
        int loopIndex = 0;
        int iterationLoopNumber = 1;
        CodeOutputPanel activeCodeOutputPanel = GetActiveCodePanel();
        activeCodeOutputPanel.ScrollView.velocity = new Vector2(0f, -4500);
        while (position < activeCodeOutputPanel.CommandsPanel.Commands.Count)
        {
            if (GameManager.instance.CheckPlayerDied())
            {
                StopAllCoroutines();
                break;
            }

            Command command = activeCodeOutputPanel.CommandsPanel.Commands[position];


            activeCodeOutputPanel = GetActiveCodePanel();
            GameObject button = activeCodeOutputPanel.Buttons[position];
            button.GetComponent<CodeButton>().ChangeBackgroundColor(Color.green);
            if (command == Command.Loop)
            {
                IsLooping = true;
            }
            else if (command == Command.EndLoop)
            {
                IsLooping = false;
                loopIndex++;
                iterationLoopNumber = 1;
                position++;
                button.GetComponent<CodeButton>().ChangeBackgroundColor(Color.white);
                continue;
            }
            else if (command == Command.If)
            {
                yield return new WaitForSeconds(GameManager.ConditionalWaitTime);

                try
                {
                    if (!conditionalPanel.CheckStatement(activeCodeOutputPanel.Buttons[position]
                        .GetComponentInChildren<Text>().text))
                    {
                        while (command != Command.Else)
                        {
                            command = activeCodeOutputPanel.CommandsPanel.Commands[position];
                            position++;
                        }

                        button.GetComponent<CodeButton>().ChangeBackgroundColor(Color.white);
                        continue;
                    }
                }
                catch (Exception e)
                {
                    button.GetComponent<CodeButton>().ChangeBackgroundColor(Color.red);
                    throw e;
                }


                runningConditional = true;
            }
            else if (command == Command.Else)
            {
                if (runningConditional)
                {
                    while (command != Command.EndIf)
                    {
                        command = activeCodeOutputPanel.CommandsPanel.Commands[position];
                        position++;
                    }

                    position--;
                    button.GetComponent<CodeButton>().ChangeBackgroundColor(Color.white);
                    continue;
                }
            }
            else if (command == Command.EndIf)
            {
                yield return new WaitForSeconds(GameManager.EndConditionalWaitTime);
                runningConditional = false;
            }
            else if (command != Command.Function1 && command != Command.Function2 &&
                     !(command == Command.Loop || command == Command.EndLoop))
            {
                GameManager.instance.NotifyEnemies(command);
                if (!GameManager.instance.SendCommandToPlayer(command))
                {
                    button.GetComponent<CodeButton>().ChangeBackgroundColor(Color.red);
                }
            }

            yield return new WaitForSeconds(GameManager.NormalCommandsWaitTime);


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
                yield return new WaitForSeconds(GameManager.FunctionWaitTime);
            }
            else if (command == Command.Function2)
            {
                SwitchToFunctionPanel(2);
                yield return GetActiveCodePanel().StartCoroutine(Play());
                SwitchToCodePanel();
                yield return new WaitForSeconds(GameManager.FunctionWaitTime);
            }


            if (IsLooping)
            {
//                Debug.Log("Loop");
//                Debug.Log("Loop - Initial Loop Index -" + activeCodeOutputPanel.Loops[loopIndex].FinalIndex);
//                Debug.Log("Loop - Final Index" + activeCodeOutputPanel.Loops[loopIndex].FinalIndex);
//                Debug.Log("Position - " + position);
//                Debug.Log("IterationLoopNumber -" + iterationLoopNumber);
                if (activeCodeOutputPanel.Loops[loopIndex].FinalIndex == position)
                {
                    if (iterationLoopNumber < activeCodeOutputPanel.Loops[loopIndex].NumberIterations)
                    {
                        position = activeCodeOutputPanel.Loops[loopIndex].InitialIndex;
                    }

                    iterationLoopNumber++;
                }
            }
            else
            {
                activeCodeOutputPanel.ScrollView.velocity = new Vector2(0f, 45f);
            }
        }

        IsPlaying = false;
        alreadyPlayed = true;
    }


    public void StartPlayRoutine()
    {
        EventSystem.current.SetSelectedGameObject(null);
        //CodeOutputPanel codeOutput = codePanel.GetComponentInChildren<CodeOutputPanel>();
        if (!alreadyPlayed && !IsPlaying)
        {
            IsPlaying = true;
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


    public void DeactivateLoopPanel()
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
        }
    }


    public void OnClickConditional()
    {
        CodeOutputPanel activeCodeOutputPanel = GetActiveCodePanel();
        // GameManager.instance.NextCommandTutoredGameplay();
        if (GameManager.instance.ConditionalMode == 0)
        {
            conditionalButtonText.fontSize = 25;
            conditionalPanel.gameObject.SetActive(!conditionalPanel.gameObject.activeSelf);
            GameManager.instance.NextCommandTutoredGameplay();
        }
        else if (GameManager.instance.ConditionalMode == 1)
        {
            conditionalButtonText.fontSize = 17;
            activeCodeOutputPanel.HandleCommands(Command.Else, null);
        }
        else if (GameManager.instance.ConditionalMode == 2)
        {
            conditionalButtonText.fontSize = 25;
            activeCodeOutputPanel.HandleCommands(Command.EndIf, null);
        }
    }

    public void ToggleConditionalPanel()
    {
        // conditionalPanel.CheckIsValid();
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
        GameObject conditionalButtonObj = GameObject.Find("Conditional Button");


        if (conditionalButtonObj != null)
        {
            conditionalButton = conditionalButtonObj.GetComponent<Button>();

            conditionalButtonText = conditionalButton.GetComponentInChildren<Text>();
        }
    }

    // Update is called once per frame
}