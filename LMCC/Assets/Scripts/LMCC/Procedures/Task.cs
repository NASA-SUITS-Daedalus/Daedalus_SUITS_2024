using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


/*
 * The task class contains information and behaviors relevant to the tasks/
 * operators necessary to carry out procedures (such as the egress or ingress
 * procedures). 
 * 
 * Tasks are represented as a linked list, with each Task object knowing the
 * Task that follows it (if one exists). 
 * 
 * For each procedure, there should be a script that defines the "head" of the
 * Task linked list. See SampleProcedure.cs for an example. 
 */
public class Task : MonoBehaviour
{

    // The prefab that contains a task's UI elements
    public GameObject taskUI;

    // The task's text label
    public TextMeshProUGUI taskLabel;

    // More information about the current task
    public string tooltip;

    // Whether or not this task is the last one in its parent step
    public bool isLastTaskInParent;

    // Whether or not this task is the last one in the procedure overall
    public bool isLastTaskInProcedure = false;

    // The panel to show when the procedure is completed
    public GameObject procedureCompletePanel;

    // The task that follows the current one
    public Task nextTask;

    // The task's parent step object
    public ParentStep parent;

    // Whether or not the task is currently active
    protected bool isActive = false;

    // Whether or not the task has been completed
    protected bool hasBeenCompleted = false;

    // The color that a highlighted task should be
    Color highlightColor = Color.yellow;

    // The color that a task ready to be checked off should be
    Color readyColor = Color.green;

    // The color that a completed step should be
    Color completedColor = Color.gray;

    // Start function
    void Start()
    {
        // Set the UI elements

        // Get the task description???
        //string taskDescription = taskUI.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text;

        parent = transform.GetComponentInParent<ParentStep>();
    }

    /*
     * Set the task as being active.
     */
    public void makeActive()
    {
        // Make the checkbox interactable
        // (This should just be for testing)
        taskUI.GetComponent<Toggle>().interactable = true;

        // Set the current active's task's background as active
        taskUI.GetComponent<Image>().color = highlightColor;
        taskUI.GetComponent<Image>().enabled = true;

        // Set the task as active
        isActive = true;

        // Set the parent as active if it isn't already
        parent.SetAsActive(true);

    }

    /*
     * Set the task as being ready to be checked off.
     */
    public void setReady()
    {

        // Highlight the task in a different color
        taskUI.GetComponent<Image>().color = readyColor;

        // Make the checkbox interactable
        if (!hasBeenCompleted)
        {
            taskUI.GetComponent<Toggle>().interactable = true;
        }
        
    }

    /*
     * Mark a task as being complete and, if applicable, activate the next task. 
     * (Do this when the task's checkbox is clicked.)
     * 
     * If the task is the last one in its parent step, mark the entire step 
     * as being complete. 
     * 
     * Return the next task (which should be the new active task). If one does
     * not exist, return null.
     */
    public void completeTask()
    {
        // Enable the next task, if one exists
        if (nextTask != null) { nextTask.makeActive(); }

        // Make the checkbox non-interactable
        taskUI.GetComponent<Toggle>().interactable = false;

        // Disable the current task
        isActive = false;

        // Set the current task as having been completed
        hasBeenCompleted = true;

        // Set the current active task's background as inactive
        taskUI.GetComponent<Image>().enabled = false;

        // If all of the tasks in the same family are complete,
        // mark the entire panel as being completed.
        if (isLastTaskInParent)
        {
            parent.transform.GetChild(0).gameObject.GetComponent<Toggle>().isOn = true;
            parent.transform.GetChild(0).gameObject.GetComponent<Toggle>().interactable = false;
            parent.GetComponent<Image>().color = completedColor;
            parent.SetAsActive(false);

        }

        // If this is the last task in the procedure, show the
        // "procedure completed" panel.
        if (isLastTaskInParent && procedureCompletePanel != null)
        {
            procedureCompletePanel.SetActive(true);
        }

    }

    /*
     * Get the task's active status.
     */
    public bool IsActive()
    {
        return isActive;
    }

    /*
     * Get the task's completion status.
     */
    public bool GetCompletionStatus()
    {
        return hasBeenCompleted;
    }
}
