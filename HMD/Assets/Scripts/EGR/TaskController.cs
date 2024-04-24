using UnityEngine;
using TMPro;

public class TaskCycler : MonoBehaviour
{
    public TextMeshPro stepTitleTextMeshPro;
    public TextMeshPro taskTextMeshPro;

    private string[] tasks = new string[]
    {
        "Step 1: Connect UIA to DCU\nEV1 and EV2: connect UIA and DCU umbilical.",
        "Step 2: Power ON and Configure DCU\nEV-1 and EV-2: PWR switch to ON.\nBoth DCUs: BATT switch to UMB.",
        "Step 3: Start Depress\nUIA: DEPRESS PUMP PWR switch to ON.",
        "Step 4: Prepare O2 Tanks\nUIA: OXYGEN O2 VENT switch to OPEN.",
        "Step 4: Prepare O2 Tanks\nWait until Primary and Secondary OXY tanks < 10psi.",
        "Step 4: Prepare O2 Tanks\nUIA: OXYGEN O2 VENT switch to CLOSE.",
        "Step 4: Prepare O2 Tanks\nBoth DCUs: OXY switch to PRI.",
        "Step 4: Prepare O2 Tanks\nUIA: OXYGEN EMU-1 and EMU-2 switches to OPEN.",
        "Step 4: Prepare O2 Tanks\nWait until EV1 and EV2 Primary O2 tanks > 3000 psi.",
        "Step 4: Prepare O2 Tanks\nUIA: OXYGEN EMU-1 and EMU-2 switches to CLOSE.",
        "Step 4: Prepare O2 Tanks\nBoth DCUs: OXY switch to SEC.",
        "Step 4: Prepare O2 Tanks\nUIA: OXYGEN EMU-1 and EMU-2 switches to OPEN.",
        "Step 4: Prepare O2 Tanks\nWait until EV1 and EV2 Secondary O2 tanks > 3000 psi.",
        "Step 4: Prepare O2 Tanks\nUIA: OXYGEN EMU-1 and EMU-2 switches to CLOSE.",
        "Step 4: Prepare O2 Tanks\nBoth DCUs: OXY switch to PRI.",
        "Step 5: Prepare Water Tanks\nBoth DCUs: PUMP switch to OPEN.",
        "Step 5: Prepare Water Tanks\nUIA: EV-1 and EV-2 WASTE WATER switches to OPEN.",
        "Step 5: Prepare Water Tanks\nWait until EV1 and EV2 Coolant tanks < 5%.",
        "Step 5: Prepare Water Tanks\nUIA: EV-1 and EV-2 WASTE WATER switches to CLOSE.",
        "Step 5: Prepare Water Tanks\nUIA: EV-1 and EV-2 SUPPLY WATER switches to OPEN.",
        "Step 5: Prepare Water Tanks\nWait until EV1 and EV2 Coolant tanks > 95%.",
        "Step 5: Prepare Water Tanks\nUIA: EV-1 and EV-2 SUPPLY WATER switches to CLOSE.",
        "Step 5: Prepare Water Tanks\nBoth DCUs: PUMP switch to CLOSE.",
        "Step 6: End Depress and Check Switches\nWait until SUIT P and O2 P = 4.",
        "Step 6: End Depress and Check Switches\nUIA: DEPRESS PUMP PWR switch to OFF.",
        "Step 6: End Depress and Check Switches\nBoth DCUs: BATT switch to LOCAL.",
        "Step 6: End Depress and Check Switches\nUIA: EV-1 and EV-2 PWR switches to OFF.",
        "Step 6: End Depress and Check Switches\nBoth DCUs: verify switches:\n- OXY switch to PRI\n- COMMS switch to A\n- FAN switch to PRI\n- PUMP switch to CLOSE\n- CO2 switch to A",
        "Step 7: Disconnect UIA and DCU\nEV1 and EV2: disconnect UIA and DCU umbilical.",
        "Exit the pod, follow egress path, and maintain communication. Stay safe!"
    };

    private int currentTaskIndex = 0;

    private void Start()
    {
        UpdateTaskText();
    }

    public void GoForward()
    {
        currentTaskIndex++;
        if (currentTaskIndex >= tasks.Length)
        {
            currentTaskIndex = 0;
        }
        UpdateTaskText();
    }

    public void GoBack()
    {
        currentTaskIndex--;
        if (currentTaskIndex < 0)
        {
            currentTaskIndex = tasks.Length - 1;
        }
        UpdateTaskText();
    }

    private void UpdateTaskText()
    {
        string currentTask = tasks[currentTaskIndex];
        string[] lines = currentTask.Split('\n');
        stepTitleTextMeshPro.text = lines[0];
        taskTextMeshPro.text = string.Join("\n", lines, 1, lines.Length - 1);
    }
}