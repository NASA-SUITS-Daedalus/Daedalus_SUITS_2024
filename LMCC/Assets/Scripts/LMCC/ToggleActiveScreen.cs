using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleActiveScreen : MonoBehaviour
{
    public GameObject egressScreen;
    public GameObject ingressScreen;
    public GameObject repairScreen;

    public Button egressButton;
    public Button ingressButton;
    public Button repairButton;

    private Color originalColor;
    private Color highlightColor = Color.yellow;

    private void Start()
    {
        // Store the original color of the buttons
        // (We use ingress because egress is by default yellow)
        originalColor = ingressButton.GetComponent<Image>().color;

        // Add click event listeners to the buttons
        egressButton.onClick.AddListener(OnEgressButtonClicked);
        ingressButton.onClick.AddListener(OnIngressButtonClicked);
        repairButton.onClick.AddListener(OnRepairButtonClicked);

        // Set Egress as the default active mode
        OnEgressButtonClicked();
    }

    private void OnEgressButtonClicked()
    {
        ActivateScreen(egressScreen);
        DeactivateScreen(ingressScreen);
        DeactivateScreen(repairScreen);

        HighlightButton(egressButton);
        UnhighlightButton(ingressButton);
        UnhighlightButton(repairButton);
    }

    private void OnIngressButtonClicked()
    {
        ActivateScreen(ingressScreen);
        DeactivateScreen(egressScreen);
        DeactivateScreen(repairScreen);

        HighlightButton(ingressButton);
        UnhighlightButton(egressButton);
        UnhighlightButton(repairButton);
    }

    private void OnRepairButtonClicked()
    {
        ActivateScreen(repairScreen);
        DeactivateScreen(egressScreen);
        DeactivateScreen(ingressScreen);

        HighlightButton(repairButton);
        UnhighlightButton(egressButton);
        UnhighlightButton(ingressButton);
    }

    private void ActivateScreen(GameObject screen)
    {
        if (screen != null)
        {
            screen.SetActive(true);
        }
    }

    private void DeactivateScreen(GameObject screen)
    {
        if (screen != null)
        {
            screen.SetActive(false);
        }
    }

    private void HighlightButton(Button button)
    {
        button.GetComponent<Image>().color = highlightColor;
    }

    private void UnhighlightButton(Button button)
    {
        button.GetComponent<Image>().color = originalColor;
    }
}