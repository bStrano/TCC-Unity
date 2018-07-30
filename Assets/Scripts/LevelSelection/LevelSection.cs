using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelSection : MonoBehaviour
{

    //[SerializeField]
    //private string title;
    //[SerializeField]
    //private Text titleText;
    [SerializeField]
    private GameObject levelButtonPrefab;
    [SerializeField]
    private Transform buttonsPanelLocation;
    [SerializeField]
    private List<Level> levelList;

    private LevelPersistence levelPersistence;
    // Use this for initialization
    void Start()
    {
        levelPersistence = new LevelPersistence();
        InitializeLevelButtons();
        //titleText.text = title;
    }

    private void InitializeLevelButtons()
    {
        int levelIndex = 0;
        foreach (Level level in levelList)
        {
            GameObject button = GameObject.Instantiate(levelButtonPrefab);

            LevelButton levelButton = button.GetComponent<LevelButton>();

            
            Debug.Log(levelPersistence.isUnlocked(level.LevelNumber.ToString()));
            if (levelPersistence.isUnlocked(level.LevelNumber.ToString()))
            {
                levelButton.LevelNumber.text = level.LevelNumber.ToString();
                levelButton.GetComponent<Button>().interactable = true;
            } else
            {
                levelButton.LevelNumber.text = "";
                levelButton.GetComponent<Button>().interactable = false;
            }
            
            ButtonLevelClickHandler(levelButton, level);
            button.transform.SetParent(buttonsPanelLocation, false);
            levelIndex++;

        }
    }


    private void ButtonLevelClickHandler(LevelButton levelButton, Level level)
    {
            levelButton.GetComponent<Button>().onClick.AddListener(() => CallSwitchScene(level) );
    }

    public void CallSwitchScene(Level level)
    {
        Debug.Log("c");
        LevelManager.instance.SwitchScene("Level_" + level.LevelNumber, level);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
