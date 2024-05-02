using UnityEngine;
using UnityEngine.Events;

namespace Microsoft.MixedReality.Toolkit.UI
{
    public class PinPointButton : MonoBehaviour
    {
        [SerializeField]
        private GameObject pinPointIconPrefab; // Drag your pin-point icon prefab here

        [SerializeField]
        private GameObject hazardPinPrefab; // Drag your hazard pin prefab here

        // Event to be invoked when the button is clicked
        public UnityEvent onGeneralPinClick = new UnityEvent();
        public UnityEvent onHazardPinClick = new UnityEvent();

        public void DropGeneralPin()
        {
            if (pinPointIconPrefab != null)
            {
                Instantiate(pinPointIconPrefab, transform.position, Quaternion.identity);
                onGeneralPinClick.Invoke(); // Invoke the event for general pin click
            }
            else
            {
                Debug.LogError("Pin-point icon prefab is not assigned to the button.");
            }
        }

        public void DropHazardPin()
        {
            if (hazardPinPrefab != null)
            {
                Instantiate(hazardPinPrefab, transform.position, Quaternion.identity);
                onHazardPinClick.Invoke(); // Invoke the event for hazard pin click
            }
            else
            {
                Debug.LogError("Hazard-point icon prefab is not assigned to the button.");
            }
        }
    }
}