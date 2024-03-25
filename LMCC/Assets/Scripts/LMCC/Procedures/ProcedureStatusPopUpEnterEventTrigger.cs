using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Unity.UI;


public class ProcedureStatusPopUpEnterEventTrigger : MonoBehaviour, IPointerEnterHandler
{

    // The UI for the tooltip popup
    public GameObject tooltipUI;

    // The parent step that this trigger is associated with
    public Task task;

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
     * Show the status popup when the user hovers over the active step.
     */
    public void OnPointerEnter(PointerEventData peData)
    {
        if (tooltipUI != null && task.IsActive())
        {
            tooltipUI.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

}
