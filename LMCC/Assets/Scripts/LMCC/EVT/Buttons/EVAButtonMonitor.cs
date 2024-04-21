using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public OxygenMonitor oxygenMonitor;
    public SuitPressureMonitor pressureMonitor;
    public CoolantMonitor coolantMonitor;
    public FanMonitor fanMonitor;
    public ScrubberMonitor scrubberMonitor;
    public ImportantDataMonitor importantDataMonitor;
    public Button tssButton;
    public Button eva1;
    public Button eva2;

    // Called when the EVA1 button is clicked
    public void OnEVA1ButtonClick()
    {
        HighlightButton(eva1);
        oxygenMonitor.SetCurrentEVA("eva1");
        pressureMonitor.SetCurrentEVA("eva1");
        coolantMonitor.SetCurrentEVA("eva1");
        fanMonitor.SetCurrentEVA("eva1");
        scrubberMonitor.SetCurrentEVA("eva1");
        importantDataMonitor.SetCurrentEVA("eva1");

    }

    // Called when the EVA2 button is clicked
    public void OnEVA2ButtonClick()
    {
        HighlightButton(eva2);
        oxygenMonitor.SetCurrentEVA("eva2");
        pressureMonitor.SetCurrentEVA("eva2");
        coolantMonitor.SetCurrentEVA("eva2");
        fanMonitor.SetCurrentEVA("eva2");
        scrubberMonitor.SetCurrentEVA("eva2");
        importantDataMonitor.SetCurrentEVA("eva2");
    }

    public void OnTSSButtonClick()
    {
        HighlightButton(eva1);
    }

    private void HighlightButton(Button button)
    {
        // Unhighlight all buttons
        UnhighlightAllButtons();

        // Highlight the specified button
        button.image.color = Color.green; 
    }

    private void UnhighlightAllButtons()
    {
        eva1.image.color = Color.white;
        eva2.image.color = Color.white;
    }
}
