using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlertDialog : MonoBehaviour
{
    [SerializeField] private Text messageTextField;
    [SerializeField] private Text btnTextField;



    public void CloseDialog()
    {
        this.gameObject.SetActive(false);
    }

    public void OpenDialog()
    {
        this.gameObject.SetActive(true);
    }
    


    public void SetupDialog(string message, string btnText)
    {
        this.messageTextField.text = message;
        this.btnTextField.text = btnText;
    }

    private void Start()
    {

    }
}