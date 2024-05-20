using UnityEngine;
using UnityEngine.UI;

public class SuitDataButtonController : MonoBehaviour
{
    public OxygenMonitor oxygenMonitor;
    public SuitPressureMonitor pressureMonitor;
    public CoolantMonitor coolantMonitor;
    public FanMonitor fanMonitor;
    public ScrubberMonitor scrubberMonitor;

    public Button oxygenButton;
    public Button pressureButton;
    public Button coolantButton;
    public Button fanButton;
    public Button scrubberButton;
    public Button tssButton;

    // Called when the Oxygen button is clicked
    public void OnOxygenButtonClick()
    {
        HighlightButton(oxygenButton);
        SetCurrentData("Oxygen", oxygenButton);
    }

    // Called when the Pressure button is clicked
    public void OnPressureButtonClick()
    {
        HighlightButton(pressureButton);
        SetCurrentData("Pressure", pressureButton);
        
    }

    public void OnCoolantButtonClick()
    {
        HighlightButton(coolantButton);
        SetCurrentData("Coolant", coolantButton);
    }

    public void OnFanButtonClick()
    {
        HighlightButton(fanButton);
        SetCurrentData("Fan", fanButton);
    }

    public void OnScrubberButtonClick()
    {
        HighlightButton(scrubberButton);
        SetCurrentData("Scrubber", scrubberButton);
    }

    public void OnTSSButtonClick()
    {
        // Highlight the Oxygen button
        
        HighlightButton(oxygenButton);
        SetCurrentData("Oxygen", oxygenButton);

        // Set the current data

    }

    // Common method to set the current data and highlight the button
    private void SetCurrentData(string data, Button button)
    {
        oxygenMonitor.SetCurrentSuitData(data);
        pressureMonitor.SetCurrentSuitData(data);
        coolantMonitor.SetCurrentSuitData(data);
        scrubberMonitor.SetCurrentSuitData(data);
        fanMonitor.SetCurrentSuitData(data);
    }

    // Helper method to highlight a specific button
    private void HighlightButton(Button button)
    {
        // Unhighlight all buttons
        UnhighlightAllButtons();

        // Highlight the specified button
        button.image.color = Color.yellow; // Change the color as needed
    }

    // Helper method to unhighlight all buttons
    private void UnhighlightAllButtons()
    {
        oxygenButton.image.color = Color.white;
        pressureButton.image.color = Color.white;
        coolantButton.image.color = Color.white;
        fanButton.image.color = Color.white;
        scrubberButton.image.color = Color.white;
    }
}
