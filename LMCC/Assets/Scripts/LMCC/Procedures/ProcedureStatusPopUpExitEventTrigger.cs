using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ProcedureStatusPopUpExitEventTrigger : MonoBehaviour, IPointerExitHandler
{

    // The UI for the tooltip popup
    public GameObject tooltipUI;

    /*
    * If a pop-up UI element is not provided, get an object with the tag.
    */
    public void Start()
    {
        if (tooltipUI == null)
        {
            tooltipUI = GameObject.FindGameObjectWithTag("ProcedurePopUp");
        }
    }

    /*
     * Show the status popup when the user hovers over the active task.
     */
    public void OnPointerExit(PointerEventData peData)
    {
        if (tooltipUI != null)
        {
            tooltipUI.transform.GetChild(0).gameObject.SetActive(false);
        }

    }

}
