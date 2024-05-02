using UnityEngine;
using UnityEngine.Events;

namespace Microsoft.MixedReality.Toolkit.UI
{
    public class PinPointButton : MonoBehaviour
    {
        [SerializeField] private GameObject pinPointIconPrefab; // Drag your pin-point icon prefab here
        [SerializeField] private GameObject hazardPinPrefab; // Drag your hazard pin prefab here
        [SerializeField] private Transform spawnTransform; // Drag the GameObject you want to use as the spawn point here

        // Event to be invoked when the button is clicked
        public UnityEvent onGeneralPinClick = new UnityEvent();
        public UnityEvent onHazardPinClick = new UnityEvent();

        public void DropGeneralPin()
        {
            if (pinPointIconPrefab != null && spawnTransform != null)
            {
                // Calculate the spawn position
                Vector3 spawnPosition = spawnTransform.position + (spawnTransform.TransformDirection(Vector3.forward) * 1.5f) + (spawnTransform.TransformDirection(Vector3.down) * 0.2f);

                // Instantiate the prefab with the calculated spawn position and upright rotation
                Instantiate(pinPointIconPrefab, spawnPosition, Quaternion.identity);

                onGeneralPinClick.Invoke(); // Invoke the event for general pin click
                // Debug.Log("Dropped!");
            }
            else
            {
                Debug.LogError("Pin-point icon prefab or spawnTransform is not assigned to the button.");
            }
        }

        public void DropHazardPin()
        {
            if (hazardPinPrefab != null && spawnTransform != null)
            {
                // Calculate the spawn position
                Vector3 spawnPosition = spawnTransform.position + (spawnTransform.TransformDirection(Vector3.forward) * 1.5f) + (spawnTransform.TransformDirection(Vector3.down) * 0.2f);

                // Instantiate the prefab with the calculated spawn position and upright rotation
                Instantiate(hazardPinPrefab, spawnPosition, Quaternion.identity);

                onHazardPinClick.Invoke(); // Invoke the event for hazard pin click
            }
            else
            {
                Debug.LogError("Hazard-point icon prefab or spawnTransform is not assigned to the button.");
            }
        }
    }
}