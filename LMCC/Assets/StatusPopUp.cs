using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatusPopUp : MonoBehaviour
{

    // The text that gives more information on the current task
    protected TextMeshProUGUI infoText;

    // The text that gives the status of the relevant data
    protected TextMeshProUGUI statusText;

    // The text that gives the current task's completion status
    protected TextMeshProUGUI proceedText;

    // The default color for status text
    protected Color defaultColor = Color.white;

    // The ready color for status text
    protected Color readyColor = Color.green;

    // Start is called before the first frame update
    void Start()
    {
        // Get the relevant components from the parent object
        infoText = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        statusText = transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
        proceedText = transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>();
    }

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

    /*
     * Update the information text.
     */
    public void setInfoText(string text)
    {
        infoText.text = text;
    }

    /*
     * Update the status text.
     */
    public void setStatusText(string text, bool readyToProceed=false)
    {
        statusText.text = text;

        if (readyToProceed)
        {
            statusText.color = readyColor;
        }

        else
        {
            statusText.color = defaultColor;
        }
    }

    /*
     * Show the proceed message.
     */
    public void showProceedMessage(bool show=true)
    {
        proceedText.gameObject.SetActive(show);
    }
}
