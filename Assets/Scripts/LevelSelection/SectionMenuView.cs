using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class SectionMenuView : MonoBehaviour
{
    [SerializeField] private Section section;
    [SerializeField] private GameObject levelButtonPrefab;
    [SerializeField] private Transform buttonsPanelLocation;
    [SerializeField] private GameObject mainPanel;
    


    [SerializeField] private GameObject sectionContent;

    public void LoadSectionContent()
    {
        SectionContentView sectionContentView = sectionContent.GetComponent<SectionContentView>();
        sectionContentView.Section = section;

        sectionContent.SetActive(true);
        mainPanel.SetActive(false);
    }



    public Section Section
    {
        get
        {
            return section;
        }

        set
        {
            section = value;
        }
    }

    void Start()
    {

    }

  







    // Update is called once per frame
    void Update()
    {

    }
}
