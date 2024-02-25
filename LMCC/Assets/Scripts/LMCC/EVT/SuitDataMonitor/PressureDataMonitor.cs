using UnityEngine;
using TMPro;

public class SuitPressureMonitor : MonoBehaviour
{
    public TELEMETRYDataHandler TELEMETRYDataHandler;
    public TextMeshProUGUI pressureDataText;

    private string currentEVA = "eva1";
    private string currentSuitData = "Pressure";

    void Update()
    {
        UpdatePressureUI();
    }

    public void UpdatePressureUI()
    {
        if (currentSuitData == "Pressure")
        {
            string pressureData = GetPressureData();
            pressureDataText.text = $"Suit Pressure Data for {currentEVA}:\n{pressureData}";
        }
    }

    public void SetCurrentEVA(string eva)
    {
        currentEVA = eva;
        UpdatePressureUI();
    }

    public void SetCurrentSuitData(string suitData)
    {
        currentSuitData = suitData;
        UpdatePressureUI();
    }

    string GetPressureData()
    {
        float suitOxyPressure = TELEMETRYDataHandler.GetSuitPressureOxy(currentEVA);
        float suitCO2Pressure = TELEMETRYDataHandler.GetSuitPressureCO2(currentEVA);
        float suitOtherPressure = TELEMETRYDataHandler.GetSuitPressureOther(currentEVA);
        float suitPressureTotal = TELEMETRYDataHandler.GetSuitPresssureTotal(currentEVA);
        float helmetPressure = TELEMETRYDataHandler.GetHelmetPressure(currentEVA);

        return $"Suit Oxy Pressure: {suitOxyPressure}\n" +
               $"Suit Co2 Pressure: {suitCO2Pressure}\n" +
               $"Suit Other Pressure: {suitOtherPressure}\n" +
               $"Suit Pressure Total: {suitPressureTotal}\n" +
               $"Helmet Pressure: {helmetPressure}";
    }
}