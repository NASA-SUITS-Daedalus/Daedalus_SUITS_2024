using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public OxygenMonitor oxygenMonitor;

    // Called when the EVA1 button is clicked
    public void OnEVA1ButtonClick()
    {
        oxygenMonitor.SetCurrentEVA1();
    }

    // Called when the EVA2 button is clicked
    public void OnEVA2ButtonClick()
    {
        oxygenMonitor.SetCurrentEVA2();
    }
}
