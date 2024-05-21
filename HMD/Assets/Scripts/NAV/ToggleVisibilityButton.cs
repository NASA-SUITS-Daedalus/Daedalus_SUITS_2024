using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class ToggleVisibilityButton : MonoBehaviour
{
    private bool isPinVisible = true;
    private bool isBreadcrumbVisible = true;

    [SerializeField] private GameObject visibilityButtonBackplate;

    public void OnButtonClicked()
    {
        // Toggle the visibility of pins and breadcrumbs
        TogglePinVisibility();
        ToggleBreadcrumbVisibility();

        // Update the button highlight based on the visibility state
        UpdateButtonHighlight();
    }

    private void TogglePinVisibility()
    {
        // Find all game objects with the tag "Pin"
        GameObject[] pins = GameObject.FindGameObjectsWithTag("Pin");

        // Toggle the visibility of each pin
        foreach (GameObject pin in pins)
        {
            pin.SetActive(isPinVisible);
        }

        // Update the visibility flag
        isPinVisible = !isPinVisible;
    }

    private void ToggleBreadcrumbVisibility()
    {
        // Find all game objects with the tag "Breadcrumb"
        GameObject[] breadcrumbs = GameObject.FindGameObjectsWithTag("Breadcrumb");

        // Toggle the visibility of each breadcrumb
        foreach (GameObject breadcrumb in breadcrumbs)
        {
            breadcrumb.SetActive(isBreadcrumbVisible);
        }

        // Update the visibility flag
        isBreadcrumbVisible = !isBreadcrumbVisible;
    }

    private void UpdateButtonHighlight()
    {
        if (isPinVisible && isBreadcrumbVisible)
        {
            // Both pins and breadcrumbs are visible, unhighlight the button
            UnhighlightButton(visibilityButtonBackplate);
        }
        else
        {
            // All are hidden, highlight the button
            HighlightButton(visibilityButtonBackplate);
        }
    }

    private void HighlightButton(GameObject backplate)
    {
        // Activate the transparent yellow backplate
        if (backplate != null)
        {
            backplate.SetActive(true);
        }
    }

    private void UnhighlightButton(GameObject backplate)
    {
        // Deactivate the transparent yellow backplate
        if (backplate != null)
        {
            backplate.SetActive(false);
        }
    }
}