using UnityEngine;
using TMPro;


public class ScrubberMonitor : MonoBehaviour
{
    public TELEMETRYDataHandler TELEMETRYDataHandler;
    public TextMeshProUGUI scrubberDataText;

    private string currentEVA = "eva1";
    private string currentSuitData = "Scrubber";

    void Update()
    {
        UpdateScrubberUI();
    }

    public void UpdateScrubberUI()
    {
        if (currentSuitData == "Scrubber")
        {
            string scrubberData = GetScrubberData();
            scrubberDataText.text = $"Scrubber Data for {currentEVA}:\n{scrubberData}";
        }
    }

    public void SetCurrentEVA(string eva)
    {
        currentEVA = eva;
        UpdateScrubberUI();
    }

    public void SetCurrentSuitData(string suitData)
    {
        currentSuitData = suitData;
        UpdateScrubberUI();
    }

    string GetScrubberData()
    {
        float scrubber_a_co2_storage = TELEMETRYDataHandler.GetScrubber_A_CO2Storage(currentEVA);
        float scrubber_b_co2_storage = TELEMETRYDataHandler.GetScrubber_B_CO2Storage(currentEVA);

        return $"Scrubber A CO2 Storage: {scrubber_a_co2_storage}\n" +
               $"Scrubber B CO2 Storage: {scrubber_b_co2_storage}";
    }
}