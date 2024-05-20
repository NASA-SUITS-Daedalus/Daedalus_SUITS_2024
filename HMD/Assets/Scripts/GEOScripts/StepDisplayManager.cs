using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StepDisplayManager : MonoBehaviour
{
    public TextMeshPro stepDisplayText;
    private int currentStepIndex = 0;
    public SPECDataHandler specDataHandler;
    public Color collectColor = Color.green;
    public Color doNotCollectColor = Color.red;

    // Dummy variable to store the EVA number (replace with the actual script later)
    public EVANumberHandler EVAnum;

    private string[] stepInstructions = new string[]
    {
        "Step 1: Navigate to the designated sampling location.",
        "Step 2: Perform a visual inspection of the rock.",
        "Step 3: Trigger an XRF scan by pressing and holding. Aim near the sample until you hear a beep, then release.",
        "Step 4: Do not collect this sample. All elements within normal range.",
        "Step 5: Sample collection complete."
    };
    
    private float previousSiO2;
    private float previousTiO2;
    private float previousAl2O3;
    private float previousFeO;
    private float previousMnO;
    private float previousMgO;
    private float previousCaO;
    private float previousK2O;
    private float previousP2O3;
    private float previousOther;

    private const float FloatTolerance = 0.001f;

    private void Start()
    {
        previousSiO2 = GetCompoundData("SiO2");
        previousTiO2 = GetCompoundData("TiO2");
        previousAl2O3 = GetCompoundData("Al2O3");
        previousFeO = GetCompoundData("FeO");
        previousMnO = GetCompoundData("MnO");
        previousMgO = GetCompoundData("MgO");
        previousCaO = GetCompoundData("CaO");
        previousK2O = GetCompoundData("K2O");
        previousP2O3 = GetCompoundData("P2O3");
        previousOther = GetCompoundData("other");
        // Set the initial step display text
        UpdateStepDisplay(currentStepIndex);
    }

   private void Update()
    {
        // Check for changes in compound data only during step 4
        if (currentStepIndex == 2)
        {
            if (HasCompoundDataChanged())
            {
                OnSampleCollected();
            }
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

        // Check if it's the sample collection step (Step 4)
        if (currentStepIndex == 3)
        {
            // Perform XRF scan and analysis
            bool shouldCollect = PerformXRFScan();

            // Update the step instructions based on the XRF scan result
            if (shouldCollect)
            {
                string outOfRangeElement = GetOutOfRangeElement();
                stepDisplayText.text = $"Step 4: <color=#{ColorUtility.ToHtmlStringRGBA(collectColor)}>Collect this sample. {outOfRangeElement} is out of range.</color>";
            }
            else
            {
                stepDisplayText.text = $"Step 4: <color=#{ColorUtility.ToHtmlStringRGBA(doNotCollectColor)}>Do not collect this sample. All elements within normal range.</color>";
            }
        }
    }
    private void OnSampleCollected()
    {
        // Update the step display text to confirm sample collection
        stepDisplayText.text = "Sample scanned successfully!";
    }
        private bool HasCompoundDataChanged()
    {
        // Retrieve the current compound data from the SPECDataHandler
        float currentSiO2 = GetCompoundData("SiO2");
        float currentTiO2 = GetCompoundData("TiO2");
        float currentAl2O3 = GetCompoundData("Al2O3");
        float currentFeO = GetCompoundData("FeO");
        float currentMnO = GetCompoundData("MnO");
        float currentMgO = GetCompoundData("MgO");
        float currentCaO = GetCompoundData("CaO");
        float currentK2O = GetCompoundData("K2O");
        float currentP2O3 = GetCompoundData("P2O3");
        float currentOther = GetCompoundData("other");

        // Compare the current compound data with the previous values
        bool hasChanged = (Mathf.Abs(currentSiO2 - previousSiO2) > FloatTolerance ||
                           Mathf.Abs(currentTiO2 - previousTiO2) > FloatTolerance ||
                           Mathf.Abs(currentAl2O3 - previousAl2O3) > FloatTolerance ||
                           Mathf.Abs(currentFeO - previousFeO) > FloatTolerance ||
                           Mathf.Abs(currentMnO - previousMnO) > FloatTolerance ||
                           Mathf.Abs(currentMgO - previousMgO) > FloatTolerance ||
                           Mathf.Abs(currentCaO - previousCaO) > FloatTolerance ||
                           Mathf.Abs(currentK2O - previousK2O) > FloatTolerance ||
                           Mathf.Abs(currentP2O3 - previousP2O3) > FloatTolerance ||
                           Mathf.Abs(currentOther - previousOther) > FloatTolerance);

        // Update the previous values with the current values
        previousSiO2 = currentSiO2;
        previousTiO2 = currentTiO2;
        previousAl2O3 = currentAl2O3;
        previousFeO = currentFeO;
        previousMnO = currentMnO;
        previousMgO = currentMgO;
        previousCaO = currentCaO;
        previousK2O = currentK2O;
        previousP2O3 = currentP2O3;
        previousOther = currentOther;

        return hasChanged;
    }

    private bool PerformXRFScan()
    {
        // Retrieve the XRF scan data from the SPECDataHandler
        float sio2 = GetCompoundData("SiO2");
        float tio2 = GetCompoundData("TiO2");
        float al2o3 = GetCompoundData("Al2O3");
        float feo = GetCompoundData("FeO");
        float mno = GetCompoundData("MnO");
        float mgo = GetCompoundData("MgO");
        float cao = GetCompoundData("CaO");
        float k2o = GetCompoundData("K2O");
        float p2o3 = GetCompoundData("P2O3");
        float other = GetCompoundData("other");

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
        float sio2 = GetCompoundData("SiO2");
        float tio2 = GetCompoundData("TiO2");
        float al2o3 = GetCompoundData("Al2O3");
        float feo = GetCompoundData("FeO");
        float mno = GetCompoundData("MnO");
        float mgo = GetCompoundData("MgO");
        float cao = GetCompoundData("CaO");
        float k2o = GetCompoundData("K2O");
        float p2o3 = GetCompoundData("P2O3");
        float other = GetCompoundData("other");

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
    private float GetCompoundData(string compound)
    {
        string eva = (EVAnum.getEVANumber() == 1) ? "eva1" : "eva2";
        return specDataHandler.GetCompoundData(eva, compound);
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
    public void OnRestartButtonClicked()
    {
        // Move to the next step
        UpdateStepDisplay(0);
    }
}
