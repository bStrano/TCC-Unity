using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoopButton : MonoBehaviour
{
    private Image image;
    private Text text;

    void Start()
    {
        this.image = gameObject.GetComponent<Image>();
        this.text = gameObject.GetComponentInChildren<Text>();
    }

    public void ActivateLoopMode()
    {
        this.image.color = new Color(0,0,255);
        this.text.color = new Color(209,163,255);
    }
    
    public void DisableLoopMode()
    {
        this.image.color = new Color(255, 255, 255);
        this.text.color = new Color(255, 255, 255);
    }

    
}
