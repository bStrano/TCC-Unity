using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SectionContentView : MonoBehaviour {

 
    private Section section;
    [SerializeField] private GameObject levelButtonPrefab;
    [SerializeField] private GameObject buttonsPanelLocation;
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private Text titleText;
    [SerializeField] private GameObject sectionMenu;

    public Section Section
    {
 

        set
        {
            section = value;
        }
    }

    public void RemovePreviousButtons()
    {
        Button[] buttons = buttonsPanelLocation.GetComponentsInChildren<Button>();
        Debug.Log(buttons.Length);
        foreach(Button button in buttons)
        {
            Destroy(button.gameObject);
        }
    }

    public void BackToSectionMenu()
    {
        sectionMenu.SetActive(true);
        mainPanel.SetActive(false);
    }
    public void OnEnable()
    {
        RemovePreviousButtons();
        titleText.text = section.Title;
        mainPanel.GetComponent<Image>().sprite = section.PanelBackground;
        InitializeLevelButtons();
    }

    private void InitializeLevelButtons()
    {
       
        int levelIndex = 0;
        foreach (Level level in section.LevelList)
        {
            GameObject button = GameObject.Instantiate(levelButtonPrefab);

            LevelButton levelButton = button.GetComponent<LevelButton>();


            if (LevelPersistence.IsLevelUnlocked(level.LevelNumber.ToString()) || level.Unlocked)
            {
                levelButton.LevelNumber.text = (levelIndex+1).ToString();
                levelButton.GetComponent<Button>().interactable = true;
            }
            else
            {
                levelButton.LevelNumber.text = "";
                levelButton.GetComponent<Button>().interactable = false;
            }

            ButtonLevelClickHandler(levelButton, level);
            button.transform.SetParent(buttonsPanelLocation.transform, false);
            levelIndex++;

        }
    }

    private void ButtonLevelClickHandler(LevelButton levelButton, Level level)
    {
        levelButton.GetComponent<Button>().onClick.AddListener(() => CallSwitchScene(level));
    }


    public void CallSwitchScene(Level level)
    {
        LevelManager.instance.SwitchScene("Level_" + level.LevelNumber, level);
    }


    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
