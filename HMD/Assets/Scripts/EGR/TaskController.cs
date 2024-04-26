using UnityEngine;
using TMPro;
using System.Collections;

public class TaskController : MonoBehaviour
{
    public TELEMETRYDataHandler telemetryDataHandler;
    public UIADataHandler uiaDataHandler;
    public Transform background;
    public Transform foreground;
    public TextMeshPro stepTitleTextMeshPro;
    public TextMeshPro taskTextMeshPro;
    public TextMeshPro progressTextMeshPro;

    private string[] tasks = new string[]
    {
        "Step 1: Connect UIA to DCU\nEV1 and EV2: connect UIA and DCU umbilical.",         // Task index 0
        "Step 2: Power ON and Configure DCU\nEV-1 and EV-2: PWR switch to ON.\nBoth DCUs: BATT switch to UMB.",         // Task index 1
        "Step 3: Start Depress\nUIA: DEPRESS PUMP PWR switch to ON.",         // Task index 2
        "Step 4: Prepare O2 Tanks\nUIA: OXYGEN O2 VENT switch to OPEN.",         // Task index 3
        "Step 4: Prepare O2 Tanks\nWait until Primary and Secondary OXY tanks < 10psi.",         // Task index 4
        "Step 4: Prepare O2 Tanks\nUIA: OXYGEN O2 VENT switch to CLOSE.",         // Task index 5
        "Step 4: Prepare O2 Tanks\nBoth DCUs: OXY switch to PRI.",         // Task index 6
        "Step 4: Prepare O2 Tanks\nUIA: OXYGEN EMU-1 and EMU-2 switches to OPEN.",         // Task index 7
        "Step 4: Prepare O2 Tanks\nWait until EV1 and EV2 Primary O2 tanks > 3000 psi.",         // Task index 8
        "Step 4: Prepare O2 Tanks\nUIA: OXYGEN EMU-1 and EMU-2 switches to CLOSE.",         // Task index 9
        "Step 4: Prepare O2 Tanks\nBoth DCUs: OXY switch to SEC.",         // Task index 10
        "Step 4: Prepare O2 Tanks\nUIA: OXYGEN EMU-1 and EMU-2 switches to OPEN.",         // Task index 11
        "Step 4: Prepare O2 Tanks\nWait until EV1 and EV2 Secondary O2 tanks > 3000 psi.",         // Task index 12
        "Step 4: Prepare O2 Tanks\nUIA: OXYGEN EMU-1 and EMU-2 switches to CLOSE.",         // Task index 13
        "Step 4: Prepare O2 Tanks\nBoth DCUs: OXY switch to PRI.",         // Task index 14
        "Step 5: Prepare Water Tanks\nBoth DCUs: PUMP switch to OPEN.",         // Task index 15
        "Step 5: Prepare Water Tanks\nUIA: EV-1 and EV-2 WASTE WATER switches to OPEN.",         // Task index 16
        "Step 5: Prepare Water Tanks\nWait until EV1 and EV2 Coolant tanks < 5%.",         // Task index 17
        "Step 5: Prepare Water Tanks\nUIA: EV-1 and EV-2 WASTE WATER switches to CLOSE.",         // Task index 18
        "Step 5: Prepare Water Tanks\nUIA: EV-1 and EV-2 SUPPLY WATER switches to OPEN.",         // Task index 19
        "Step 5: Prepare Water Tanks\nWait until EV1 and EV2 Coolant tanks > 95%.",         // Task index 20
        "Step 5: Prepare Water Tanks\nUIA: EV-1 and EV-2 SUPPLY WATER switches to CLOSE.",         // Task index 21
        "Step 5: Prepare Water Tanks\nBoth DCUs: PUMP switch to CLOSE.",         // Task index 22
        "Step 6: End Depress and Check Switches\nWait until SUIT P and O2 P = 4.",         // Task index 23
        "Step 6: End Depress and Check Switches\nUIA: DEPRESS PUMP PWR switch to OFF.",         // Task index 24
        "Step 6: End Depress and Check Switches\nBoth DCUs: BATT switch to LOCAL.",         // Task index 25
        "Step 6: End Depress and Check Switches\nUIA: EV-1 and EV-2 PWR switches to OFF.",         // Task index 26
        "Step 6: End Depress and Check Switches\nBoth DCUs: verify switches:\n- OXY switch to PRI\n- COMMS switch to A\n- FAN switch to PRI\n- PUMP switch to CLOSE\n- CO2 switch to A",         // Task index 27
        "Step 7: Disconnect UIA and DCU\nEV1 and EV2: disconnect UIA and DCU umbilical.",         // Task index 28
        "Exit the pod, follow egress path, and maintain communication. Stay safe!"         // Task index 29
    };

    private int currentTaskIndex = 0;

    private void Start()
    {
        UpdateTaskText();
    }

    private void Update()
    {
        UpdateProgressBar();
    }

    private void UpdateProgressBar()
    {
        int currentTaskIndex = TaskManager.Instance.GetCurrentTaskIndex();
        float progress;

        switch (currentTaskIndex)
        {
            case 4: // Step 4: Prepare O2 Tanks - Wait until Primary and Secondary OXY tanks < 10psi
                // Activate the progress bar
                background.gameObject.SetActive(true);
                foreground.gameObject.SetActive(true);
                progressTextMeshPro.gameObject.SetActive(true);

                float primaryPressure = telemetryDataHandler.GetOxyPriPressure("eva1");
                float secondaryPressure = telemetryDataHandler.GetOxySecPressure("eva1");
                float maxPressure = Mathf.Max(primaryPressure, secondaryPressure);

                if (maxPressure < 10f)
                {
                    // If the maximum pressure is below 10, consider the task as completed
                    progress = 1f;
                }
                else
                {
                    // Calculate progress based on the maximum pressure value
                    float maxInitialPressure = 3000f; // Adjust this value based on the expected maximum initial pressure
                    progress = Mathf.Clamp01((maxInitialPressure - maxPressure) / (maxInitialPressure - 10f));
                }

                UpdateProgressBarSize(progress);
                UpdateProgressText($"{maxPressure:F0}psi (Current) < 10psi (Goal)");

                if (primaryPressure < 10f && secondaryPressure < 10f)
                {
                    // GoForward();
                }
            break;

            case 8: // Step 4: Prepare O2 Tanks - Wait until EV1 and EV2 Primary O2 tanks > 3000 psi
                // Activate the progress bar
                // Activate the progress bar
                background.gameObject.SetActive(true);
                foreground.gameObject.SetActive(true);
                progressTextMeshPro.gameObject.SetActive(true);

                primaryPressure = telemetryDataHandler.GetOxyPriPressure("eva1");
                progress = Mathf.Clamp01(primaryPressure / 3000f);
                UpdateProgressBarSize(progress);
                UpdateProgressText($"{primaryPressure:F0}psi (Current) / 3000psi (Goal)");

                if (primaryPressure > 3000f)
                {
                    // GoForward();
                }
                break;

            case 12: // Step 4: Prepare O2 Tanks - Wait until EV1 and EV2 Secondary O2 tanks > 3000 psi
                // Activate the progress bar
                background.gameObject.SetActive(true);
                foreground.gameObject.SetActive(true);
                progressTextMeshPro.gameObject.SetActive(true);

                float secondaryStorage = telemetryDataHandler.GetOxySecStorage("eva1");
                progress = Mathf.Clamp01(secondaryStorage / 3000f);
                UpdateProgressBarSize(progress);
                UpdateProgressText($"{secondaryStorage:F0}psi (Current) / 3000psi (Goal)");

                if (secondaryStorage > 3000f)
                {
                    // GoForward();
                }
                break;

            case 17: // Step 5: Prepare Water Tanks - Wait until EV1 and EV2 Coolant tanks < 5%
                // Activate the progress bar
                background.gameObject.SetActive(true);
                foreground.gameObject.SetActive(true);
                progressTextMeshPro.gameObject.SetActive(true);

                float eva1CoolantMl = telemetryDataHandler.GetCoolantMl("eva1");
                float eva2CoolantMl = telemetryDataHandler.GetCoolantMl("eva2");
                progress = Mathf.Clamp01((5f - Mathf.Max(eva1CoolantMl, eva2CoolantMl)) / 5f);
                UpdateProgressBarSize(progress);
                UpdateProgressText($"{Mathf.Max(eva1CoolantMl, eva2CoolantMl):F0}% / 5%");

                if (eva1CoolantMl < 5f && eva2CoolantMl < 5f)
                {
                    // GoForward();
                }
                break;

            case 20: // Step 5: Prepare Water Tanks - Wait until EV1 and EV2 Coolant tanks > 95%
                // Activate the progress bar
                background.gameObject.SetActive(true);
                foreground.gameObject.SetActive(true);
                progressTextMeshPro.gameObject.SetActive(true);

                eva1CoolantMl = telemetryDataHandler.GetCoolantMl("eva1");
                eva2CoolantMl = telemetryDataHandler.GetCoolantMl("eva2");
                progress = Mathf.Clamp01((Mathf.Min(eva1CoolantMl, eva2CoolantMl) - 95f) / 5f);
                UpdateProgressBarSize(progress);
                UpdateProgressText($"{Mathf.Min(eva1CoolantMl, eva2CoolantMl):F0}% / 95%");

                if (eva1CoolantMl > 95f && eva2CoolantMl > 95f)
                {
                    // GoForward();
                }
                break;

            case 23: // Step 6: End Depress and Check Switches - Wait until SUIT P and O2 P = 4
                // Activate the progress bar
                background.gameObject.SetActive(true);
                foreground.gameObject.SetActive(true);
                progressTextMeshPro.gameObject.SetActive(true);

                float suitPressureOxy = telemetryDataHandler.GetSuitPressureOxy("eva1");
                float suitPressureCO2 = telemetryDataHandler.GetSuitPressureCO2("eva1");
                float totalPressure = suitPressureOxy + suitPressureCO2;
                progress = Mathf.Clamp01((4f - Mathf.Abs(totalPressure - 4f)) / 4f);
                UpdateProgressBarSize(progress);
                UpdateProgressText($"{totalPressure:F1}psi / 4psi");

                if (Mathf.Approximately(totalPressure, 4f))
                {
                    // GoForward();
                }
                break;

            default:
                // Deactivate the progress bar for steps which require no progress bar
                background.gameObject.SetActive(false);
                foreground.gameObject.SetActive(false);
                progressTextMeshPro.gameObject.SetActive(false);
                break;
        }
    }

    private void UpdateProgressBarSize(float progress)
    {
        // In Unity, if you want to transform a 3D object in one direction, you have to
        // translate the object as you scale it to ensure there exists a pivot point.
        Vector3 backgroundScale = background.localScale;
        Vector3 foregroundScale = foreground.localScale;

        // Calculate the new scale of the foreground object
        foregroundScale.x = backgroundScale.x * progress;

        // Update the scale of the foreground object
        foreground.localScale = foregroundScale;

        // Align the left side of the foreground bar with the left side of the background bar
        Vector3 foregroundPosition = foreground.localPosition;
        Vector3 backgroundPosition = background.localPosition;
        float leftpivot = backgroundPosition.x - backgroundScale.x * 0.5f;

        foregroundPosition.x = leftpivot + foregroundScale.x * 0.5f;

        Debug.Log(foregroundPosition.x);
        Debug.Log(backgroundPosition.x);

        // Update foreground local position
        foreground.localPosition = foregroundPosition;
    }

    private void UpdateProgressText(string text)
    {
        progressTextMeshPro.text = text;
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
}