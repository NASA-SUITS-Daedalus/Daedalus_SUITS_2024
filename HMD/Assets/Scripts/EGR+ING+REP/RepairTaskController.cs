using UnityEngine;
using TMPro;
using System.Collections;

public class RepairTaskController : MonoBehaviour
{
    // Access TSS Data through other scripts
    public TELEMETRYDataHandler telemetryDataHandler;
    public DataRanges dataRanges;
    public UIADataHandler uiaDataHandler;
    public DCUDataHandler dcuDataHandler;
    public COMMDataHandler commDataHandler;

    // Progress Text Game Object
    public TextMeshPro taskStatusTextMeshPro;

    // Task Detail Game Objects
    public TextMeshPro stepTitleTextMeshPro;
    public TextMeshPro taskTextMeshPro;

    // Switch Location Game Object (This tells user where is the switch, either UIA, DCU, COMM Tower, MMRTG, or Tool box)
    public TextMeshPro SwitchLocationText;

    public Color completedColor = Color.green;
    public Color incompleteColor = Color.red;
    public Color inProgressColor = Color.yellow;

    private string[] tasks = new string[]
    {
         "Step 1: COMM Tower Screen\nEV1: Select Gear icon",         // Task index 0
        "Step 2: COMM Tower Screen\nEV1: Select Shutdown",         // Task index 1
        "Step 3: COMM Tower Screen\nEV1: Verify shutdown complete and notify EV2 and LMCC1",         // Task index 2
        "Step 4: MMRTG\nEV2: Move POWER - OFF, notify EV1 and LMCC1",         // Task index 3
        "Step 5: EV2: Navigate to COMM Tower\nEV2 Retrieve one end of power cable",         // Task index 4
        "Step 6: Tool box\nEV1: Retrieve spare cable",         // Task index 5
        "Step 7: MMRTG\nEV2: Take appropriate end of cable to MMRTG, notify EV1 and LMCC1 when at MMRTG",         // Task index 6
        "Step 8: Comm Tower\nEV1: Disconnect damaged cable from Comm Tower, notify EV2 and LMCC1",         // Task index 7
        "Step 9: MMRTG\nEV2: Disconnect damaged cable from MMRTG, notify EV1 and LMCC1",         // Task index 8
        "Step 10: COMM Tower\nEV1: Connect new cable from Comm Tower, notify EV2 and LMCC1",         // Task index 9
        "Step 11: MMRTG\nEV2: Connect new cable from MMRTG, notify EV1 and LMCC1",         // Task index 10
        "Step 12: MMRTG\nEV2: Move POWER - ON, notify EV1 and LMCC1",         // Task index 11
        "Step 13: COMM Tower\nEV1: POWER - ON, notify EV2 and LMCC1",         // Task index 12
        "Step 14: COMM Tower\nEV1: when start up complete, notify EV2 and LMCC1",         // Task index 13
        "Step 15: COMM Tower Screen\nEV1: Verify channel \"B\" is operational, notify EV2 and LMCC1",         // Task index 14
        "Step 16: ALL DCU+LMCC\nOn LMCC1 Go, switch to COM - B",         // Task index 15
        "Step 17: ALL DCU+LMCC\nPerform comm check",         // Task index 16
        "Step 18: ALL DCU+LMCC\nIf Comm good, EV1/LMCC1 switch back to COM-A, EV2/LMCC2 continue COM-B, Else all to COM - A",         // Task index 17
        "Step 19: Repair Completed"         // Task index 18
    };

    private int currentTaskIndex = 0;

    private void Start()
    {
        UpdateTaskText();
    }

    private void Update()
    {
        UpdateTaskStatus();
    }

    private void UpdateTaskStatus()
    {
        int currentTaskIndex = TaskManager.Instance.GetCurrentTaskIndex();
        UpdateSwitchLocationText(currentTaskIndex);

        switch (currentTaskIndex)
        {
            case 0: // Step 1: COMM Tower Screen - EV1 Select Gear icon
                ResetTaskStatusText();
                break;

            case 1: // Step 2: COMM Tower Screen - EV1 Select Shutdown
                // We want to shutdown the COMM tower
                UpdateTaskStatusText(!commDataHandler.GetComm_Tower());
                break;

            case 2: // Step 3: COMM Tower Screen - EV1 Verify shutdown complete and notify EV2 and LMCC1
                ResetTaskStatusText();
                break;

            case 3: // Step 4: MMRTG - EV2 Move POWER – OFF, notify EV1 and LMCC1
                ResetTaskStatusText();
                break;

            case 4: // Step 5: EV2 Navigate to Comm Tower - EV2 Retrieve one end of power cable
                ResetTaskStatusText();
                break;

            case 5: // Step 6: Tool box - EV1 Retrieve spare cable
                ResetTaskStatusText();
                break;

            case 6: // Step 7: MMRTG - EV2 Take appropriate end of cable to MMRTG, notify EV1 and LMCC1 when at MMRTG
                ResetTaskStatusText();
                break;

            case 7: // Step 8: Comm Tower - EV1 Disconnect damaged cable from Comm Tower, notify EV2 and LMCC1
                ResetTaskStatusText();
                break;

            case 8: // Step 9: MMRTG - EV2 Disconnect damaged cable from MMRTG, notify EV1 and LMCC1
                ResetTaskStatusText();
                break;

            case 9: // Step 10: Comm Tower - EV1 Connect new cable from Comm Tower, notify EV2 and LMCC1
                ResetTaskStatusText();
                break;

            case 10: // Step 11: MMRTG - EV2 Connect new cable from MMRTG, notify EV1 and LMCC1
                ResetTaskStatusText();
                break;

            case 11: // Step 12: MMRTG - EV2 Move POWER – ON, notify EV1 and LMCC1
                ResetTaskStatusText();
                break;

            case 12: // Step 13: Comm Tower - EV1 POWER – ON, notify EV2 and LMCC1
                UpdateTaskStatusText(commDataHandler.GetComm_Tower());
                break;

            case 13: // Step 14: Comm Tower - EV1 when start up complete, notify EV2 and LMCC1
                ResetTaskStatusText();
                break;

            case 14: // Step 15: Comm Tower Screen - EV1 Verify channel "B" is operational, notify EV2 and LMCC1
                ResetTaskStatusText();
                break;

            case 15: // Step 16: ALL DCU+LMCC - On LMCC1 Go, switch to COM – B
                bool isBothCommOnB = dcuDataHandler.GetComm("eva1") && dcuDataHandler.GetComm("eva2");
                UpdateTaskStatusText(isBothCommOnB);
                break;

            case 16: // Step 17: ALL DCU+LMCC - Perform comm check
                ResetTaskStatusText();
                break;

            case 17: // Step 18: ALL DCU+LMCC - If Comm good, EV1/LMCC1 switch back to COM-A, EV2/LMCC2 continue COM-B, Else all to COM – A
                ResetTaskStatusText();
                break;

            default:
                ResetTaskStatusText();
                break;
        }
    }

    private void UpdateTaskStatusText(bool isStepCompleted)
    {
        if (isStepCompleted)
        {
            taskStatusTextMeshPro.text = "Completed";
            taskStatusTextMeshPro.color = completedColor;
        }
        else
        {
            taskStatusTextMeshPro.text = "Incomplete";
            taskStatusTextMeshPro.color = incompleteColor;
        }
    }

    private void ResetTaskStatusText()
    {
        taskStatusTextMeshPro.text = "";
    }

    public void GoForward()
    {
        int currentTaskIndex = TaskManager.Instance.GetCurrentTaskIndex();
        if (currentTaskIndex < tasks.Length - 1)
        {
            TaskManager.Instance.SetCurrentTaskIndex(currentTaskIndex + 1);
            UpdateTaskText();
        }
    }

    public void GoBack()
    {
        int currentTaskIndex = TaskManager.Instance.GetCurrentTaskIndex();
        if (currentTaskIndex > 0)
        {
            TaskManager.Instance.SetCurrentTaskIndex(currentTaskIndex - 1);
            UpdateTaskText();
        }
    }

    private void UpdateTaskText()
    {
        int currentTaskIndex = TaskManager.Instance.GetCurrentTaskIndex();
        string currentTask = tasks[currentTaskIndex];
        string[] lines = currentTask.Split('\n');
        stepTitleTextMeshPro.text = lines[0];
        taskTextMeshPro.text = string.Join("\n", lines, 1, lines.Length - 1);
    }

    private void UpdateSwitchLocationText(int currentTaskIndex)
    {
        if (currentTaskIndex >= 0 && currentTaskIndex < tasks.Length)
        {
            string currentTask = tasks[currentTaskIndex];
            if (currentTask.Contains("COMM Tower") && currentTask.Contains("MMRTG"))
            {
                SwitchLocationText.text = "COMM Tower\nMMRTG";
            }
            else if (currentTask.Contains("COMM Tower"))
            {
                SwitchLocationText.text = "COMM Tower";
            }
            else if (currentTask.Contains("MMRTG"))
            {
                SwitchLocationText.text = "MMRTG";
            }
            else if (currentTask.Contains("Tool box"))
            {
                SwitchLocationText.text = "Tool box";
            }
            else if (currentTask.Contains("ALL DCU+LMCC"))
            {
                SwitchLocationText.text = "ALL DCU+LMCC";
            }
            else
            {
                SwitchLocationText.text = "";
            }
        }
        else
        {
            SwitchLocationText.text = "";
        }
    }
}