using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class ResetNav : MonoBehaviour
{
    [SerializeField] private GameObject confirmationDialog; // Reference to the confirmation dialog prefab

    public void ShowConfirmationDialog()
    {
        // Instantiate the confirmation dialog
        GameObject dialogInstance = Instantiate(confirmationDialog, transform.position, Quaternion.identity);
    }

    public void ConfirmReset()
    {
        // User confirmed the reset, proceed with removing pins and breadcrumbs
        RemoveAllPins();
        RemoveAllBreadcrumbs();

        // Destroy the confirmation dialog
        Destroy(confirmationDialog);
    }

    public void CancelReset()
    {
        // User canceled the reset, destroy the confirmation dialog
        Destroy(confirmationDialog);
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