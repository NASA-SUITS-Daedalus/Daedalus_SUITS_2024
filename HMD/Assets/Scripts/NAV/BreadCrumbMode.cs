using UnityEngine;
using UnityEngine.Events;
using TMPro;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.Input;

namespace Microsoft.MixedReality.Toolkit.UI
{
    public class BreadcrumbMode : MonoBehaviour
    {
        [SerializeField] private GameObject breadcrumbPrefab; // Drag your breadcrumb prefab here
        [SerializeField] private float distanceThreshold = 5f; // Adjust the distance threshold as needed

        // Used for highlighting when a mode is currently activated
        [SerializeField] private GameObject breadCrumbBackplate;
        public TextMeshPro breadCrumbStatusText;

        private bool isBreadcrumbModeActive = false;
        private int breadcrumbCount = 1;
        private Vector3 lastBreadcrumbPosition;
        private const float VISIBILITY_DISTANCE = 50f; // Distance threshold for breadcrumb visibility

        private void Start()
        {
            lastBreadcrumbPosition = CameraCache.Main.transform.position;
        }

        public void OnBreadcrumbModeButtonPressed()
        {
            // Toggle between the two modes
            isBreadcrumbModeActive = !isBreadcrumbModeActive;
            if (isBreadcrumbModeActive)
            {
                // Start auto drop if it is active
                StartCoroutine(AutoDropBreadcrumbs());
                HighlightButton(breadCrumbBackplate);
            }
            else
            {
                // Unhighlight button if breadcrumb mode is not active
                UnhighlightButton(breadCrumbBackplate);
            }
            UpdateBreadcrumbStatusText(); // Update the status text
        }

        private void UpdateBreadcrumbStatusText()
        {
            if (breadCrumbStatusText != null)
            {
                breadCrumbStatusText.text = isBreadcrumbModeActive ? "ON" : "OFF";
            }
        }

        private System.Collections.IEnumerator AutoDropBreadcrumbs()
        {
            while (isBreadcrumbModeActive)
            {
                // Calculate the distance between the current position and the last breadcrumb position
                float distance = Vector3.Distance(CameraCache.Main.transform.position, lastBreadcrumbPosition);

                if (distance >= distanceThreshold)
                {
                    // Drop a breadcrumb at the current position
                    GameObject breadcrumb = Instantiate(breadcrumbPrefab, CameraCache.Main.transform.position, Quaternion.identity);

                    // Get the TMP_Text component from the breadcrumb prefab
                    TMP_Text labelText = breadcrumb.GetComponentInChildren<TMP_Text>();
                    labelText.text = breadcrumbCount.ToString();

                    // Start a coroutine to update the distance and rotation continuously
                    StartCoroutine(UpdateBreadcrumbState(breadcrumb.transform, labelText));

                    breadcrumbCount++;
                    lastBreadcrumbPosition = CameraCache.Main.transform.position;
                }

                yield return null;
            }
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

        private System.Collections.IEnumerator UpdateBreadcrumbState(Transform breadcrumbTransform, TMP_Text labelText)
        {
            Renderer breadcrumbRenderer = breadcrumbTransform.GetComponentInChildren<Renderer>();

            while (true)
            {
                // Calculate the distance between the breadcrumb and the headset
                float distance = Vector3.Distance(breadcrumbTransform.position, CameraCache.Main.transform.position);

                // Rotate the TMP object to face the camera
                labelText.transform.rotation = Quaternion.LookRotation(labelText.transform.position - CameraCache.Main.transform.position);

                // Enable or disable the breadcrumb renderer based on the distance
                breadcrumbRenderer.enabled = (distance <= VISIBILITY_DISTANCE);

                yield return null;
            }
        }
    }
}