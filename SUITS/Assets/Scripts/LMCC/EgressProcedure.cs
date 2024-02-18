using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EgressProcedure : MonoBehaviour
{

    // The GameObject containing the 9 major steps
    public GameObject step0;
    public GameObject step1;
    public GameObject step2;
    public GameObject step3;
    public GameObject step4;
    public GameObject step5;
    public GameObject step6;
    public GameObject step7;
    public GameObject step8;

    /*
     * Note that the tasks associated with each step should be
     * Toggles. For the purposes of setup in the Start() function,
     * they will (more generically) be declared as GameObjects. 
     * 
     * We could've also made these public GameObjects and set them
     * in the Inspector, but to save space we will just get them
     * in the Start() function. 
     */

    // The tasks for step 0
    GameObject step0_1;

    // The tasks for step 1
    GameObject step1_1;
    GameObject step1_2;

    // The tasks for step 2
    GameObject step2_1;
    GameObject step2_2;
    GameObject step2_3;

    // The tasks for step 3
    GameObject step3_1;
    GameObject step3_2;
    GameObject step3_3;
    GameObject step3_4;
    GameObject step3_5;
    GameObject step3_6;

    // The tasks for step 4
    GameObject step4_1;
    GameObject step4_2;
    GameObject step4_3;

    // The tasks for step 5
    GameObject step5_1;
    GameObject step5_1_1;
    GameObject step5_1_2;
    GameObject step5_1_3;
    GameObject step5_2;
    GameObject step5_2_1;
    GameObject step5_2_2;
    GameObject step5_2_3;

    // The tasks for step 6
    GameObject step6_1;
    GameObject step6_2;

    // The tasks for step 7
    GameObject step7_1;
    GameObject step7_2;
    GameObject step7_3;

    // The tasks for step 8
    GameObject step8_1;
    GameObject step8_2;
    GameObject step8_3;

    // The active step (as a whole)
    GameObject activeStep;

    // The active task (a Toggle object)
    /*
     * By this, we mean the active subtask that represents
     * an actual operator that the user can complete
     */
    GameObject activeTask;

    // The color that a completed step should be
    Color completedColor = Color.gray;

    // The color that a highlighted task should be
    Color highlightColor = Color.yellow;

    // The necessary data from the telemetry stream


    // Start is called before the first frame update
    void Start()
    {
        // Set up the egress procedure

        // Set the tasks for each step

        // Step 0
        step0_1 = step0.transform.GetChild(0).gameObject;

        // Step 1
        step1_1 = step1.transform.GetChild(1).gameObject;
        step1_2 = step1.transform.GetChild(2).gameObject;

        // Step 2
        step2_1 = step2.transform.GetChild(1).gameObject;
        step2_2 = step2.transform.GetChild(2).gameObject;
        step2_3 = step2.transform.GetChild(3).gameObject;

        // Step 3
        step3_1 = step3.transform.GetChild(1).gameObject;
        step3_2 = step3.transform.GetChild(2).gameObject;
        step3_3 = step3.transform.GetChild(3).gameObject;
        step3_4 = step3.transform.GetChild(4).gameObject;
        step3_5 = step3.transform.GetChild(5).gameObject;
        step3_6 = step3.transform.GetChild(6).gameObject;

        // Step 4
        step4_1 = step4.transform.GetChild(1).gameObject;
        step4_2 = step4.transform.GetChild(2).gameObject;
        step4_3 = step4.transform.GetChild(3).gameObject;

        // Step 5
        step5_1 = step5.transform.GetChild(1).gameObject;
        step5_1_1 = step5.transform.GetChild(2).gameObject;
        step5_1_2 = step5.transform.GetChild(3).gameObject;
        step5_1_3 = step5.transform.GetChild(4).gameObject;
        step5_2 = step5.transform.GetChild(5).gameObject;
        step5_2_1 = step5.transform.GetChild(6).gameObject;
        step5_2_2 = step5.transform.GetChild(7).gameObject;
        step5_2_3 = step5.transform.GetChild(8).gameObject;

        // Step 6
        step6_1 = step6.transform.GetChild(1).gameObject;
        step6_2 = step6.transform.GetChild(2).gameObject;

        // Step 7
        step7_1 = step7.transform.GetChild(1).gameObject;
        step7_2 = step7.transform.GetChild(2).gameObject;
        step7_3 = step7.transform.GetChild(3).gameObject;

        // Step 8
        step8_1 = step8.transform.GetChild(1).gameObject;
        step8_2 = step8.transform.GetChild(2).gameObject;
        step8_3 = step8.transform.GetChild(3).gameObject;


        /*
         * For the purposes of testing, we're calling the 
         * main egress procedure method in the start function.
         * In the future, we might want to instead attach that
         * main method to a button that starts the procedure itself.
         */
        PerformEgressProcedure();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /* 
     * Main function to carry out the egress procedure
     */
    void PerformEgressProcedure()
    {

        // Make sure step 0 is the only one active
        activeStep = step0;
        activeTask = step0_1;

        // Update the active task's highlighting
        activeTask.GetComponent<Image>().color = highlightColor;

    }


    /*
     * These individual step completion methods will be attached to 
     * their respective steps' toggle events. 
     */

    // Start step 1

    /*
     * After LMCC clicks the step 0 checkbox,
     * update the active step to step 1
     * and update the active task to step 1.1.
     */
    public void completedStep0()
    {

        /*
         * We could do something interesting in these methods
         * where we prevent LMCC from marking a task
         * as complete if the UIA data isn't correct
         * for the particular step.
         * 
         * For example, here we might prevent the user from
         * checking off step 1.1 if the EMU-1 switch isn't on.
         * That could be a separately defined helper method. 
         */

        updateActiveTask(step1_1);
        updateActiveStep(step1);
    }

    /*
     * After LMCC clicks the step 1.1 checkbox,
     * update the active task to step 1.1.
     */
    public void completedStep1_1()
    {
        updateActiveTask(step1_2);
    }

    /*
     * After LMCC clicks the step 1.2 checkbox,
     * update the active task to step 2.1.
     * and update the active step to step 2.
     * (Make sure updating the active task comes first!!)
     */
    public void completedStep1_2()
    {
        updateActiveTask(step2_1);
        updateActiveStep(step2);
    }

    public void completedStep2_1()
    {
        updateActiveTask(step2_2);
    }

    public void completedStep2_2()
    {
        updateActiveTask(step2_3);
    }


    // Start step 3
    public void completedStep2_3()
    {
        updateActiveTask(step3_1);
        updateActiveStep(step3);
    }

    public void completedStep3_1()
    {
        updateActiveTask(step3_2);
    }

    public void completedStep3_2()
    {
        updateActiveTask(step3_3);
    }

    public void completedStep3_3()
    {
        updateActiveTask(step3_4);
    }

    public void completedStep3_4()
    {
        updateActiveTask(step3_5);
    }

    public void completedStep3_5()
    {
        updateActiveTask(step3_6);
    }

    // Start step 4
    public void completedStep3_6()
    {
        updateActiveTask(step4_1);
        updateActiveStep(step4);
    }

    public void completedStep4_1()
    {
        updateActiveTask(step4_2);
    }

    public void completedStep4_2()
    {
        updateActiveTask(step4_3);
    }

    // Start step 5
    public void completedStep4_3()
    {
        updateActiveTask(step5_1_1);
        updateActiveStep(step5);
    }

    public void completedStep5_1_1()
    {
        updateActiveTask(step5_1_2);
    }

    public void completedStep5_1_2()
    {
        updateActiveTask(step5_1_3);
    }

    public void completedStep5_1_3()
    {
        step5_1.GetComponent<Toggle>().isOn = true;
        updateActiveTask(step5_2_1);
    }

    public void completedStep5_2_1()
    {
        updateActiveTask(step5_2_2);
    }

    public void completedStep5_2_2()
    {
        updateActiveTask(step5_2_3);
    }

    // Start step 6
    public void completedStep5_2_3()
    {
        step5_2.GetComponent<Toggle>().isOn = true;
        updateActiveTask(step6_1);
        updateActiveStep(step6);
    }

    public void completedStep6_1()
    {
        updateActiveTask(step6_2);
    }

    // Start step 7
    public void completedStep6_2()
    {
        updateActiveTask(step7_1);
        updateActiveStep(step7);
    }

    public void completedStep7_1()
    {
        updateActiveTask(step7_2);
    }

    public void completedStep7_2()
    {
        updateActiveTask(step7_3);
    }

    // Start step 8
    public void completedStep7_3()
    {
        updateActiveTask(step8_1);
        updateActiveStep(step8);
    }

    public void completedStep8_1()
    {
        updateActiveTask(step8_2);
    }

    public void completedStep8_2()
    {
        updateActiveTask(step8_3);
    }

    // Finish egress procedure
    public void completedStep8_3()
    {
        finishEgressProcedure();
    }

    /*
     * Update the telemetry stream data
     */
    void updateData()
    {
        // TODO finish this once the telemetry stream client is finished
    }

    /*
     * Switch the step that is currently marked as active
     */
    void updateActiveStep(GameObject newStep)
    {
        // Mark the current active step's name as completed
        activeStep.transform.GetChild(0).gameObject.GetComponent<Toggle>().isOn = true;
        activeStep.transform.GetChild(0).gameObject.GetComponent<Toggle>().interactable = false;

        // Gray out the current active step's panel
        activeStep.GetComponent<Image>().color = completedColor;

        // Update the active step
        activeStep = newStep;

    }

    /*
     * Switch the task that is currently marked as active
     */
    void updateActiveTask(GameObject newTask)
    {

        // Disable the current active task's toggle
        activeTask.GetComponent<Toggle>().interactable = false;

        // Set the current active task's background as inactive
        activeTask.GetComponent<Image>().enabled = false;

        // Update the active task
        activeTask = newTask;
        //Debug.Log("Active task is " + activeTask.name);

        // Enable the new task
        activeTask.GetComponent<Toggle>().interactable = true;

        // Set the current active's task's background as active
        activeTask.GetComponent<Image>().color = highlightColor;
        activeTask.GetComponent<Image>().enabled = true;

    }

    /*
     * Finish up the egress procedure
     */
    void finishEgressProcedure()
    {
        // Mark the current active step's name as completed
        activeStep.transform.GetChild(0).gameObject.GetComponent<Toggle>().isOn = true;
        activeStep.transform.GetChild(0).gameObject.GetComponent<Toggle>().interactable = false;

        // Gray out the current active step's panel
        activeStep.GetComponent<Image>().color = completedColor;

        // Disable the current active task's toggle
        activeTask.GetComponent<Toggle>().interactable = false;

        // Set the current active task's background as inactive
        activeTask.GetComponent<Image>().enabled = false;

        // TODO show a message that tells LMCC the egress procedure is done?

    }

    /*
     * Restart the egress procedure if necessary
     */
}
