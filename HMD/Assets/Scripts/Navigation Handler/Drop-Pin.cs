using UnityEngine;
using UnityEngine.Events;

namespace Microsoft.MixedReality.Toolkit.UI 
{
    public class PinPointButton : MonoBehaviour
    {
        [SerializeField]
        private GameObject pinPointIconPrefab; // Drag your pin-point icon prefab here

        // Event to be invoked when the button is clicked
        public UnityEvent onClick = new UnityEvent();

        public void OnButtonClicked()
        {
            if (pinPointIconPrefab != null)
            {
                Instantiate(pinPointIconPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Debug.LogError("Pin-point icon prefab is not assigned to the button.");
            }
        }
    }
}

