using UnityEngine;

public class CloseNavLaunch : MonoBehaviour
{
    public GameObject objectToDeactivate; // Reference to the GameObject you want to deactivate

    public void DeactivateObject()
    {
        // Check if the objectToDeactivate is not null
        if (objectToDeactivate != null)
        {
            // Deactivate the GameObject
            objectToDeactivate.SetActive(false);
        }
        else
        {
            Debug.LogError("No GameObject assigned to objectToDeactivate!");
        }
    }
}