using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairProcedure : Procedure
{

    // A string to prompt LMCC to wait for the EVs
    protected string prompt;

    // Start is called before the first frame update
    void Start()
    {
        // TODO delete, this is just for testing
        //startProcedure();

        taskChecks = new List<System.Action>
        {
            step1Task1,
            step1Task2,
            step2Task1,
            step2Task2,
            step3Task1,
            step3Task2,
            step3Task3,
            step3Task4,
            step4Task1,
            step4Task2,
            step4Task3,
            step4Task4
        };

        prompt = "Click the checkbox when this task is complete.";
    }


    /*
     * Repair procedure step 1.1
     * EV-1: On the COMM Tower screen, select the Gear Icon.
     * 
     */
    protected void step1Task1()
    {
        info = "EV-1: On the COMM Tower screen, select the Gear Icon.";

        status = prompt;

    }

    /*
     * Repair procedure step 1.2
     * EV-1: On the COMM Tower screen, select "Shutdown".
     * 
     */
    protected void step1Task2()
    {
        info = "EV-1: On the COMM Tower screen, select \"Shutdown\".";

        status = prompt;
    }

    /*
     * Repair procedure step 2.1
     * EV-2: Set the MMRTG switch to OFF.
     * 
     */
    protected void step2Task1()
    {
        info = "EV-2: Set the MMRTG switch to OFF.";

        status = prompt;
    }

    /*
     * Repair procedure step 2.2
     * EV-2: Navigate back to EV-1 at the COMM tower once the power is down.
     * 
     */
    protected void step2Task2()
    {
        info = "EV-2: Navigate back to EV-1 at the COMM tower once the power is down.";

        status = prompt;
    }

    /*
     * Repair procedure step 3.1
     * EV-1: Retrieve the spare cable from the tool box. 
     * 
     */
    protected void step3Task1()
    {
        info = "EV-1: Retrieve the spare cable from the tool box.";

        status = prompt;
    }

    /*
     * Repair procedure step 3.2
     * EV-2: Take one end of the spare cable from EV-1 and return to 
     * the MMRTG site.
     * 
     */
    protected void step3Task2()
    {
        info = "EV-2: Take one end of the spare cable from EV-1 and return to " +
            "the MMRTG site.";

        status = prompt;
    }

    /*
     * Repair procedure step 3.3
     * EV-1: Disconnect the damaged cable from the COMM Tower and connect
     * the spare cable in the same port.
     * 
     */
    protected void step3Task3()
    {
        info = "EV-1: Disconnect the damaged cable from the COMM Tower and connect" +
            "the spare cable in the same port.";

        status = prompt;
    }

    /*
     * Repair procedure step 3.4
     * EV-2: Disconnect the damaged cable from the MMRTG site and connect
     * the spare cable in the same port.
     * 
     */
    protected void step3Task4()
    {
        info = "EV-2: Disconnect the damaged cable from the MMRTG site and " +
            "connect the spare cable in the same port.";

        status = prompt;
    }

    /*
     * Repair procedure step 4.1
     * EV-1: Press the COMM Tower power button ON.
     * 
     */
    protected void step4Task1()
    {
        info = "EV-1: Press the COMM Tower power button ON.";

        status = prompt;
    }

    /*
     * Repair procedure step 4.2
     * EV-2: Navigate back to the MMRTG site.
     * 
     */
    protected void step4Task2()
    {
        info = "EV-2: Navigate back to the MMRTG site.";

        status = prompt;
    }

    /*
     * Repair procedure step 4.3
     * EV-2: Set the MMRTG power switch to ON.
     * 
     */
    protected void step4Task3()
    {
        info = "EV-2: Set the MMRTG power switch to ON.";

        status = prompt;
    }

    /*
     * Repair procedure step 4.4
     * EV-1: On the COMM Tower screen, verify that channel B (COM-B) 
     * is operational. 
     * 
     */
    protected void step4Task4()
    {
        info = "EV-1: On the COMM Tower screen, verify that channel B " +
            "(COM-B) is operational. ";

        status = prompt;
    }


}
