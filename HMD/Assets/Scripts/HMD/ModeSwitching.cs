using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ModeSwitching : MonoBehaviour
{
    public GameObject egressHUD;
    public GameObject geoHUD;
    public GameObject navigationHUD;
    public GameObject ingressHUD;
    public GameObject repairHUD;

    public TextMeshPro modeTextMeshPro;

    private void Start()
    {
        // Disable all HUDs initially
        DisableAllHUDs();
    }

    private void DisableAllHUDs()
    {
        egressHUD.SetActive(false);
        geoHUD.SetActive(false);
        navigationHUD.SetActive(false);
        ingressHUD.SetActive(false);
        repairHUD.SetActive(false);
    }

    public void ToggleEgressMode()
    {
        DisableAllHUDs();
        egressHUD.SetActive(true);
        UpdateModeText("EGRESS IN PROGRESS", Color.green);
    }

    public void ToggleGeoMode()
    {
        DisableAllHUDs();
        geoHUD.SetActive(true);
        UpdateModeText("GEO IN PROGRESS", Color.red);
    }

    public void ToggleNavigationMode()
    {
        DisableAllHUDs();
        navigationHUD.SetActive(true);
        UpdateModeText("NAVIGATION IN PROGRESS", Color.blue);
    }

    public void ToggleIngressMode()
    {
        DisableAllHUDs();
        ingressHUD.SetActive(true);
        UpdateModeText("INGRESS IN PROGRESS", Color.magenta);
    }

    public void ToggleRepairMode()
    {
        DisableAllHUDs();
        repairHUD.SetActive(true);
        UpdateModeText("REPAIR IN PROGRESS", Color.cyan);
    }

    private void UpdateModeText(string modeText, Color textColor)
    {
        if (modeTextMeshPro != null)
        {
            modeTextMeshPro.text = modeText;
            modeTextMeshPro.color = textColor;
        }
    }
}