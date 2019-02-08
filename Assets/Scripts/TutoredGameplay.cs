using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutoredGameplay : MonoBehaviour
{

    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject functionPanel;
    [SerializeField] private List<Button> buttons;
    [SerializeField] private List<String> labels;
    [SerializeField] private GameObject labelPanel;
    private int activeIndex = 0;

    public List<Button> Buttons
    {
        get
        {
            return buttons;
        }

        set
        {
            buttons = value;
        }
    }

    public bool NextButton()
    {
        Debug.Log("Next Button");
        Debug.Log(activeIndex);
        DesactivateButton(buttons[activeIndex]);
        activeIndex++;
        
        if (activeIndex < buttons.Count)
        {
            ActivateButton(buttons[activeIndex]);
            ChangeLabel();
            return true;
        }
        
        return false;
    }

    public void ChangeLabel()
    {

        if(labels.Count <= 0)
        {
            labelPanel.SetActive(false);
            return;
        }
        if(labels[activeIndex] != "")
        {
            labelPanel.SetActive(true);
            Text labelText = labelPanel.GetComponentInChildren<Text>();
            Debug.Log(labels[activeIndex]);
            labelText.text = labels[activeIndex];
        } else
        {
            labelPanel.SetActive(false);
        }
    }

    public void DesactivateLabelPanel()
    {
        labelPanel.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        ChangeLabel();
        if (buttons.Count > 0)
        {
            buttons[buttons.Count - 1].onClick.AddListener(() => this.labelPanel.SetActive(false));
            Button[] mainPanelButtons = mainPanel.GetComponentsInChildren<Button>();
            Button[] functionPanelButtons = functionPanel.GetComponentsInChildren<Button>();
            foreach(Button button in mainPanelButtons)
            {
                DesactivateButton(button);
            }
        
            foreach (Button button in functionPanelButtons)
            {
                DesactivateButton(button);
            }

            ActivateButton(buttons[activeIndex]);
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
        ChangeInternalImageAlpha(button, 0.2f);
        button.interactable = false;
        
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
