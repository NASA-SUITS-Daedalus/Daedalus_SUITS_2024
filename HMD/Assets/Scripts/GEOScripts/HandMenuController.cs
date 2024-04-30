using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMenuController : MonoBehaviour
{
    public StepDisplayManager stepDisplayManager;

    public void OnPreviousStepButtonClicked()
    {
        // Call the method in StepDisplayManager to move to the previous step
        stepDisplayManager.OnPreviousStepButtonClicked();
    }

    public void OnNextStepButtonClicked()
    {
        // Call the method in StepDisplayManager to move to the next step
        stepDisplayManager.OnNextStepButtonClicked();
    }
    public void OnRestartButtonClicked()
    {
        // Call the method in StepDisplayManager to move to the next step
        stepDisplayManager.OnRestartButtonClicked();
    }
}