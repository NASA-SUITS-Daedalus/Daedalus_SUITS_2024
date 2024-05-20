using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusDisplayer : MonoBehaviour
{

    // EV-1 display button
    public Button ev1Button;

    // EV-2 display button
    public Button ev2Button;

    // UIA display button
    public Button uiaButton;

    // EV-1 overall display
    public GameObject ev1Display;

    // EV-1 readings
    public TextMeshProUGUI ev1Readings;

    // EV-2 overall display
    public GameObject ev2Display;

    // EV-2 readings
    public TextMeshProUGUI ev2Readings;

    // UIA overall display
    public GameObject uiaDisplay;

    // UIA readings
    public TextMeshProUGUI uiaReadings;

    // The telemetry data receiver
    public TELEMETRYDataHandler teleData;

    // The UIA data receiver
    public UIADataHandler uiaData;

    // The DCU data receiver
    public DCUDataHandler dcuData;

    // Formatter helper
    public ProcedureUtility helper;

    // Start
    void Start()
    {
        //helper = new ProcedureUtility();
    }

    // Update the readings
    void Update()
    {
        updateEVAReadings("eva1", ev1Readings);
        updateEVAReadings("eva2", ev2Readings);
        updateUIAReadings();

    }

    // Update EVA-specific readings
    void updateEVAReadings(string eva, TextMeshProUGUI readingsText)
    {
        try
        {
            readingsText.text = string.Format("{0}\n{1}\n{2}\n{3}\n{4}\n{5}\n{6}\n{7}\n{8}\n{9}",
                helper.formatSwitch(uiaData.GetPower(eva)),
                helper.formatPressure(teleData.GetOxyPriPressure(eva)),
                helper.formatPressure(teleData.GetOxySecPressure(eva)),
                helper.formatCapacity(teleData.GetCoolantMl(eva)),
                helper.formatDCUBatt(dcuData.GetBatt(eva)),
                helper.formatDCUOxy(dcuData.GetOxy(eva)),
                helper.formatDCUComms(dcuData.GetComm(eva)),
                helper.formatDCUFan(dcuData.GetFan(eva)),
                helper.formatDCUPump(dcuData.GetPump(eva)),
                helper.formatDCUCO2(dcuData.GetCO2(eva))
                );
        }
        catch
        {
            readingsText.text = string.Format("{0}\n{1}\n{2}\n{3}\n{4}\n{5}\n{6}\n{7}\n{8}\n{9}",
                helper.formatSwitch(false),
                helper.formatPressure(0f),
                helper.formatPressure(0f),
                helper.formatCapacity(0),
                helper.formatDCUBatt(true),
                helper.formatDCUOxy(false),
                helper.formatDCUComms(true),
                helper.formatDCUFan(true),
                helper.formatDCUPump(false),
                helper.formatDCUCO2(true)
                ) ; 
        }

    }

    // Update UIA readings
    void updateUIAReadings()
    {
        try
        {
            uiaReadings.text = string.Format("{0}\n{1}",
            helper.formatSwitch(uiaData.GetOxy_Vent()),
            helper.formatSwitch(uiaData.GetDepress())
            );
        }
        catch {
            uiaReadings.text = string.Format("{0}\n{1}",
            helper.formatSwitch(false),
            helper.formatSwitch(false)
            );
        }
    }

    void display(Button buttonObject, Button otherButtonObject1, Button otherButtonObject2,
        GameObject displayObject, GameObject otherObject1, GameObject otherObject2)
    {
        // Disable the other displays
        otherObject1.SetActive(false);
        otherObject2.SetActive(false);

        // Enable the other buttons
        otherButtonObject1.interactable = true;
        otherButtonObject2.interactable = true;

        // Enable the current display
        displayObject.SetActive(true);

        // Disable the current button
        buttonObject.interactable = false;

    }

    public void displayEV1()
    {
        display(ev1Button, ev2Button, uiaButton,
            ev1Display, ev2Display, uiaDisplay);
    }

    public void displayEV2()
    {
        display(ev2Button, ev1Button, uiaButton,
            ev2Display, ev1Display, uiaDisplay);
    }

    public void displayUIA()
    {
        display(uiaButton, ev2Button, ev1Button,
            uiaDisplay, ev2Display, ev1Display);
    }


}
