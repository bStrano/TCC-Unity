using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialContentView : MonoBehaviour {
    [SerializeField] private Image contentImage;
    [SerializeField] private Text contentDescriptionText;

    private void InitializeContents(Sprite image, string description)
    {
        contentImage.sprite = image;
        //  contentImage.sprite = contentImageSprite;
        // contentDescriptionText.text = contentDescription;


    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
