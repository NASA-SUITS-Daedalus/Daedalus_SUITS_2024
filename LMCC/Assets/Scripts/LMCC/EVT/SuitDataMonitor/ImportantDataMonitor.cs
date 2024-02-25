using UnityEngine;
using TMPro;


public class ImportantDataMonitor : MonoBehaviour
{
    public TELEMETRYDataHandler TELEMETRYDataHandler;
    public TextMeshProUGUI importantDataText;

    private string currentEVA = "eva1";

    void Update()
    {
        UpdateImportantDataUI();
    }

    public void UpdateImportantDataUI()
    {      
        string importantData = GetImportantData();
        importantDataText.text = $"Permenant Data for {currentEVA}:\n{importantData}";
    }

    public void SetCurrentEVA(string eva)
    {
        currentEVA = eva;
        UpdateImportantDataUI();
    }

    string GetImportantData()
    {
        float batt_time_left = TELEMETRYDataHandler.GetBattTimeLeft(currentEVA);
        float heart_rate = TELEMETRYDataHandler.GetHeartRate(currentEVA); 
        float oxy_consumption = TELEMETRYDataHandler.GetOxyConsumption(currentEVA);
        float co2_production = TELEMETRYDataHandler.GetCO2Production(currentEVA);
        float temperature = TELEMETRYDataHandler.GetTemparature(currentEVA);

        return $"Battery Time Left: {batt_time_left}\n" +
               $"Heart Rate: {heart_rate}\n" +
               $"Oxygen Consumption: {oxy_consumption}\n" +
               $"Co2 Production: {co2_production}\n" +
               $"Temperature: {temperature}";
    }
}