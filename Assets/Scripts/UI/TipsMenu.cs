using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipsMenu : MonoBehaviour {
    [SerializeField] private GameObject tipPanelObject;
    [SerializeField] private Button[] tips;
    private float buttonHeight;
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
        float buttonHeight = this.buttonHeight + (this.buttonHeight*0.10f);
        float nextPosition = buttonHeight;
        foreach(Button button in tips)
        {
            button.gameObject.transform.position= new Vector2(button.gameObject.transform.position.x,button.gameObject.transform.position.y-nextPosition);
            nextPosition +=buttonHeight;
        }
        isColapsed = false;
    }

    public void CloseMenu()
    {
        tipPanelObject.SetActive(false);
    }

    private void HideMenu()
    {

        float buttonHeight = this.buttonHeight + (this.buttonHeight * 0.10f);
        float nextPosition = buttonHeight;
        foreach (Button button in tips)
        {
            button.gameObject.transform.position = new Vector2(button.gameObject.transform.position.x, button.gameObject.transform.position.y + nextPosition);
            nextPosition += buttonHeight;
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
        buttonHeight = tips[0].GetComponent<RectTransform>().sizeDelta.y;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
