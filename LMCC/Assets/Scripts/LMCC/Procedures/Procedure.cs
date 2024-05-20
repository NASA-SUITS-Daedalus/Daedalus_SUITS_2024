using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*
 * An abstract class to provide a default implementation for the major 
 * procedures (e.g., egress, ingress, cable repair).
 */
public abstract class Procedure : MonoBehaviour
{

    // The pop up that helps inform the user of the current task's status
    public StatusPopUp statusPopUp;

    // The procedure's end screen
    public GameObject endScreen;

    // The starting task for the procedure
    public Task startTask;

    // The telemetry data receiver
    public TELEMETRYDataHandler teleData;

    // The DCU data receiver
    public DCUDataHandler dcuData;

    // The UIA data receiver
    public UIADataHandler uiaData;

    // Whether or not the procedure has started yet
    public bool hasStarted;

    // A helper for the procedure interface
    public ProcedureUtility helper;

    // Whether or not the procedure has been completed
    protected bool hasBeenCompleted;

    // The procedure's current active task
    protected Task activeTask;

    // The active task's position in the flattened list of tasks
    protected int activeTaskIdx = 0;

    // The list of functions to check each task's status
    protected List<Action> taskChecks;

    // A flag representing if the current task is ready to advance. 
    protected bool advanceFlag = false;

    // A string to display more information on the current task.
    protected string info = "";

    // A string to display the current status of the task.
    protected string status = "";

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    protected void Update()
    {
        /*
         * Call the function associated with the current task. 
         */
        if (isOngoing() && taskChecks.Count > 0 && activeTaskIdx < taskChecks.Count)
        {

            // Execute the fuction that updates the status of the current task.
            taskChecks[activeTaskIdx]();

            // Set the extra information.
            statusPopUp.setInfoText(info);

            // Check if the task is ready to advance. 
            if (advanceFlag)
            {
                statusPopUp.setStatusText(status, true);
                readyActiveTask();
            }

            // Otherwise, just update the task status. 
            else
            {
                statusPopUp.setStatusText(status, false);
            }
        }
        
    }

    /*
     * Start the procedure.
     * This should happen after the user confirms they would like to begin
     * the relevant procedure (e.g., through pressing a button).
     */
    public void startProcedure()
    {

        // Make the starting task active
        startTask.makeActive();

        // Keep track of the current active task
        activeTask = startTask;

        // Note that the task has started
        hasStarted = true;
    }

    /*
     * Conclude the procedure after the user has gone through all of the steps.
     * This may include showing an end screen.
     * 
     * NOTE: This function is not used anymore. The final task in the list
     * now knows that it's the final task and will trigger the end screen 
     * on its own. 
     */
    public void concludeProcedure()
    {
        // Show an end screen, if one exists
        if (endScreen != null)
        {
            endScreen.SetActive(true);
        }

        // Close out the current active task
        activeTask = null;

        // Note that the task has been completed
        hasBeenCompleted = true;
    }

    /*
     * Set the active task.
     * This should be called by a TaskUI's OnClick toggle event (where it 
     * sets itself as the active task).
     */
    public void setActiveTask(Task task)
    {
        // Set the new task
        activeTask = task;

        // Increment the task index
        activeTaskIdx++;

        // Hide the proceed message
        statusPopUp.showProceedMessage(false);

    }

    /*
     * Check if the procedure is currently ongoing. 
     */
    public bool isOngoing()
    {
        return (hasStarted && !hasBeenCompleted);
    }

    /*
     * Set the active task as being ready to be checked off.
     */
    protected void readyActiveTask()
    {
        // Change the active task's appearance and make it interactable
        activeTask.setReady();

        // Show the proceed message
        statusPopUp.showProceedMessage();
    }

}
