using UnityEngine;
using TMPro;


public class CoolantMonitor : MonoBehaviour
{
    public TELEMETRYDataHandler TELEMETRYDataHandler;
    public TextMeshProUGUI coolantDataText;

    private string currentEVA = "eva1";
    private string currentSuitData = "Coolant";

    void Update()
    {
        UpdateCoolantUI();
    }

    public void UpdateCoolantUI()
    {
        if (currentSuitData == "Coolant")
        {
            string coolantData = GetCoolantData();
            coolantDataText.text = $"Coolant Data for {currentEVA}:\n{coolantData}";
        }
    }

    public void SetCurrentEVA(string eva)
    {
        currentEVA = eva;
        UpdateCoolantUI();
    }

    public void SetCurrentSuitData(string suitData)
    {
        currentSuitData = suitData;
        UpdateCoolantUI();
    }

    string GetCoolantData()
    {
        float coolant_ml = TELEMETRYDataHandler.GetCoolantMl(currentEVA);
        float coolant_gas_pressure = TELEMETRYDataHandler.GetCoolantGasPressure(currentEVA);
        float coolant_liquid_pressure = TELEMETRYDataHandler.GetCoolantLiquidPressure(currentEVA);

        return $"Coolant(ml): {coolant_ml}\n" +
               $"Coolant Gas Pressure: {coolant_gas_pressure}\n" +
               $"Coolant Liquid Pressure: {coolant_liquid_pressure}";
    }
}