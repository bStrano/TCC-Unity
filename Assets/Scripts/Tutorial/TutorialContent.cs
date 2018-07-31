using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class TutorialContent
{

   [SerializeField] private GameObject contentPrefab;


    [SerializeField] private Sprite contentImageSprite;
    [SerializeField] private string title;
    [SerializeField] private string contentDescription;

    public GameObject ContentPrefab
    {
        get
        {
            return contentPrefab;
        }

        set
        {
            contentPrefab = value;
        }
    }

    public Sprite ContentImageSprite
    {
        get
        {
            return contentImageSprite;
        }

        set
        {
            contentImageSprite = value;
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

    public string ContentDescription
    {
        get
        {
            return contentDescription;
        }

        set
        {
            contentDescription = value;
        }
    }

    private void InitializeContents()
    {
       // contentImage = pagePrefab.GetComponent<Image>();
        //titleText = pagePrefab.GetComponent<Text>();
      //  contentImage.sprite = contentImageSprite;
      //  titleText.text = title;
       // contentDescriptionText.text = contentDescription;
        

    }


}
