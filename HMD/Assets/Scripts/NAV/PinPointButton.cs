using UnityEngine;
using UnityEngine.Events;
using TMPro;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.Input;

namespace Microsoft.MixedReality.Toolkit.UI
{
    public class PinPointButton : MonoBehaviour, IMixedRealitySpeechHandler
    {
        [SerializeField] private GameObject pinPointIconPrefab; // Drag your pin-point icon prefab here
        [SerializeField] private GameObject hazardPinPrefab; // Drag your hazard pin prefab here
        [SerializeField] private GameObject samplePinPrefab; // Drag your sample pin prefab here
        [SerializeField] private Transform spawnTransform; // Drag the GameObject you want to use as the spawn point here
        private int samplePinCount = 0;
        private string labelText;
        [SerializeField] private GameObject generalPinBackplate;
        [SerializeField] private GameObject hazardPinBackplate;
        [SerializeField] private GameObject samplePinBackplate;
        // Event to be invoked when the button is clicked
        public UnityEvent onGeneralPinClick = new UnityEvent();
        public UnityEvent onHazardPinClick = new UnityEvent();
        public UnityEvent onSamplePinClick = new UnityEvent();

        private bool isGeneralPinButtonPressed = false;
        private bool isHazardPinButtonPressed = false;
        private bool isSamplePinButtonPressed = false;
        private string currentLabel = string.Empty;
        private const float VISIBILITY_DISTANCE = 50f; // Distance threshold for pin visibility

        private void Start()
        {
            // Register the speech command
            CoreServices.InputSystem?.RegisterHandler<IMixedRealitySpeechHandler>(this);
        }

        private void Update()
        {
            Debug.Log(isGeneralPinButtonPressed);
            Debug.Log(isHazardPinButtonPressed);
            Debug.Log(isSamplePinButtonPressed);
        }

        private void OnDestroy()
        {
            // Unregister the speech command
            CoreServices.InputSystem?.UnregisterHandler<IMixedRealitySpeechHandler>(this);
        }

        public void OnSpeechKeywordRecognized(SpeechEventData eventData)
        {
            if (eventData.Command.Keyword.ToLower() == "place pin")
            {
                if (isGeneralPinButtonPressed)
                {
                    labelText = "";
                    PlacePin(pinPointIconPrefab);
                    isGeneralPinButtonPressed = false; // Set to false after placing the pin
                }
                else if (isHazardPinButtonPressed)
                {
                    labelText = "";
                    PlacePin(hazardPinPrefab);
                    isHazardPinButtonPressed = false; // Set to false after placing the pin
                }
                else if (isSamplePinButtonPressed)
                {
                    samplePinCount++;
                    labelText = $"Sample {samplePinCount}: ";
                    PlacePin(samplePinPrefab);
                    isSamplePinButtonPressed = false; // Set to false after placing the pin
                }
            }
        }

        public void OnGeneralPinButtonPressed()
        {
            isGeneralPinButtonPressed = true;
            isHazardPinButtonPressed = false;
            isSamplePinButtonPressed = false;
            HighlightButton(generalPinBackplate);
            UnhighlightButton(hazardPinBackplate);
            UnhighlightButton(samplePinBackplate);
        }

        public void OnHazardPinButtonPressed()
        {
            isGeneralPinButtonPressed = false;
            isHazardPinButtonPressed = true;
            isSamplePinButtonPressed = false;
            HighlightButton(hazardPinBackplate);
            UnhighlightButton(generalPinBackplate);
            UnhighlightButton(samplePinBackplate);
            
        }

        public void OnSamplePinButtonPressed()
        {
            isGeneralPinButtonPressed = false;
            isHazardPinButtonPressed = false;
            isSamplePinButtonPressed = true;
            HighlightButton(samplePinBackplate);
            UnhighlightButton(generalPinBackplate);
            UnhighlightButton(hazardPinBackplate);
            
        
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

        private void PlacePin(GameObject pinPrefab)
        {
            foreach (var source in MixedRealityToolkit.InputSystem.DetectedInputSources)
            {
                if (source.SourceType == InputSourceType.Hand)
                {
                    IMixedRealityPointer pointer = null;
                    foreach (var p in source.Pointers)
                    {
                        if (p is IMixedRealityNearPointer)
                        {
                            continue;
                        }
                        pointer = p;
                        break;
                    }

                    if (pointer != null && pointer.Result != null && pointer.Result.Details.Object != null)
                    {
                        var hitPoint = pointer.Result.Details.Point;

                        // Get the height of the headset
                        float headsetHeight = CameraCache.Main.transform.position.y;

                        // Set the pin height relative to the headset height
                        Vector3 pinPosition = new Vector3(hitPoint.x, headsetHeight - 0.5f, hitPoint.z);

                        // Check if the pin position is too close to an existing pin
                        if (!IsPinTooCloseToExistingPin(pinPosition))
                        {
                            GameObject pin = Instantiate(pinPrefab, pinPosition, Quaternion.identity);
                            // Perform any additional setup or configuration for the instantiated pin
                            // Get the TMP_Text component from the pin prefab
                            TMP_Text distanceText = pin.GetComponentInChildren<TMP_Text>();
                            if (pinPrefab == pinPointIconPrefab)
                            {
                                UnhighlightButton(generalPinBackplate);
                            }
                            if (pinPrefab == hazardPinPrefab)
                            {
                                UnhighlightButton(hazardPinBackplate);
                            }
                            if (pinPrefab == samplePinPrefab)
                            {
                                UnhighlightButton(samplePinBackplate);
                            }
                            // Start a coroutine to update the distance and rotation continuously
                            StartCoroutine(UpdatePinState(pin.transform, distanceText, labelText));
                        }
                    }
                }
            }
        }

        private System.Collections.IEnumerator UpdatePinState(Transform pinTransform, TMP_Text distanceText, string pinlabelText)
        {
            Renderer pinRenderer = pinTransform.GetComponentInChildren<Renderer>();

            while (true)
            {
                // Calculate the distance between the pin and the headset
                float distance = Vector3.Distance(pinTransform.position, CameraCache.Main.transform.position);

                // Update the text to display the label and distance
                if (!string.IsNullOrEmpty(pinlabelText))
                {
                    distanceText.text = $"{pinlabelText}  {distance:F2}m";
                }
                else
                {
                    distanceText.text = $"{distance:F2}m";
                }

                // Rotate the TMP object to face the camera
                distanceText.transform.rotation = Quaternion.LookRotation(distanceText.transform.position - CameraCache.Main.transform.position);

                // Enable or disable the pin renderer based on the distance
                pinRenderer.enabled = (distance <= VISIBILITY_DISTANCE);

                yield return null;
            }
        }

        private bool IsPinTooCloseToExistingPin(Vector3 pinPosition)
        {
            // Adjust the minimum distance as needed
            float minDistance = 0f;

            // Check the distance between the new pin position and existing pins
            foreach (GameObject existingPin in GameObject.FindGameObjectsWithTag("Pin"))
            {
                if (Vector3.Distance(pinPosition, existingPin.transform.position) < minDistance)
                {
                    return true;
                }
            }
            return false;
        }
    }
}