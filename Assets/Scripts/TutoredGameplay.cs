using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutoredGameplay : MonoBehaviour
{
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject functionPanel;
    [SerializeField] private GameObject loopPanel;
    [SerializeField] private List<Button> buttons;
    [SerializeField] private List<TMP_Dropdown> dropdowns;
    [SerializeField] private List<String> labels;
    [SerializeField] private GameObject labelPanel;
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private GameObject ifPanel;

    private int activeIndex;
    private int activeDropdownIndex;

    public List<Button> Buttons
    {
        get { return buttons; }

        set { buttons = value; }
    }

    public GameObject LabelPanel
    {
        get { return labelPanel; }
        set { labelPanel = value; }
    }


    private void NextDropdown()
    {
        if (activeDropdownIndex - 1 >= 0)
        {
            Debug.Log("Teste: " + activeDropdownIndex);
            dropdowns[activeDropdownIndex - 1].interactable = false;
        }

        dropdowns[activeDropdownIndex].interactable = true;


        activeDropdownIndex++;
        Debug.Log("Teste: " + activeDropdownIndex);
    }

    public bool NextButton()
    {
        if (buttons[activeIndex] != null)
        {
            DesactivateButton(buttons[activeIndex]);
            Debug.Log("Active Dropdown Index: " + activeDropdownIndex + " / " + dropdowns.Count);
        }

        activeIndex++;


        bool wasDropdown = false;

        if (activeIndex < buttons.Count)
        {
            if (buttons[activeIndex] == null)
            {
                if (dropdowns.Count == 0)
                {
                    activeIndex++;
                    if (activeIndex < buttons.Count) return false;
                }
                else
                {
                    NextDropdown();
                    wasDropdown = true;
                }
            }
            else
            {
                if (dropdowns.Count > 0)
                {
                    if (activeDropdownIndex == dropdowns.Count)
                    {
                        dropdowns[activeDropdownIndex - 1].interactable = false;
                    }
                }
            }


            if (!wasDropdown)
            {
                ActivateButton(buttons[activeIndex]);
            }

            ChangeLabel();
            return true;
        }

        return false;
    }

    public void ChangeLabel()
    {
        if (labels.Count <= 0)
        {
            LabelPanel.SetActive(false);
            return;
        }

        if (labels[activeIndex] != "")
        {
            LabelPanel.SetActive(true);

            if (ifPanel != null)
            {
                if (ifPanel.activeSelf)
                {
                    LabelPanel.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 100);
                }
                else
                {
                    LabelPanel.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                }
            }

            Text labelText = LabelPanel.GetComponentInChildren<Text>();
            labelText.text = labels[activeIndex];
        }
        else
        {
            LabelPanel.SetActive(false);
        }
    }

    public void DesactivateLabelPanel()
    {
        LabelPanel.SetActive(false);
    }
    // Start is called before the first frame update

    public void ShowLabelPanel()
    {
//        if (ifPanel.activeSelf)
//        {
        Debug.Log("Teste");
//        }
        LabelPanel.SetActive(true);
    }

    void Start()
    {
        ChangeLabel();


        if (buttons.Count > 0)
        {
//            buttons[buttons.Count - 1].onClick.AddListener(() => this.labelPanel.SetActive(false));
            Button[] mainPanelButtons = mainPanel.GetComponentsInChildren<Button>();
            Button[] functionPanelButtons = functionPanel.GetComponentsInChildren<Button>();
            if (loopPanel != null)
            {
                Button[] loopPanelButtons = loopPanel.GetComponentsInChildren<Button>();
                if (loopPanelButtons != null)
                {
                    foreach (Button button in loopPanelButtons)
                    {
                        DesactivateButton(button);
                    }
                }
            }

            foreach (Button button in mainPanelButtons)
            {
                DesactivateButton(button);
            }

            foreach (Button button in functionPanelButtons)
            {
                DesactivateButton(button);
            }

            foreach (var dropdown in dropdowns)
            {
                dropdown.interactable = false;
            }

            if (dropdowns.Count > 0)
            {
                dropdowns[0].interactable = true;
            }
            

            ActivateButton(buttons[activeIndex]);
        }

        if (tutorialPanel.activeSelf)
        {
            DesactivateLabelPanel();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    void ActivateButton(Button button)
    {
        this.ChangeInternalImageAlpha(button, 1f);
        button.interactable = true;
    }


    public void DesactivateButton(Button button)
    {
        try
        {
            ChangeInternalImageAlpha(button, 0.2f);
            button.interactable = false;
        }
        catch (Exception e)
        {
            Debug.LogWarning(e);
        }
    }

    private void ChangeInternalImageAlpha(Button button, float alpha)
    {
        Image[] internalImage = button.GetComponentsInChildren<Image>();
        if (internalImage.Length > 1)
        {
            var tempColor = internalImage[1].color;
            tempColor.a = alpha;
            internalImage[1].color = tempColor;
        }
    }

    private void CloseTutoredGameplayPanel()
    {
        gameObject.SetActive(false);
    }
}