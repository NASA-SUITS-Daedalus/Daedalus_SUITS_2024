using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EgressProcedure : Procedure
{

    private bool testFlag = false;

    // Start is called before the first frame update
    void Start()
    {

        // TODO delete this from Start function, this is just for testing
        startProcedure();

        Invoke("testCheck", 5);

        // Make the list of task checkers
        taskChecks = new List<System.Action>
        {
            step1Task1,
            step1Task2
        };
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}

    /*
     * Egress procedure step 1.1
     */
    protected void step1Task1()
    {
        statusPopUp.setInfoText("Plug the UIA to the DCU using the umbilical cable.");

        if (testFlag)
        {
            statusPopUp.setStatusText("UIA-DCU STATUS: CONNECTED", true);
            readyActiveTask();
        }
        else
        {
            statusPopUp.setStatusText("UIA-DCU STATUS: NOT CONNECTED", false);
        }

    }

    private void testCheck()
    {
        testFlag = true;
    }

    /*
     * Egress procedure step 1.1
     */
    protected void step1Task2()
    {
        statusPopUp.setInfoText("Switch the UIA EMU POWER to ON.\n" +
            "This activates the Umbilical on the UIA side.");

        statusPopUp.setStatusText("UIA EMU POWER STATUS: OFF", false);
    }

}
