using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class Tutorial : MonoBehaviour {
    private int maxPage;
    [SerializeField] private GameObject layoutPrefab;
    [SerializeField] private Transform contentPanel;
    [SerializeField] private TutorialContent[] pages;
    private TutorialContent activePage;
    private int activePagePosition;





    // Use this for initialization
    void Start () {
        ChangeActivePage(0);

        maxPage = pages.Length;

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

            tutorialContent.ContentPrefab.GetComponent<Image>().sprite = tutorialContent.ContentImageSprite;

        }
    }

    private void ChangeActivePage(int pageIndex)
    {
        activePagePosition = pageIndex;
        activePage = pages[pageIndex];
        Debug.Log(pageIndex);
        Debug.Log(activePage);
    }

    private void ChangePage(int pageIndex)
    {

        activePage.ContentPrefab.SetActive(false);
        ChangeActivePage(pageIndex);
        activePage.ContentPrefab.SetActive(true);
    }

    public void NextButtonHandler()
    {
        Debug.Log("Next");
        ChangePage(activePagePosition+1);
    }



   public void PreviousButtonHandler()
    {

        ChangePage(activePagePosition-1);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
