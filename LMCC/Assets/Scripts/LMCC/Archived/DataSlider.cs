using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*
 * This is a general script that manages the suit data slider displays in
 * the LMCC interface.
 */
public class DataSlider : MonoBehaviour
{
    // The "code" for the data type (e.g., "HeartRate")
    // (so the telemetry stream client knows which value to check)
    
    // Add a public TextMeshProUGUI variable to display notifications
    public TextMeshProUGUI notificationText;

    // Referance to ElapsedTime script
    public ElapsedTime elapsedTimeScript;

    // List to store notifications
    private List<string> notifications = new List<string>();
    private Dictionary<int, string> lastNotificationTime = new Dictionary<int, string>();

    public string data = "HeartRate";

    // The slider itself
    public Slider slider;

    // The fill image (the color overlay over the base bar)
    public Image fill;

    // TODO add text to display the current value?

    // The minimum value to display
    public float minimumValue = 0;

    // The maximum value to display
    public float maximumValue = 160;

    // The upper bound before going into "danger" measurements
    // (If there is no danger upper bound,
    // enter a value that is greater than the maximum value.)
    public float dangerUpperBound = 140;

    // The upper bound before going into "caution" measurements
    // (If there is no caution upper bound,
    // enter a value that is greater than the maximum value.)
    public float cautionUpperBound = 120;

    // The lower bound before going into "caution" measurements
    // (If there is no caution lower bound,
    // enter a value that is less than the maximum value.)
    public float cautionLowerBound = 70;

    // The lower bound before going into "danger" measurements
    // (If there is no danger lower bound,
    // enter a value that is less than the maximum value.)
    public float dangerLowerBound = 60;

    // Whether or not the current data should be displayed as a percent
    public bool displayAsPercent;

    // Whether or not the current data should be displayed as a whole number
    public bool displayAsWholeNumber;

    // The current value of the data to be displayed
    float currentValue;

    // A dummy value to use to test the UI
    // DELETE LATER?
    public float dummyValue = 80;

    // Define danger/caution/safety colors
    private Color safeColor = Color.green;
    private Color cautionColor = Color.yellow;
    private Color dangerColor = Color.red;

    // Start is called before the first frame update
    void Start()
    {
        // TODO delete, this is for demonstration purposes
        currentValue = dummyValue;

        // Set the slider's minimum and maximum values
        slider.minValue = minimumValue;
        slider.maxValue = maximumValue;

        notificationText.text = "";
        Color textColor = notificationText.color;
        textColor.a = 1f;
        notificationText.color = textColor;

    }

    // Update is called once per frame
    void Update()
    {

        // Get the current value of the relevant data
        currentValue = getCurrentVal();

        // TODO delete, this is for demonstration purposes


        // Update the slider display
        slider.value = currentValue;

        // Color the fill bar according to the safety state
        colorFillSafety();

        // Check and show notifications based on heart rate tresholds
        checkAndShowNotifications();

        // Display notifications in the UI
        //DisplayNotifications();

    }

    // Get the appropriate data from the telemetry stream 
    float getCurrentVal()
    {
        // TODO set up the data that an instance of this script checks
        // For example, something like
        // switch (data)
        // {
        //      case "HeartRate":
        //          currentValue = TelemetryStreamClient.heartRate;
        // }

        // dummy value (TODO delete, this is for demonstration purposes)
        return currentValue - 0.01f;
    }

    // Check the current safety state of the data
    string checkState()
    {

        // Check if the temperature is at a safe level
        if (currentValue > cautionLowerBound && currentValue < cautionUpperBound) return "SAFE";

        // Check if the temperature is at a dangerous level
        else if (currentValue < dangerLowerBound || currentValue > dangerUpperBound) return "DANGER";

        // Otherwise, caution
        else return "CAUTION";
    }

    // Update the slider's fill bar to reflect the current safety state
    void colorFillSafety()
    {
        string state = checkState();
        switch (state)
        {
            case "SAFE":
                fill.color = safeColor;
                break;
            case "DANGER":
                fill.color = dangerColor;
                break;
            default:
                fill.color = cautionColor;
                break;
        }
    }

    void checkAndShowNotifications()
    {
        if (data == "HeartRate")
        {
            float heartRatePercentage = (currentValue / maximumValue) * 100;

            if (heartRatePercentage <= 50 && heartRatePercentage > 20)
            {
                AddNotification(50, "Heart rate is 50%");
            }
            else if (heartRatePercentage <= 20 && heartRatePercentage > 10)
            {
                AddNotification(20, "Heart rate is 20%");
            }
            else if (heartRatePercentage <= 10 && heartRatePercentage > 5)
            {
                AddNotification(10, "Heart rate is 10%");
            }
            else if (heartRatePercentage <= 5)
            {
                AddNotification(5, "Heart rate is 5%");
            }
            else
            {
                // Clear the notification if heart rate is above the thresholds
                ClearNotification();
            }
        }
    }
    void AddNotification(float threshold, string notification)
    {
        // Check if a notification for this threshold was already added at the current time
        
        if (!lastNotificationTime.ContainsKey((int)threshold))
        {
            // Add the notification
            string currentTime = elapsedTimeScript.GetFormattedTime();
            notifications.Add("Time: " + currentTime + " " + notification);

            // Update the last notification time for this threshold
            lastNotificationTime[(int)threshold] = currentTime;

            // Display notifications in the UI
            DisplayNotifications();
          
        }
    }

    void DisplayNotifications()
    {
        if (notifications != null)
        {
            notificationText.text = string.Join("\n\n", notifications);
            notificationText.text += "\n";
        }
    }


    // Clear the notification
    void ClearNotification()
    {
        if (notificationText != null)
        {
            notificationText.text = "";
        }
    }
}
