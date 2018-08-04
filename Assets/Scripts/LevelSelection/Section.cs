using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Section {

    [SerializeField] private string title;
    [SerializeField] private Sprite panelBackground;
    [SerializeField] private List<Level> levelList;

    public List<Level> LevelList
    {
        get
        {
            return levelList;
        }

        set
        {
            levelList = value;
        }
    }

    public Sprite PanelBackground
    {
        get
        {
            return panelBackground;
        }

        set
        {
            panelBackground = value;
        }
    }

    public string Title
    {
        get
        {
            return title;
        }

        set
        {
            title = value;
        }
    }
}
