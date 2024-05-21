using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class ResetNav : MonoBehaviour
{
    [SerializeField] private GameObject confirmationDialog; // Reference to the confirmation dialog prefab

    public void ShowConfirmationDialog()
    {
        // Activate the confirmation dialog
        confirmationDialog.SetActive(true);
    }

    public void ConfirmReset()
    {
        // Deactivate the confirmation dialog
        confirmationDialog.SetActive(false);

        // User confirmed the reset, proceed with removing pins and breadcrumbs
        RemoveAllPins();
        RemoveAllBreadcrumbs();
    }

    public void CancelReset()
    {
        // User cancelled process, deactivate the confirmation dialog
        confirmationDialog.SetActive(false);
    }

    private void RemoveAllPins()
    {
        // Find all the pin GameObjects in the scene and destroy them
        GameObject[] pinObjects = GameObject.FindGameObjectsWithTag("Pin");
        foreach (GameObject pin in pinObjects)
        {
            Destroy(pin);
        }
    }

    private void RemoveAllBreadcrumbs()
    {
        // Find all the breadcrumb GameObjects in the scene and destroy them
        GameObject[] breadcrumbObjects = GameObject.FindGameObjectsWithTag("Breadcrumb");
        foreach (GameObject breadcrumb in breadcrumbObjects)
        {
            Destroy(breadcrumb);
        }
    }
}