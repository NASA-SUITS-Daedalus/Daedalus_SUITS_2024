using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public OxygenMonitor oxygenMonitor;
    public SuitPressureMonitor pressureMonitor;
    public CoolantMonitor coolantMonitor;
    public FanMonitor fanMonitor;
    public ScrubberMonitor scrubberMonitor;
    public ImportantDataMonitor importantDataMonitor;

    // Called when the EVA1 button is clicked
    public void OnEVA1ButtonClick()
    {
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
        oxygenMonitor.SetCurrentEVA("eva2");
        pressureMonitor.SetCurrentEVA("eva2");
        coolantMonitor.SetCurrentEVA("eva2");
        fanMonitor.SetCurrentEVA("eva2");
        scrubberMonitor.SetCurrentEVA("eva2");
        importantDataMonitor.SetCurrentEVA("eva2");
    }
}
