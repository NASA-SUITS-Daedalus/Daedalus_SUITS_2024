using UnityEngine;
using TMPro;


public class FanMonitor : MonoBehaviour
{
    public TELEMETRYDataHandler TELEMETRYDataHandler;
    public TextMeshProUGUI fanDataText;

    private string currentEVA = "eva1";
    private string currentSuitData = "Fan";

    void Update()
    {
        UpdateFanUI();
    }

    public void UpdateFanUI()
    {
        if (currentSuitData == "Fan")
        {
            string fanData = GetFanData();
            fanDataText.text = $"Fan Data for {currentEVA}:\n{fanData}";
        }
    }

    public void SetCurrentEVA(string eva)
    {
        currentEVA = eva;
        UpdateFanUI();
    }

    public void SetCurrentSuitData(string suitData)
    {
        currentSuitData = suitData;
        UpdateFanUI();
    }

    string GetFanData()
    {
        float fan_pri_rpm = TELEMETRYDataHandler.GetFanPriRpm(currentEVA);
        float fan_sec_rpm = TELEMETRYDataHandler.GetFanSecRpm(currentEVA);

        return $"Fan Pri Rpm: {fan_pri_rpm}\n" +
               $"Fan Second Rpm: {fan_sec_rpm}\n";
    }
}