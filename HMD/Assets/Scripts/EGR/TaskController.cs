using UnityEngine;
using TMPro;
using System.Collections;

public class TaskController : MonoBehaviour
{
    public TELEMETRYDataHandler telemetryDataHandler;
    public UIADataHandler uiaDataHandler;
    public DCUDataHandler dcuDataHandler;
    public Transform background;
    public Transform foreground;
    public TextMeshPro stepTitleTextMeshPro;
    public TextMeshPro taskTextMeshPro;
    public TextMeshPro progressTextMeshPro;
    public TextMeshPro taskStatusTextMeshPro;

    public Color completedColor = Color.green;
    public Color incompleteColor = Color.red;
    public Color inProgressColor = Color.yellow;

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

    // Helper method to get the color name based on switch state
    private string GetColorName(bool switchState)
    {
        return switchState ? "green" : "red";
    }

    private void Start()
    {
        UpdateTaskText();
    }

    private void Update()
    {
        UpdateProgressBar();
    }

    private void ResetProgressBarAndText()
    {
        // Deactivate the progress bar
        background.gameObject.SetActive(false);
        foreground.gameObject.SetActive(false);
        progressTextMeshPro.gameObject.SetActive(false);

        // Reset the progress text
        progressTextMeshPro.text = string.Empty;

        // Deactivate the status text
        taskStatusTextMeshPro.gameObject.SetActive(false);

        // Reset the status text
        taskStatusTextMeshPro.text = string.Empty;
    }

    private void UpdateProgressBar()
    {
        int currentTaskIndex = TaskManager.Instance.GetCurrentTaskIndex();
        float progress = 0f;

        switch (currentTaskIndex)
        {
            case 1: // Step 2: Power ON and Configure DCU
                    // Reset progress bar and text
                ResetProgressBarAndText();

                // Check if EV-1 and EV-2 PWR switch is ON and BATT switch is UMB
                bool isEVA1PowerOn = uiaDataHandler.GetPower("eva1");
                bool isEVA2PowerOn = uiaDataHandler.GetPower("eva2");
                bool isDCU1BattUMB = dcuDataHandler.GetBatt("eva1");
                bool isDCU2BattUMB = dcuDataHandler.GetBatt("eva2");

                if (isEVA1PowerOn && isEVA2PowerOn && isDCU1BattUMB && isDCU2BattUMB)
                {
                    progress = 1f;
                }

                // Update the status text with colored lines based on switch states
                taskStatusTextMeshPro.gameObject.SetActive(true);
                taskStatusTextMeshPro.text = $"<color={GetColorName(isEVA1PowerOn)}>EV-1 PWR: {(isEVA1PowerOn ? "ON" : "OFF")}</color>\n<color={GetColorName(isEVA2PowerOn)}>EV-2 PWR: {(isEVA2PowerOn ? "ON" : "OFF")}</color>\n<color={GetColorName(isDCU1BattUMB)}>DCU1 BATT: {(isDCU1BattUMB ? "ON" : "OFF")}</color>\n<color={GetColorName(isDCU2BattUMB)}>DCU2 BATT: {(isDCU2BattUMB ? "ON" : "OFF")}</color>";
                break;

            case 2: // Step 3: Start Depress
                    // Reset progress bar and text
                ResetProgressBarAndText();

                // Check if DEPRESS PUMP PWR switch is ON
                bool isDepressPumpPwrOn = uiaDataHandler.GetDepress();

                if (isDepressPumpPwrOn)
                {
                    progress = 1f;
                }

                // Update the status text
                taskStatusTextMeshPro.gameObject.SetActive(true);
                taskStatusTextMeshPro.text = $"DEPRESS PUMP PWR: {(isDepressPumpPwrOn ? "ON" : "OFF")}";
                break;

            case 3: // Step 4: Prepare O2 Tanks - UIA: OXYGEN O2 VENT switch to OPEN
                    // Reset progress bar and text
                ResetProgressBarAndText();

                // Check if OXYGEN O2 VENT switch is OPEN
                bool isOxygenO2VentOpen = uiaDataHandler.GetOxy_Vent();

                if (isOxygenO2VentOpen)
                {
                    progress = 1f;
                }

                // Update the status text
                taskStatusTextMeshPro.gameObject.SetActive(true);
                taskStatusTextMeshPro.text = $"OXYGEN O2 VENT: {(isOxygenO2VentOpen ? "OPEN" : "CLOSED")}";
                break;

            case 4: // Step 4: Prepare O2 Tanks - Wait until Primary and Secondary OXY tanks < 10psi
                    // Reset progress bar and text
                ResetProgressBarAndText();

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

            case 5: // Step 4: Prepare O2 Tanks - UIA: OXYGEN O2 VENT switch to CLOSE
                    // Reset progress bar and text
                ResetProgressBarAndText();

                // Check if OXYGEN O2 VENT switch is CLOSE
                bool isOxygenO2VentClose = !uiaDataHandler.GetOxy_Vent();

                if (isOxygenO2VentClose)
                {
                    progress = 1f;
                }

                // Update the status text
                taskStatusTextMeshPro.gameObject.SetActive(true);
                taskStatusTextMeshPro.text = $"OXYGEN O2 VENT: {(isOxygenO2VentClose ? "CLOSED" : "OPEN")}";
                break;

            case 6: // Step 4: Prepare O2 Tanks - Both DCUs: OXY switch to PRI
                    // Reset progress bar and text
                ResetProgressBarAndText();

                // Check if OXY switch is PRI on both DCUs
                bool isDCU1OxyPRI = dcuDataHandler.GetOxy("eva1");
                bool isDCU2OxyPRI = dcuDataHandler.GetOxy("eva2");

                if (isDCU1OxyPRI && isDCU2OxyPRI)
                {
                    progress = 1f;
                }

                // Update the status text
                taskStatusTextMeshPro.gameObject.SetActive(true);
                taskStatusTextMeshPro.text = $"<color={GetColorName(isDCU1OxyPRI)}>DCU1 OXY: {(isDCU1OxyPRI ? "PRI" : "SEC")}</color>\n<color={GetColorName(isDCU2OxyPRI)}>DCU2 OXY: {(isDCU2OxyPRI ? "PRI" : "SEC")}</color>";
                break;

            case 7: // Step 4: Prepare O2 Tanks - UIA: OXYGEN EMU-1 and EMU-2 switches to OPEN
                    // Reset progress bar and text
                ResetProgressBarAndText();

                // Check if OXYGEN EMU-1 and EMU-2 switches are OPEN
                bool isOxygenEMU1Open = uiaDataHandler.GetOxy("eva1");
                bool isOxygenEMU2Open = uiaDataHandler.GetOxy("eva2");

                if (isOxygenEMU1Open && isOxygenEMU2Open)
                {
                    progress = 1f;
                }

                // Update the status text
                taskStatusTextMeshPro.gameObject.SetActive(true);
                taskStatusTextMeshPro.text = $"<color={GetColorName(isOxygenEMU1Open)}>OXYGEN EMU-1: {(isOxygenEMU1Open ? "OPEN" : "CLOSED")}</color>\n<color={GetColorName(isOxygenEMU2Open)}>OXYGEN EMU-2: {(isOxygenEMU2Open ? "OPEN" : "CLOSED")}</color>";
                break;

            case 8: // Step 4: Prepare O2 Tanks - Wait until EV1 and EV2 Primary O2 tanks > 3000 psi
                    // Reset progress bar and text
                ResetProgressBarAndText();

                // Activate the progress bar
                background.gameObject.SetActive(true);
                foreground.gameObject.SetActive(true);
                progressTextMeshPro.gameObject.SetActive(true);

                float primaryPressureCase8 = telemetryDataHandler.GetOxyPriPressure("eva1");
                progress = Mathf.Clamp01(primaryPressureCase8 / 3000f);
                UpdateProgressBarSize(progress);
                UpdateProgressText($"{primaryPressureCase8:F0}psi (Current) / 3000psi (Goal)");

                if (primaryPressureCase8 > 3000f)
                {
                    // GoForward();
                }
                break;

            case 9: // Step 4: Prepare O2 Tanks - UIA: OXYGEN EMU-1 and EMU-2 switches to CLOSE
                    // Reset progress bar and text
                ResetProgressBarAndText();

                // Check if OXYGEN EMU-1 and EMU-2 switches are CLOSE
                bool isOxygenEMU1Close = !uiaDataHandler.GetOxy("eva1");
                bool isOxygenEMU2Close = !uiaDataHandler.GetOxy("eva2");

                if (isOxygenEMU1Close && isOxygenEMU2Close)
                {
                    progress = 1f;
                }

                // Update the status text
                taskStatusTextMeshPro.gameObject.SetActive(true);
                taskStatusTextMeshPro.text = $"<color={GetColorName(isOxygenEMU1Close)}>OXYGEN EMU-1: {(isOxygenEMU1Close ? "CLOSED" : "OPEN")}</color>\n<color={GetColorName(isOxygenEMU2Close)}>OXYGEN EMU-2: {(isOxygenEMU2Close ? "CLOSED" : "OPEN")}</color>";
                break;

            case 10: // Step 4: Prepare O2 Tanks - Both DCUs: OXY switch to SEC
                     // Reset progress bar and text
                ResetProgressBarAndText();

                // Check if OXY switch is SEC on both DCUs
                bool isDCU1OxySEC = !dcuDataHandler.GetOxy("eva1");
                bool isDCU2OxySEC = !dcuDataHandler.GetOxy("eva2");

                if (isDCU1OxySEC && isDCU2OxySEC)
                {
                    progress = 1f;
                }

                // Update the status text
                taskStatusTextMeshPro.gameObject.SetActive(true);
                taskStatusTextMeshPro.text = $"<color={GetColorName(isDCU1OxySEC)}>DCU1 OXY: {(isDCU1OxySEC ? "SEC" : "PRI")}</color>\n<color={GetColorName(isDCU2OxySEC)}>DCU2 OXY: {(isDCU2OxySEC ? "SEC" : "PRI")}</color>";
                break;

            case 11: // Step 4: Prepare O2 Tanks - UIA: OXYGEN EMU-1 and EMU-2 switches to OPEN
                     // Reset progress bar and text
                ResetProgressBarAndText();

                // Check if OXYGEN EMU-1 and EMU-2 switches are OPEN
                bool isOxygenEMU1OpenCase11 = uiaDataHandler.GetOxy("eva1");
                bool isOxygenEMU2OpenCase11 = uiaDataHandler.GetOxy("eva2");

                if (isOxygenEMU1OpenCase11 && isOxygenEMU2OpenCase11)
                {
                    progress = 1f;
                }

                // Update the status text
                taskStatusTextMeshPro.gameObject.SetActive(true);
                taskStatusTextMeshPro.text = $"<color={GetColorName(isOxygenEMU1OpenCase11)}>OXYGEN EMU-1: {(isOxygenEMU1OpenCase11 ? "OPEN" : "CLOSED")}</color>\n<color={GetColorName(isOxygenEMU2OpenCase11)}>OXYGEN EMU-2: {(isOxygenEMU2OpenCase11 ? "OPEN" : "CLOSED")}</color>";
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
                // Reset progress bar and text for other cases
                ResetProgressBarAndText();
                break;
        }

        // Update the color of the progress bar based on the progress
        if (progress < 1f)
        {
            if (progress > 0f)
            {
                foreground.GetComponent<Renderer>().material.color = inProgressColor;
            }
            else
            {
                foreground.GetComponent<Renderer>().material.color = incompleteColor;
            }
        }
        else
        {
            foreground.GetComponent<Renderer>().material.color = completedColor;
        }

        // Update the task status text
        if (progress < 1f)
        {
            taskStatusTextMeshPro.color = Color.red;
        }
        else
        {
            taskStatusTextMeshPro.color = Color.green;
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