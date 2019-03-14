using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour {

    [SerializeField] private GameObject loading;
    [SerializeField] private Text percentageText;
    [SerializeField] private Text percentageDescriptionText;

    [SerializeField] private float currentAmount;
    [SerializeField] private int maxValue; 
 

	// Use this for initialization
	void Start () {

        InitializeText();
        StartCoroutine(UpdateAmount(100));
    }

    void InitializeText()
    {
        percentageText.fontStyle = FontStyle.Bold;
        SetPercentageDescriptionText();
    }

    void SetPercentageDescriptionText()
    {
        String description = currentAmount.ToString() + " / " + maxValue.ToString();
        percentageDescriptionText.text = description;
    }

    IEnumerator UpdateAmount(float newAmount)
    {

        newAmount = newAmount / 100;

        Image loadingImage = loading.GetComponent<Image>();
        
        loading.SetActive(true);
        while(currentAmount < newAmount)
        {
            currentAmount += 0.1f;
            loadingImage.fillAmount = currentAmount;
            Debug.Log(currentAmount);
            yield return null;
        }

        SetPercentageDescriptionText();
        var percentage = currentAmount/maxValue * 100;
        if((int) percentage == 100)
        {
            percentageText.fontSize = 7;
            percentageText.text = "Concluido!";
        } else
        {
            percentageText.fontSize = 15;
            percentageText.text = ((int) currentAmount).ToString() + "%";
           
        }
    }
	
	// Update is called once per frame
	void Update () {

        

    }
}
