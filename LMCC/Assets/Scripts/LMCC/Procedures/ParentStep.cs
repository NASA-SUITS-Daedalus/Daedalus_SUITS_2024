using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentStep : MonoBehaviour
{

    // Whether or not the current step is active
    protected bool isActive = false;

    /*
     * Check if the current step is active.
     */
    public bool IsActive()
    {
        return isActive;
    }

    /*
     * Set the step's active status.
     */
    public void SetAsActive(bool status)
    {
        isActive = status;
    }

}
