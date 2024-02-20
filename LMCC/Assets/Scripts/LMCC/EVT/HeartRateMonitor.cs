using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeartRateMonitor : MonoBehaviour
{
    public TELEMETRYDataHandler TELEMETRYDataHandler;
    public Slider heartRateSlider;
    public TextMeshProUGUI heartRateText;
    public Color lowColor;
    public Color highColor;

    // You can adjust these values based on the expected heart rate range
    public float lowHeartRate = 60f;
    public float highHeartRate = 100f;

    // Update is called once per frame
    void Update()
    {
        // Get the heart rate from TELEMETRYDataHandler
        float heartRate = TELEMETRYDataHandler.GetHeartRate("eva1");
        // Debug.Log(heartRate);

        // Update the heart rate slider value
        UpdateHeartRateSlider(heartRate);

        // Update the TextMeshPro text
        UpdateHeartRateText(heartRate);

        // Adjust UI color based on heart rate
        AdjustUIColor(heartRate);
    }

    void UpdateHeartRateSlider(float heartRate)
    {
        // Normalize the heart rate value between 0 and 1
        float normalizedValue = Mathf.InverseLerp(lowHeartRate, highHeartRate, heartRate);

        // Update the slider value
        heartRateSlider.value = normalizedValue;
    }

    void UpdateHeartRateText(float heartRate)
    {
        // Update the TextMeshPro text with the heart rate
        heartRateText.text = $"{heartRate} BPM";
    }

    void AdjustUIColor(float heartRate)
    {
        // Normalize the heart rate value between 0 and 1
        float normalizedValue = Mathf.InverseLerp(lowHeartRate, highHeartRate, heartRate);

        // Interpolate color based on normalized heart rate
        Color interpolatedColor = Color.Lerp(lowColor, highColor, normalizedValue);

        // Change the color of the HeartRate slider
        Image sliderImage = heartRateSlider.GetComponentInChildren<Image>();
        sliderImage.color = interpolatedColor;
    }
}
