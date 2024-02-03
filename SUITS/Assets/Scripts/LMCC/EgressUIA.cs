using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EgressUIA : MonoBehaviour
{

    // Switches display button
    public Button switchesButton;

    // Levels display button
    public Button levelsButton;

    // Switches overall display
    public GameObject switchesDisplay;

    // Levels overall display
    public GameObject levelsDisplay;


    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

    void display(Button buttonObject, Button otherButtonObject,
        GameObject displayObject, GameObject otherObject)
    {
        // Disable the other display
        otherObject.SetActive(false);

        // Enable the other button
        otherButtonObject.interactable = true;

        // Enable the current display
        displayObject.SetActive(true);

        // Disable the current button
        buttonObject.interactable = false;

    }

    public void displaySwitches()
    {
        display(switchesButton, levelsButton,
            switchesDisplay, levelsDisplay);
    }

    public void displayLevels()
    {
        display(levelsButton, switchesButton,
            levelsDisplay, switchesDisplay);
    }


}
