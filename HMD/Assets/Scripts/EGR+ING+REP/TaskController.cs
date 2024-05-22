using UnityEngine;
using TMPro;
using System.Collections;

public class TaskController : MonoBehaviour
{
    // Access TSS Data through other scripts
    public TELEMETRYDataHandler telemetryDataHandler;
    public DataRanges dataRanges;
    public UIADataHandler uiaDataHandler;
    public DCUDataHandler dcuDataHandler;

    // Progress Bar Game Objects
    public Transform ev1Background;
    public Transform ev1Foreground;
    public Transform ev2Background;
    public Transform ev2Foreground;
    public TextMeshPro ev1ProgressTextMeshPro;
    public TextMeshPro ev2ProgressTextMeshPro;

    // Overall Progress Bar Game Object
    public Transform overallBackgroundBar;
    public Transform overallForegroundBar;
    public TextMeshPro overallProgressTextMeshPro;

    // Progress Text Game Objects
    public TextMeshPro taskStatusTextMeshPro;

    // Task Detail Game Objects
    public TextMeshPro stepTitleTextMeshPro;
    public TextMeshPro taskTextMeshPro;

    // Switch Location Game Object (This tells user where is the switch, either
    // UIA or DCU)
    public TextMeshPro SwitchLocationText;

    public Color completedColor = Color.green;
    public Color incompleteColor = Color.red;
    public Color inProgressColor = Color.yellow;

    private string[] tasks = new string[]
    {
        "Step 1: Connect UIA to DCU\nEV1 and EV2: connect UIA and DCU umbilical.",         // Task index 0
        "Step 2: Power UIA ON and Configure DCU\nEV-1 and EV-2: PWR switch to ON.\nBoth DCUs: BATT switch to UMB.",         // Task index 1
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
        "Step 6: End Depress and Check Switches",         // Task index 27
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
        UpdateOverallProgressBar();
        UpdateOverallProgressText();
    }

    private void Update()
    {
        UpdateProgressBar();
    }


    // Update the Overall Egress Progress Bar + Text
    private void UpdateOverallProgressBar()
    {
        int currentTaskIndex = TaskManager.Instance.GetCurrentTaskIndex();
        int totalTasks = tasks.Length;

        float progress = (float)currentTaskIndex / (totalTasks - 1);

        Vector3 backgroundScale = overallBackgroundBar.localScale;
        Vector3 foregroundScale = overallForegroundBar.localScale;

        foregroundScale.x = backgroundScale.x * progress;
        overallForegroundBar.localScale = foregroundScale;

        Vector3 foregroundPosition = overallForegroundBar.localPosition;
        Vector3 backgroundPosition = overallBackgroundBar.localPosition;
        float leftPivot = backgroundPosition.x - backgroundScale.x * 0.5f;
        foregroundPosition.x = leftPivot + foregroundScale.x * 0.5f;
        overallForegroundBar.localPosition = foregroundPosition;
    }

    private void UpdateOverallProgressText()
    {
        int currentTaskIndex = TaskManager.Instance.GetCurrentTaskIndex();
        int totalTasks = tasks.Length;

        overallProgressTextMeshPro.text = $"Egress: {currentTaskIndex + 1}/{totalTasks} Steps";
    }

    // Update the individual step's progress bar + text
    private void UpdateProgressBar()
    {
        int currentTaskIndex = TaskManager.Instance.GetCurrentTaskIndex();
        UpdateSwitchLocationText(currentTaskIndex);
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
                ActivateProgressBar();

                float eva1PrimaryPressure = telemetryDataHandler.GetOxyPriPressure("eva1");
                float eva1SecondaryPressure = telemetryDataHandler.GetOxySecPressure("eva1");
                float eva2PrimaryPressure = telemetryDataHandler.GetOxyPriPressure("eva2");
                float eva2SecondaryPressure = telemetryDataHandler.GetOxySecPressure("eva2");

                // We want to make sure both are less than 10psi, so we use the maximum pressure of the two tanks
                float eva1MaxPressure = Mathf.Max(eva1PrimaryPressure, eva1SecondaryPressure);
                float eva2MaxPressure = Mathf.Max(eva2PrimaryPressure, eva2SecondaryPressure);

                float progressEV1 = 0f;
                float progressEV2 = 0f;

                if (eva1MaxPressure < 10f && eva2MaxPressure < 10f)
                {
                    // If both EV1 and EV2 primary pressures are below 10, consider the task as completed
                    progressEV1 = 1f;
                    progressEV2 = 1f;
                }
                else
                {
                    // Calculate progress based on the primary pressure values
                    double oxyPriPressureMax = dataRanges.oxy_pri_pressure.Max;

                    progressEV1 = Mathf.Clamp01((float)((oxyPriPressureMax - eva1MaxPressure) / (oxyPriPressureMax - 10f)));
                    progressEV2 = Mathf.Clamp01((float)((oxyPriPressureMax - eva2MaxPressure) / (oxyPriPressureMax - 10f)));
                }

                UpdateProgressBarSize(progressEV1, progressEV2);

                Debug.Log(progressEV1);
                Debug.Log(progressEV2);

                ev1ProgressTextMeshPro.text = $"EV1: {eva1MaxPressure:F0}psi (Current) < 10psi (Goal)";
                ev2ProgressTextMeshPro.text = $"EV2: {eva2MaxPressure:F0}psi (Current) < 10psi (Goal)";

                // Update the color of the progress bars based on their respective progress
                if (progressEV1 < 1f)
                {
                    if (progressEV1 > 0.01f)
                    {
                        ev1Foreground.GetComponent<Renderer>().material.color = inProgressColor;
                    }
                    else
                    {
                        ev1Foreground.GetComponent<Renderer>().material.color = incompleteColor;
                    }
                }
                else
                {
                    ev1Foreground.GetComponent<Renderer>().material.color = completedColor;
                }

                if (progressEV2 < 1f)
                {
                    if (progressEV2 > 0.01f)
                    {
                        ev2Foreground.GetComponent<Renderer>().material.color = inProgressColor;
                    }
                    else
                    {
                        ev2Foreground.GetComponent<Renderer>().material.color = incompleteColor;
                    }
                }
                else
                {
                    ev2Foreground.GetComponent<Renderer>().material.color = completedColor;
                }

                if (eva1MaxPressure <= 10f && eva2MaxPressure <= 10f)
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
                bool isDCU1OxyPRICase6 = dcuDataHandler.GetOxy("eva1");
                bool isDCU2OxyPRICase6 = dcuDataHandler.GetOxy("eva2");

                if (isDCU1OxyPRICase6 && isDCU2OxyPRICase6)
                {
                    progress = 1f;
                }

                // Update the status text
                taskStatusTextMeshPro.gameObject.SetActive(true);
                taskStatusTextMeshPro.text = $"<color={GetColorName(isDCU1OxyPRICase6)}>DCU1 OXY: {(isDCU1OxyPRICase6 ? "PRI" : "SEC")}</color>\n<color={GetColorName(isDCU2OxyPRICase6)}>DCU2 OXY: {(isDCU2OxyPRICase6 ? "PRI" : "SEC")}</color>";
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
                ActivateProgressBar();

                float eva1PrimaryPressureCase8 = telemetryDataHandler.GetOxyPriPressure("eva1");
                float eva2PrimaryPressureCase8 = telemetryDataHandler.GetOxyPriPressure("eva2");

                // I used 2999f to prevent flickering
                float progressEV1Case8 = Mathf.Clamp01(eva1PrimaryPressureCase8 / 2999f);
                float progressEV2Case8 = Mathf.Clamp01(eva2PrimaryPressureCase8 / 2999f);

                UpdateProgressBarSize(progressEV1Case8, progressEV2Case8);

                ev1ProgressTextMeshPro.text = $"EV1: {eva1PrimaryPressureCase8:F0}psi (Current) / 3000psi (Goal)";
                ev2ProgressTextMeshPro.text = $"EV2: {eva2PrimaryPressureCase8:F0}psi (Current) / 3000psi (Goal)";

                // Update the color of the progress bars based on their respective progress
                if (progressEV1Case8 < 1f)
                {
                    if (progressEV1Case8 > 0f)
                    {
                        ev1Foreground.GetComponent<Renderer>().material.color = inProgressColor;
                    }
                    else
                    {
                        ev1Foreground.GetComponent<Renderer>().material.color = incompleteColor;
                    }
                }
                else
                {
                    ev1Foreground.GetComponent<Renderer>().material.color = completedColor;
                }

                if (progressEV2Case8 < 1f)
                {
                    if (progressEV2Case8 > 0f)
                    {
                        ev2Foreground.GetComponent<Renderer>().material.color = inProgressColor;
                    }
                    else
                    {
                        ev2Foreground.GetComponent<Renderer>().material.color = incompleteColor;
                    }
                }
                else
                {
                    ev2Foreground.GetComponent<Renderer>().material.color = completedColor;
                }

                if (eva1PrimaryPressureCase8 >= 3000f && eva2PrimaryPressureCase8 >= 3000f)
                {
                    // GoForward();
                }
                break;

            case 9: // Step 4: Prepare O2 Tanks - UIA: OXYGEN EMU-1 and EMU-2 switches to CLOSE
                    // Reset progress bar and text
                ResetProgressBarAndText();

                // Check if OXYGEN EMU-1 and EMU-2 switches are CLOSE
                bool isOxygenEMU1CloseCase9 = !uiaDataHandler.GetOxy("eva1");
                bool isOxygenEMU2CloseCase9 = !uiaDataHandler.GetOxy("eva2");

                if (isOxygenEMU1CloseCase9 && isOxygenEMU2CloseCase9)
                {
                    progress = 1f;
                }

                // Update the status text
                taskStatusTextMeshPro.gameObject.SetActive(true);
                taskStatusTextMeshPro.text = $"<color={GetColorName(isOxygenEMU1CloseCase9)}>OXYGEN EMU-1: {(isOxygenEMU1CloseCase9 ? "CLOSED" : "OPEN")}</color>\n<color={GetColorName(isOxygenEMU2CloseCase9)}>OXYGEN EMU-2: {(isOxygenEMU2CloseCase9 ? "CLOSED" : "OPEN")}</color>";
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
                     // Reset progress bar and text
                ResetProgressBarAndText();

                // Activate the progress bar
                ActivateProgressBar();

                float eva1SecondaryStorage = telemetryDataHandler.GetOxySecPressure("eva1");
                float eva2SecondaryStorage = telemetryDataHandler.GetOxySecPressure("eva2");

                // Used 2999f to prevent flickering of color
                float progressEV1Case12 = Mathf.Clamp01(eva1SecondaryStorage / 2999f);
                float progressEV2Case12 = Mathf.Clamp01(eva2SecondaryStorage / 2999f);

                UpdateProgressBarSize(progressEV1Case12, progressEV2Case12);

                ev1ProgressTextMeshPro.text = $"EV1: {eva1SecondaryStorage:F0}psi (Current) / 3000psi (Goal)";
                ev2ProgressTextMeshPro.text = $"EV2: {eva2SecondaryStorage:F0}psi (Current) / 3000psi (Goal)";

                // Update the color of the progress bars based on their respective progress
                if (progressEV1Case12 < 1f)
                {
                    if (progressEV1Case12 > 0f)
                    {
                        ev1Foreground.GetComponent<Renderer>().material.color = inProgressColor;
                    }
                    else
                    {
                        ev1Foreground.GetComponent<Renderer>().material.color = incompleteColor;
                    }
                }
                else
                {
                    ev1Foreground.GetComponent<Renderer>().material.color = completedColor;
                }

                if (progressEV2Case12 < 1f)
                {
                    if (progressEV2Case12 > 0f)
                    {
                        ev2Foreground.GetComponent<Renderer>().material.color = inProgressColor;
                    }
                    else
                    {
                        ev2Foreground.GetComponent<Renderer>().material.color = incompleteColor;
                    }
                }
                else
                {
                    ev2Foreground.GetComponent<Renderer>().material.color = completedColor;
                }

                if (eva1SecondaryStorage >= 3000f && eva2SecondaryStorage >= 3000f)
                {
                    // GoForward();
                }
                break;

            case 13: // Step 4: Prepare O2 Tanks - UIA: OXYGEN EMU-1 and EMU-2 switches to CLOSE
                     // Reset progress bar and text
                ResetProgressBarAndText();

                // Check if OXYGEN EMU-1 and EMU-2 switches are CLOSE
                bool isOxygenEMU1CloseCase13 = !uiaDataHandler.GetOxy("eva1");
                bool isOxygenEMU2CloseCase13 = !uiaDataHandler.GetOxy("eva2");

                if (isOxygenEMU1CloseCase13 && isOxygenEMU2CloseCase13)
                {
                    progress = 1f;
                }

                // Update the status text
                taskStatusTextMeshPro.gameObject.SetActive(true);
                taskStatusTextMeshPro.text = $"<color={GetColorName(isOxygenEMU1CloseCase13)}>OXYGEN EMU-1: {(isOxygenEMU1CloseCase13 ? "CLOSED" : "OPEN")}</color>\n<color={GetColorName(isOxygenEMU2CloseCase13)}>OXYGEN EMU-2: {(isOxygenEMU2CloseCase13 ? "CLOSED" : "OPEN")}</color>";
                break;

            case 14: // Step 4: Prepare O2 Tanks - Both DCUs: OXY switch to PRI
                     // Reset progress bar and text
                ResetProgressBarAndText();

                // Check if OXY switch is PRI on both DCUs
                bool isDCU1OxyPRICase14 = dcuDataHandler.GetOxy("eva1");
                bool isDCU2OxyPRICase14 = dcuDataHandler.GetOxy("eva2");

                if (isDCU1OxyPRICase14 && isDCU2OxyPRICase14)
                {
                    progress = 1f;
                }

                // Update the status text
                taskStatusTextMeshPro.gameObject.SetActive(true);
                taskStatusTextMeshPro.text = $"<color={GetColorName(isDCU1OxyPRICase14)}>DCU1 OXY: {(isDCU1OxyPRICase14 ? "PRI" : "SEC")}</color>\n<color={GetColorName(isDCU2OxyPRICase14)}>DCU2 OXY: {(isDCU2OxyPRICase14 ? "PRI" : "SEC")}</color>";
                break;

            case 15: // Step 5: Prepare Water Tanks - Both DCUs: PUMP switch to OPEN
                     // Reset progress bar and text
                ResetProgressBarAndText();

                // Check if PUMP switch is OPEN on both DCUs
                bool isDCU1PumpOpen = dcuDataHandler.GetPump("eva1");
                bool isDCU2PumpOpen = dcuDataHandler.GetPump("eva2");

                if (isDCU1PumpOpen && isDCU2PumpOpen)
                {
                    progress = 1f;
                }

                // Update the status text
                taskStatusTextMeshPro.gameObject.SetActive(true);
                taskStatusTextMeshPro.text = $"<color={GetColorName(isDCU1PumpOpen)}>DCU1 PUMP: {(isDCU1PumpOpen ? "OPEN" : "CLOSED")}</color>\n<color={GetColorName(isDCU2PumpOpen)}>DCU2 PUMP: {(isDCU2PumpOpen ? "OPEN" : "CLOSED")}</color>";
                break;

            case 16: // Step 5: Prepare Water Tanks - UIA: EV-1 and EV-2 WASTE WATER switches to OPEN
                     // Reset progress bar and text
                ResetProgressBarAndText();

                // Check if EV-1 and EV-2 WASTE WATER switches are OPEN
                bool isEVA1WasteWaterOpen = uiaDataHandler.GetWater_Waste("eva1");
                bool isEVA2WasteWaterOpen = uiaDataHandler.GetWater_Waste("eva2");

                if (isEVA1WasteWaterOpen && isEVA2WasteWaterOpen)
                {
                    progress = 1f;
                }

                // Update the status text
                taskStatusTextMeshPro.gameObject.SetActive(true);
                taskStatusTextMeshPro.text = $"<color={GetColorName(isEVA1WasteWaterOpen)}>EV-1 WASTE WATER: {(isEVA1WasteWaterOpen ? "OPEN" : "CLOSED")}</color>\n<color={GetColorName(isEVA2WasteWaterOpen)}>EV-2 WASTE WATER: {(isEVA2WasteWaterOpen ? "OPEN" : "CLOSED")}</color>";
                break;

            case 17: // Step 5: Prepare Water Tanks - Wait until EV1 and EV2 Coolant tanks < 5%
                     // Reset progress bar and text
                ResetProgressBarAndText();

                // Activate the progress bar
                ActivateProgressBar();

                float eva1CoolantMl = telemetryDataHandler.GetCoolantMl("eva1");
                float eva2CoolantMl = telemetryDataHandler.GetCoolantMl("eva2");

                float progressEV1Case17 = 0f;
                float progressEV2Case17 = 0f;

                if (eva1CoolantMl < 5f && eva2CoolantMl < 5f)
                {
                    // If both EV1 and EV2 coolant levels are below 5%, consider the task as completed
                    progressEV1Case17 = 1f;
                    progressEV2Case17 = 1f;
                }
                else
                {
                    // Calculate progress based on the coolant levels
                    float coolantMlMax = 100f;

                    progressEV1Case17 = Mathf.Clamp01((coolantMlMax - eva1CoolantMl) / (coolantMlMax - 5f));
                    progressEV2Case17 = Mathf.Clamp01((coolantMlMax - eva2CoolantMl) / (coolantMlMax - 5f));
                }

                UpdateProgressBarSize(progressEV1Case17, progressEV2Case17);

                Debug.Log(progressEV1Case17);
                Debug.Log(progressEV2Case17);

                ev1ProgressTextMeshPro.text = $"EV1: {eva1CoolantMl:F0}% (Current) < 5% (Goal)";
                ev2ProgressTextMeshPro.text = $"EV2: {eva2CoolantMl:F0}% (Current) < 5% (Goal)";

                // Update the color of the progress bars based on their respective progress
                if (progressEV1Case17 < 1f)
                {
                    if (progressEV1Case17 > 0.01f)
                    {
                        ev1Foreground.GetComponent<Renderer>().material.color = inProgressColor;
                    }
                    else
                    {
                        ev1Foreground.GetComponent<Renderer>().material.color = incompleteColor;
                    }
                }
                else
                {
                    ev1Foreground.GetComponent<Renderer>().material.color = completedColor;
                }

                if (progressEV2Case17 < 1f)
                {
                    if (progressEV2Case17 > 0.01f)
                    {
                        ev2Foreground.GetComponent<Renderer>().material.color = inProgressColor;
                    }
                    else
                    {
                        ev2Foreground.GetComponent<Renderer>().material.color = incompleteColor;
                    }
                }
                else
                {
                    ev2Foreground.GetComponent<Renderer>().material.color = completedColor;
                }

                if (eva1CoolantMl <= 5f && eva2CoolantMl <= 5f)
                {
                    // GoForward();
                }
                break;

            case 18: // Step 5: Prepare Water Tanks - UIA: EV-1 and EV-2 WASTE WATER switches to CLOSE
                     // Reset progress bar and text
                ResetProgressBarAndText();

                // Check if EV-1 and EV-2 WASTE WATER switches are CLOSE
                bool isEVA1WasteWaterClose = !uiaDataHandler.GetWater_Waste("eva1");
                bool isEVA2WasteWaterClose = !uiaDataHandler.GetWater_Waste("eva2");

                if (isEVA1WasteWaterClose && isEVA2WasteWaterClose)
                {
                    progress = 1f;
                }

                // Update the status text
                taskStatusTextMeshPro.gameObject.SetActive(true);
                taskStatusTextMeshPro.text = $"<color={GetColorName(isEVA1WasteWaterClose)}>EV-1 WASTE WATER: {(isEVA1WasteWaterClose ? "CLOSED" : "OPEN")}</color>\n<color={GetColorName(isEVA2WasteWaterClose)}>EV-2 WASTE WATER: {(isEVA2WasteWaterClose ? "CLOSED" : "OPEN")}</color>";
                break;

            case 19: // Step 5: Prepare Water Tanks - UIA: EV-1 and EV-2 SUPPLY WATER switches to OPEN
                     // Reset progress bar and text
                ResetProgressBarAndText();

                // Check if EV-1 and EV-2 SUPPLY WATER switches are OPEN
                bool isEVA1SupplyWaterOpen = uiaDataHandler.GetWater_Supply("eva1");
                bool isEVA2SupplyWaterOpen = uiaDataHandler.GetWater_Supply("eva2");

                if (isEVA1SupplyWaterOpen && isEVA2SupplyWaterOpen)
                {
                    progress = 1f;
                }

                // Update the status text
                taskStatusTextMeshPro.gameObject.SetActive(true);
                taskStatusTextMeshPro.text = $"<color={GetColorName(isEVA1SupplyWaterOpen)}>EV-1 SUPPLY WATER: {(isEVA1SupplyWaterOpen ? "OPEN" : "CLOSED")}</color>\n<color={GetColorName(isEVA2SupplyWaterOpen)}>EV-2 SUPPLY WATER: {(isEVA2SupplyWaterOpen ? "OPEN" : "CLOSED")}</color>";
                break;

            case 20: // Step 5: Prepare Water Tanks - Wait until EV1 and EV2 Coolant tanks > 95%
                     // Reset progress bar and text
                ResetProgressBarAndText();

                // Activate the progress bar
                ActivateProgressBar();

                eva1CoolantMl = telemetryDataHandler.GetCoolantMl("eva1");
                eva2CoolantMl = telemetryDataHandler.GetCoolantMl("eva2");

                float progressEV1Case20 = Mathf.Clamp01(eva1CoolantMl / 95f);
                float progressEV2Case20 = Mathf.Clamp01(eva2CoolantMl / 95f);

                UpdateProgressBarSize(progressEV1Case20, progressEV2Case20);

                ev1ProgressTextMeshPro.text = $"EV1: {eva1CoolantMl:F0}% (current) / 95%";
                ev2ProgressTextMeshPro.text = $"EV2: {eva2CoolantMl:F0}% (current) / 95%";

                // Update the color of the progress bars based on their respective progress
                if (progressEV1Case20 < 1f)
                {
                    if (progressEV1Case20 > 0f)
                    {
                        ev1Foreground.GetComponent<Renderer>().material.color = inProgressColor;
                    }
                    else
                    {
                        ev1Foreground.GetComponent<Renderer>().material.color = incompleteColor;
                    }
                }
                else
                {
                    ev1Foreground.GetComponent<Renderer>().material.color = completedColor;
                }

                if (progressEV2Case20 < 1f)
                {
                    if (progressEV2Case20 > 0f)
                    {
                        ev2Foreground.GetComponent<Renderer>().material.color = inProgressColor;
                    }
                    else
                    {
                        ev2Foreground.GetComponent<Renderer>().material.color = incompleteColor;
                    }
                }
                else
                {
                    ev2Foreground.GetComponent<Renderer>().material.color = completedColor;
                }

                if (eva1CoolantMl >= 95f && eva2CoolantMl >= 95f)
                {
                    // GoForward();
                }
                break;

            case 21: // Step 5: Prepare Water Tanks - UIA: EV-1 and EV-2 SUPPLY WATER switches to CLOSE
                     // Reset progress bar and text
                ResetProgressBarAndText();

                // Check if EV-1 and EV-2 SUPPLY WATER switches are CLOSE
                bool isEVA1SupplyWaterClose = !uiaDataHandler.GetWater_Supply("eva1");
                bool isEVA2SupplyWaterClose = !uiaDataHandler.GetWater_Supply("eva2");

                if (isEVA1SupplyWaterClose && isEVA2SupplyWaterClose)
                {
                    progress = 1f;
                }

                // Update the status text
                taskStatusTextMeshPro.gameObject.SetActive(true);
                taskStatusTextMeshPro.text = $"<color={GetColorName(isEVA1SupplyWaterClose)}>EV-1 SUPPLY WATER: {(isEVA1SupplyWaterClose ? "CLOSED" : "OPEN")}</color>\n<color={GetColorName(isEVA2SupplyWaterClose)}>EV-2 SUPPLY WATER: {(isEVA2SupplyWaterClose ? "CLOSED" : "OPEN")}</color>";
                break;

            case 22: // Step 5: Prepare Water Tanks - Both DCUs: PUMP switch to CLOSE
                     // Reset progress bar and text
                ResetProgressBarAndText();

                // Check if PUMP switch is CLOSE on both DCUs
                bool isDCU1PumpCloseCase22 = !dcuDataHandler.GetPump("eva1");
                bool isDCU2PumpCloseCase22 = !dcuDataHandler.GetPump("eva2");

                if (isDCU1PumpCloseCase22 && isDCU2PumpCloseCase22)
                {
                    progress = 1f;
                }

                // Update the status text
                taskStatusTextMeshPro.gameObject.SetActive(true);
                taskStatusTextMeshPro.text = $"<color={GetColorName(isDCU1PumpCloseCase22)}>DCU1 PUMP: {(isDCU1PumpCloseCase22 ? "CLOSED" : "OPEN")}</color>\n<color={GetColorName(isDCU2PumpCloseCase22)}>DCU2 PUMP: {(isDCU2PumpCloseCase22 ? "CLOSED" : "OPEN")}</color>";
                break;

            case 23: // Step 6: End Depress and Check Switches - Wait until SUIT P and O2 P = 4
                     // Reset progress bar and text
                ResetProgressBarAndText();

                // Activate the progress bar
                ActivateProgressBar();

                float eva1SuitPressureOxy = telemetryDataHandler.GetSuitPressureOxy("eva1");
                float eva2SuitPressureOxy = telemetryDataHandler.GetSuitPressureOxy("eva2");

                float progressEV1Case23 = Mathf.Clamp01(eva1SuitPressureOxy / 4f);
                float progressEV2Case23 = Mathf.Clamp01(eva2SuitPressureOxy / 4f);

                UpdateProgressBarSize(progressEV1Case23, progressEV2Case23);

                ev1ProgressTextMeshPro.text = $"EV1: {eva1SuitPressureOxy}psi / 4psi";
                ev2ProgressTextMeshPro.text = $"EV2: {eva2SuitPressureOxy}psi / 4psi";

                // Update the color of the progress bars based on their respective progress
                if (progressEV1Case23 < 1f)
                {
                    if (progressEV1Case23 > 0f)
                    {
                        ev1Foreground.GetComponent<Renderer>().material.color = inProgressColor;
                    }
                    else
                    {
                        ev1Foreground.GetComponent<Renderer>().material.color = incompleteColor;
                    }
                }
                else
                {
                    ev1Foreground.GetComponent<Renderer>().material.color = completedColor;
                }

                if (progressEV2Case23 < 1f)
                {
                    if (progressEV2Case23 > 0f)
                    {
                        ev2Foreground.GetComponent<Renderer>().material.color = inProgressColor;
                    }
                    else
                    {
                        ev2Foreground.GetComponent<Renderer>().material.color = incompleteColor;
                    }
                }
                else
                {
                    ev2Foreground.GetComponent<Renderer>().material.color = completedColor;
                }

                if (eva1SuitPressureOxy == 4f && eva2SuitPressureOxy == 4f)
                {
                    // GoForward();
                }
                break;

            case 24: // Step 6: End Depress and Check Switches - UIA: DEPRESS PUMP PWR switch to OFF
                     // Reset progress bar and text
                ResetProgressBarAndText();

                // Check if DEPRESS PUMP PWR switch is OFF
                bool isDepressPumpPwrOff = !uiaDataHandler.GetDepress();

                if (isDepressPumpPwrOff)
                {
                    progress = 1f;
                }

                // Update the status text
                taskStatusTextMeshPro.gameObject.SetActive(true);
                taskStatusTextMeshPro.text = $"DEPRESS PUMP PWR: {(isDepressPumpPwrOff ? "OFF" : "ON")}";
                break;

            case 25: // Step 6: End Depress and Check Switches - Both DCUs: BATT switch to LOCAL
                     // Reset progress bar and text
                ResetProgressBarAndText();

                // Check if BATT switch is LOCAL on both DCUs
                bool isDCU1BattLocal = dcuDataHandler.GetBatt("eva1");
                bool isDCU2BattLocal = dcuDataHandler.GetBatt("eva2");

                if (isDCU1BattLocal && isDCU2BattLocal)
                {
                    progress = 1f;
                }

                // Update the status text
                taskStatusTextMeshPro.gameObject.SetActive(true);
                taskStatusTextMeshPro.text = $"<color={GetColorName(isDCU1BattLocal)}>DCU1 BATT: {(isDCU1BattLocal ? "LOCAL" : "UMB")}</color>\n<color={GetColorName(isDCU2BattLocal)}>DCU2 BATT: {(isDCU2BattLocal ? "LOCAL" : "UMB")}</color>";
                break;

            case 26: // Step 6: End Depress and Check Switches - UIA: EV-1 and EV-2 PWR switches to OFF
                     // Reset progress bar and text
                ResetProgressBarAndText();

                // Check if EV-1 and EV-2 PWR switches are OFF
                bool isEVA1PowerOff = !uiaDataHandler.GetPower("eva1");
                bool isEVA2PowerOff = !uiaDataHandler.GetPower("eva2");

                if (isEVA1PowerOff && isEVA2PowerOff)
                {
                    progress = 1f;
                }

                // Update the status text
                taskStatusTextMeshPro.gameObject.SetActive(true);
                taskStatusTextMeshPro.text = $"<color={GetColorName(isEVA1PowerOff)}>EV-1 PWR: {(isEVA1PowerOff ? "OFF" : "ON")}</color>\n<color={GetColorName(isEVA2PowerOff)}>EV-2 PWR: {(isEVA2PowerOff ? "OFF" : "ON")}</color>";
                break;

            case 27: // Step 6: End Depress and Check Switches - Both DCUs: verify switches
                     // Reset progress bar and text
                ResetProgressBarAndText();

                // Verify OXY switch is PRI
                bool isDCU1OxyPRI = dcuDataHandler.GetOxy("eva1");
                bool isDCU2OxyPRI = dcuDataHandler.GetOxy("eva2");
                // Verify COMMS switch is A
                bool isDCU1CommsA = dcuDataHandler.GetComm("eva1");
                bool isDCU2CommsA = dcuDataHandler.GetComm("eva2");
                // Verify FAN switch is PRI
                bool isDCU1FanPRI = dcuDataHandler.GetFan("eva1");
                bool isDCU2FanPRI = dcuDataHandler.GetFan("eva2");
                // Verify PUMP switch is CLOSE
                bool isDCU1PumpClose = !dcuDataHandler.GetPump("eva1");
                bool isDCU2PumpClose = !dcuDataHandler.GetPump("eva2");
                // Verify CO2 switch is A
                bool isDCU1CO2A = dcuDataHandler.GetCO2("eva1");
                bool isDCU2CO2A = dcuDataHandler.GetCO2("eva2");

                if (isDCU1OxyPRI && isDCU2OxyPRI && isDCU1CommsA && isDCU2CommsA && isDCU1FanPRI && isDCU2FanPRI && isDCU1PumpClose && isDCU2PumpClose && isDCU1CO2A && isDCU2CO2A)
                {
                    progress = 1f;
                }

                // Update the status text
                taskStatusTextMeshPro.gameObject.SetActive(true);
                taskStatusTextMeshPro.text = $"<color={GetColorName(isDCU1OxyPRI)}>DCU1 OXY: {(isDCU1OxyPRI ? "PRI" : "SEC")}</color>     <color={GetColorName(isDCU2OxyPRI)}>DCU2 OXY: {(isDCU2OxyPRI ? "PRI" : "SEC")}</color>\n" +
                                              $"<color={GetColorName(isDCU1CommsA)}>DCU1 COMMS: {(isDCU1CommsA ? "A" : "B")}</color>     <color={GetColorName(isDCU2CommsA)}>DCU2 COMMS: {(isDCU2CommsA ? "A" : "B")}</color>\n" +
                                              $"<color={GetColorName(isDCU1FanPRI)}>DCU1 FAN: {(isDCU1FanPRI ? "PRI" : "SEC")}</color>     <color={GetColorName(isDCU2FanPRI)}>DCU2 FAN: {(isDCU2FanPRI ? "PRI" : "SEC")}</color>\n" +
                                              $"<color={GetColorName(isDCU1PumpClose)}>DCU1 PUMP: {(isDCU1PumpClose ? "CLOSED" : "OPEN")}</color>     <color={GetColorName(isDCU2PumpClose)}>DCU2 PUMP: {(isDCU2PumpClose ? "CLOSED" : "OPEN")}</color>\n" +
                                              $"<color={GetColorName(isDCU1CO2A)}>DCU1 CO2: {(isDCU1CO2A ? "A" : "B")}</color>     <color={GetColorName(isDCU2CO2A)}>DCU2 CO2: {(isDCU2CO2A ? "A" : "B")}</color>";
                break;

            case 28: // Step 7: Disconnect UIA and DCU
                     // Reset progress bar and text
                ResetProgressBarAndText();

                // Check if EV1 and EV2 have disconnected UIA and DCU umbilical
                // You can add the condition here based on the umbilical disconnection status
                // For now, let's assume they are disconnected
                progress = 1f;
                break;

            default:
                // Reset progress bar and text for other cases
                ResetProgressBarAndText();
                break;
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

    private void UpdateProgressBarSize(float progressEV1, float progressEV2)
    {
        // In Unity, if you want to transform a 3D object in one direction, you have to
        // translate the object as you scale it to ensure there exists a pivot point

        // EV1 Progress Bar
        Vector3 ev1BackgroundScale = ev1Background.localScale;
        Vector3 ev1ForegroundScale = ev1Foreground.localScale;

        ev1ForegroundScale.x = ev1BackgroundScale.x * progressEV1;
        ev1Foreground.localScale = ev1ForegroundScale;

        Vector3 ev1ForegroundPosition = ev1Foreground.localPosition;
        Vector3 ev1BackgroundPosition = ev1Background.localPosition;
        float ev1LeftPivot = ev1BackgroundPosition.x - ev1BackgroundScale.x * 0.5f;
        ev1ForegroundPosition.x = ev1LeftPivot + ev1ForegroundScale.x * 0.5f;
        ev1Foreground.localPosition = ev1ForegroundPosition;

        // EV2 Progress Bar
        Vector3 ev2BackgroundScale = ev2Background.localScale;
        Vector3 ev2ForegroundScale = ev2Foreground.localScale;

        ev2ForegroundScale.x = ev2BackgroundScale.x * progressEV2;
        ev2Foreground.localScale = ev2ForegroundScale;

        Vector3 ev2ForegroundPosition = ev2Foreground.localPosition;
        Vector3 ev2BackgroundPosition = ev2Background.localPosition;
        float ev2LeftPivot = ev2BackgroundPosition.x - ev2BackgroundScale.x * 0.5f;
        ev2ForegroundPosition.x = ev2LeftPivot + ev2ForegroundScale.x * 0.5f;
        ev2Foreground.localPosition = ev2ForegroundPosition;
    }

    private void ActivateProgressBar()
    {
        ev1Background.gameObject.SetActive(true);
        ev1Foreground.gameObject.SetActive(true);
        ev1ProgressTextMeshPro.gameObject.SetActive(true);

        ev2Background.gameObject.SetActive(true);
        ev2Foreground.gameObject.SetActive(true);
        ev2ProgressTextMeshPro.gameObject.SetActive(true);
    }

    private void ResetProgressBarAndText()
    {
        // Deactivate the progress bars
        ev1Background.gameObject.SetActive(false);
        ev1Foreground.gameObject.SetActive(false);
        ev1ProgressTextMeshPro.gameObject.SetActive(false);

        ev2Background.gameObject.SetActive(false);
        ev2Foreground.gameObject.SetActive(false);
        ev2ProgressTextMeshPro.gameObject.SetActive(false);

        // Reset the progress text
        ev1ProgressTextMeshPro.text = string.Empty;
        ev2ProgressTextMeshPro.text = string.Empty;

        // Deactivate the status text
        taskStatusTextMeshPro.gameObject.SetActive(false);

        // Reset the status text
        taskStatusTextMeshPro.text = string.Empty;
    }

    public void GoForward()
    {
        int currentTaskIndex = TaskManager.Instance.GetCurrentTaskIndex();
        if (currentTaskIndex < tasks.Length - 1)
        {
            TaskManager.Instance.SetCurrentTaskIndex(currentTaskIndex + 1);
            UpdateTaskText();
            UpdateOverallProgressBar();
            UpdateOverallProgressText();
        }
    }

    public void GoBack()
    {
        int currentTaskIndex = TaskManager.Instance.GetCurrentTaskIndex();
        if (currentTaskIndex > 0)
        {
            TaskManager.Instance.SetCurrentTaskIndex(currentTaskIndex - 1);
            UpdateTaskText();
            UpdateOverallProgressBar();
            UpdateOverallProgressText();
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
            if (currentTask.Contains("UIA") && currentTask.Contains("DCU"))
            {
                SwitchLocationText.text = "DCU\nUIA";
            }
            else if (currentTask.Contains("UIA"))
            {
                SwitchLocationText.text = "UIA";
            }
            else if (currentTask.Contains("DCU"))
            {
                SwitchLocationText.text = "DCU";
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