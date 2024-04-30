using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StepDisplayManager : MonoBehaviour
{
    public TextMeshPro stepDisplayText;
    private int currentStepIndex = 0;
    public SPECDataHandler specDataHandler;
    public float stepDuration = 10f; // Duration of each step in seconds

    private string[] stepInstructions = new string[]
    {
        "Step 1: Navigate to the designated sampling location.",
        "Step 2: Perform a visual inspection of the rock.",
        "Step 3: Perform XRF scan by pressing and holding the trigger.",
        "Step 4: Aim close to the sample until you hear a beep, then release the trigger.",
        "Step 5: Do not collect this sample.",
        "Step 6: Sample collection complete."
    };

    private void Start()
    {
        // Set the initial step display text
        UpdateStepDisplay(currentStepIndex);

        // Start the coroutine to automatically change steps disable when using hand menu.
        // StartCoroutine(ChangeStepsAutomatically());
    }

    private IEnumerator ChangeStepsAutomatically()
    {
        while (true)
        {
            // Wait for the specified step duration
            yield return new WaitForSeconds(stepDuration);

            // Move to the next step
            currentStepIndex = (currentStepIndex + 1) % stepInstructions.Length;

            // Update the step display
            UpdateStepDisplay(currentStepIndex);
        }
    }


    public void UpdateStepDisplay(int stepIndex)
    {
        // Clamp the step index within the valid range
        stepIndex = Mathf.Clamp(stepIndex, 0, stepInstructions.Length - 1);

        // Update the current step index
        currentStepIndex = stepIndex;

        // Update the step display text
        stepDisplayText.text = stepInstructions[currentStepIndex];

        // Check if it's the sample collection step (Step 5)
        if (currentStepIndex == 4)
        {
            // Perform XRF scan and analysis
            bool shouldCollect = PerformXRFScan();

            // Update the step instructions based on the XRF scan result
            if (shouldCollect)
            {
                string outOfRangeElement = GetOutOfRangeElement();
                stepDisplayText.text = $"Step 5: Collect this sample. ({outOfRangeElement} is out of range)";
            }
            else
            {
                stepDisplayText.text = "Step 5: Do not collect this sample. All elements within normal range";
            }
        }
    }

    private bool PerformXRFScan()
    {
        // Retrieve the XRF scan data from the SPECDataHandler
        float sio2 = specDataHandler.GetCompoundData("eva1", "SiO2");
        float tio2 = specDataHandler.GetCompoundData("eva1", "TiO2");
        float al2o3 = specDataHandler.GetCompoundData("eva1", "Al2O3");
        float feo = specDataHandler.GetCompoundData("eva1", "FeO");
        float mno = specDataHandler.GetCompoundData("eva1", "MnO");
        float mgo = specDataHandler.GetCompoundData("eva1", "MgO");
        float cao = specDataHandler.GetCompoundData("eva1", "CaO");
        float k2o = specDataHandler.GetCompoundData("eva1", "K2O");
        float p2o3 = specDataHandler.GetCompoundData("eva1", "P2O3");
        float other = specDataHandler.GetCompoundData("eva1", "other");

        // Perform analysis based on the XRF scan data
        bool shouldCollect = false;

        if (sio2 < 10 || tio2 > 1 || al2o3 > 10 || feo > 29 || mno > 1 || mgo > 20 || cao > 10 || k2o > 1 || p2o3 > 1.5f || other > 50)
        {
            shouldCollect = true;
        }

        return shouldCollect;
    }

    private string GetOutOfRangeElement()
    {
        
        // Retrieve the XRF scan data from the SPECDataHandler
        float sio2 = specDataHandler.GetCompoundData("eva1", "SiO2");
        float tio2 = specDataHandler.GetCompoundData("eva1", "TiO2");
        float al2o3 = specDataHandler.GetCompoundData("eva1", "Al2O3");
        float feo = specDataHandler.GetCompoundData("eva1", "FeO");
        float mno = specDataHandler.GetCompoundData("eva1", "MnO");
        float mgo = specDataHandler.GetCompoundData("eva1", "MgO");
        float cao = specDataHandler.GetCompoundData("eva1", "CaO");
        float k2o = specDataHandler.GetCompoundData("eva1", "K2O");
        float p2o3 = specDataHandler.GetCompoundData("eva1", "P2O3");
        float other = specDataHandler.GetCompoundData("eva1", "other");

        // Check which element is out of range
        if (sio2 < 10) return "SiO2";
        if (tio2 > 1) return "TiO2";
        if (al2o3 > 10) return "Al2O3";
        if (feo > 29) return "FeO";
        if (mno > 1) return "MnO";
        if (mgo > 20) return "MgO";
        if (cao > 10) return "CaO";
        if (k2o > 1) return "K2O";
        if (p2o3 > 1.5f) return "P2O3";
        if (other > 50) return "Other";

        return string.Empty;
    }

    public void OnPreviousStepButtonClicked()
    {
        // Move to the previous step
        UpdateStepDisplay(currentStepIndex - 1);
    }

    public void OnNextStepButtonClicked()
    {
        // Move to the next step
        UpdateStepDisplay(currentStepIndex + 1);
    }
}