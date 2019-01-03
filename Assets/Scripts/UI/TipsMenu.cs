using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipsMenu : MonoBehaviour {
    [SerializeField] private GameObject tipPanelObject;
    [SerializeField] private Button[] tips;
    private bool isColapsed = true;

    public void ShowTip(int tipNumber)
    {
        tipPanelObject.SetActive(true);
        TipPanel tipPanel = tipPanelObject.GetComponent<TipPanel>();
        tipPanel.TitleText.text = tips[tipNumber - 1].GetComponent<Hint>().Title;
        tipPanel.MessageText.text = tips[tipNumber - 1].GetComponent<Hint>().Message;
    }

    private void ShowMenu()
    {
        int nextPosition = 30;
        foreach(Button button in tips)
        {
            button.gameObject.transform.position= new Vector2(button.gameObject.transform.position.x,button.gameObject.transform.position.y-nextPosition);
            nextPosition +=30;
        }
        isColapsed = false;
    }

    public void CloseMenu()
    {
        tipPanelObject.SetActive(false);
    }

    private void HideMenu()
    {
        int nextPosition = 30;
        foreach (Button button in tips)
        {
            button.gameObject.transform.position = new Vector2(button.gameObject.transform.position.x, button.gameObject.transform.position.y + nextPosition);
            nextPosition += 30;
        }
        isColapsed = true;
    }

    public void HandleClick()
    {
        if (isColapsed)
        {
            ShowMenu();
        } else
        {
            HideMenu();
        }
    }



	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
