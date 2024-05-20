using UnityEngine;

public class SuitDataButtonController : MonoBehaviour
{
    public OxygenMonitor oxygenMonitor;
    public SuitPressureMonitor pressureMonitor;
    public CoolantMonitor coolantMonitor;
    public FanMonitor fanMonitor;
    public ScrubberMonitor scrubberMonitor;

    // Called when the Oxygen button is clicked
    public void OnOxygenButtonClick()
    {
        oxygenMonitor.SetCurrentSuitData("Oxygen");
        pressureMonitor.SetCurrentSuitData("Oxygen");
        coolantMonitor.SetCurrentSuitData("Oxygen");
        scrubberMonitor.SetCurrentSuitData("Oxygen");
        fanMonitor.SetCurrentSuitData("Oxygen");
    }

    // Called when the Pressure button is clicked
    public void OnPressureButtonClick()
    {
        oxygenMonitor.SetCurrentSuitData("Pressure");
        pressureMonitor.SetCurrentSuitData("Pressure");
        coolantMonitor.SetCurrentSuitData("Pressure");
        scrubberMonitor.SetCurrentSuitData("Pressure");
        fanMonitor.SetCurrentSuitData("Pressure");
    }

    public void OnCoolantButtonClick()
    {
        oxygenMonitor.SetCurrentSuitData("Coolant");
        pressureMonitor.SetCurrentSuitData("Coolant");
        coolantMonitor.SetCurrentSuitData("Coolant");
        scrubberMonitor.SetCurrentSuitData("Coolant");
        fanMonitor.SetCurrentSuitData("Coolant");
    }

    public void OnFanButtonClick()
    {
        oxygenMonitor.SetCurrentSuitData("Fan");
        pressureMonitor.SetCurrentSuitData("Fan");
        coolantMonitor.SetCurrentSuitData("Fan");
        scrubberMonitor.SetCurrentSuitData("Fan");
        fanMonitor.SetCurrentSuitData("Fan");
    }

    public void OnScrubberButtonClick()
    {
        oxygenMonitor.SetCurrentSuitData("Scrubber");
        pressureMonitor.SetCurrentSuitData("Scrubber");
        coolantMonitor.SetCurrentSuitData("Scrubber");
        scrubberMonitor.SetCurrentSuitData("Scrubber");
        fanMonitor.SetCurrentSuitData("Scrubber");
    }
}
