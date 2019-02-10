using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Tutorial : MonoBehaviour {
    private int maxPage;
    [SerializeField] private Text pageNumberText;
    [SerializeField] private Button previousButton;
    [SerializeField] private Button nextButton;
    [SerializeField] private GameObject layoutPrefab;
    [SerializeField] private Transform contentPanel;
    [SerializeField] private TutorialContent[] pages;
    [SerializeField] private Text titleField;
    [SerializeField] private List<string> titles;
    private TutorialContent activePage;
    private int activePagePosition;





    // Use this for initialization
    void Start () {
        foreach (TutorialContent tutorialContent in pages)
        {
            titles.Add(tutorialContent.Title);
        }
        maxPage = pages.Length;
        ChangeActivePage(0);
        previousButton.gameObject.SetActive(false);
        InstantiateContents();




    }

    private void InstantiateContents()
    {
      
        foreach( TutorialContent tutorialContent in pages)
        {
            tutorialContent.ContentPrefab =  Instantiate(tutorialContent.ContentPrefab, contentPanel);
            //tutorialContent.ContentPrefab.transform.parent = contentPanel;
            if(tutorialContent == activePage)
            {
                tutorialContent.ContentPrefab.SetActive(true);
            } else
            {
                tutorialContent.ContentPrefab.SetActive(false);
            }
            //tutorialContent.ContentPrefab.GetComponentsInChildren<Image>()[1].preserveAspect = true;
            tutorialContent.ContentPrefab.GetComponentsInChildren<Image>()[1].sprite = tutorialContent.ContentImageSprite;
            RectTransform content = tutorialContent.ContentPrefab.GetComponentInChildren<RectTransform>();

            
             content.GetComponentInChildren<TextMeshProUGUI>().text = tutorialContent.ContentDescription;


        }
    }

    private void ChangeActivePage(int pageIndex)
    {
        activePagePosition = pageIndex;
        titleField.text = titles[pageIndex];
        activePage = pages[pageIndex];
        UpdatePageNumber(pageIndex);
    }

    private void ChangePage(int pageIndex)
    {
        activePage.ContentPrefab.SetActive(false);
        ChangeActivePage(pageIndex);
        activePage.ContentPrefab.SetActive(true);
    }






    public void UpdatePageNumber(int pageIndex)
    {
        pageNumberText.text = (pageIndex + 1).ToString() + "/" + maxPage.ToString();
    }

   public void PreviousButtonHandler()
    {
        activePagePosition = activePagePosition - 1;

        if (activePagePosition == pages.Length - 2)
        {
            nextButton.GetComponentInChildren<Text>().text = "Próximo";
        }

        if(activePagePosition == 0)
        {
            previousButton.gameObject.SetActive(false);
        }

        ChangePage(activePagePosition);


    }

    public void NextButtonHandler()
    {
        activePagePosition = activePagePosition + 1;

        if(activePagePosition == pages.Length - 1)
        {
            nextButton.GetComponentInChildren<Text>().text = "Concluir !";
        } else if(activePagePosition == pages.Length)
        {
            GameManager.instance.FinishTutorial();
            Destroy(layoutPrefab.transform.gameObject);
            return;
        }

        if (activePagePosition == 1)
        {
            previousButton.gameObject.SetActive(true);
        }

        ChangePage(activePagePosition);


    }

    // Update is called once per frame
    void Update () {
		
	}
}
