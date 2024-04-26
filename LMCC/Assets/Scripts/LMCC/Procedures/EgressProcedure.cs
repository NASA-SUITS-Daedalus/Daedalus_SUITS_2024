using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class controls the egress procedure checklist found on the egress 
 * procedure active screen. 
 * 
 * As EV-1 and EV-2 go through the egress procedure, the active task in progress
 * is shown in yellow. When the EVs are ready to proceed to the next step, 
 * the checklist item will change from yellow to green. LMCC can check on the
 * status of the current step by hovering over the checklist item; they will
 * be told when the system is ready to proceed and will be informed to click
 * the checkbox to advance to the next step. 
 * 
 */
public class EgressProcedure : Procedure
{

    // Start is called before the first frame update
    void Start()
    {

        // TODO delete this from Start function, this is just for testing
        // (Attach this to a button at some point)
        startProcedure();

        // Make the list of task checkers
        taskChecks = new List<System.Action>
        {
            step1Task1,
            step1Task2,
            step1Task3,
            step2Task1,
            step2Task2,
            step2Task3,
            step3Task1,
            step3Task2,
            step3Task3,
            step3Task4,
            step4Task1,
            step4Task2,
            step4Task3,
            step4Task4,
            step4Task5,
            step5Task1,
            step5Task2,
            step5Task3,
            step5Task4,
            step6Task1,
            step6Task2,
            step6Task3,
            step6Task4,
            step7Task1,
            step7Task2,
            step7Task3,
            step7Task4,
            step8Task1
        };

        //// Initialize the UI helper
        //helper = new ProcedureUtility();
    }

    //// Update
    //void Update()
    //{
    //    // Call the parent update function
    //    base.Update();

    //}

    /*
     * Egress procedure step 1.1
     * EV-1 and EV-2: Connect UIA to DCU using the umbilical cable. 
     * Each EV needs to connect to the UIA via the umbilical cable. 
     */
    protected void step1Task1()
    {
        info = "Each EV needs to connect to the UIA via the umbilical cable.\n" +
            "Once connected, use the UIA's EV-1 and EV-2 power switches to power " +
            "each suit.";

        // TODO fix so that this gets the power status of each suit from UIA
        bool eva1Status = false;
        bool eva2Status = false;

        advanceFlag = (eva1Status && eva2Status);
        status = string.Format("EV-1 PWR status: {0}\nEV-2 PWR status: {1}",
                                helper.formatSwitch(eva1Status),
                                helper.formatSwitch(eva2Status));
    }

    /*
     * Egress procedure step 1.2
     * Both DCU: Switch BATT to UMB.
     * For each DCU, switch the battery mode to umbilical. 
     */
    protected void step1Task2()
    {
        info = "For each DCU, switch the battery mode to umbilical.";

        // TODO fix so that this gets the current battery status of each DCU (local or umbilical)
        bool eva1Status = false;
        bool eva2Status = false;

        advanceFlag = (eva1Status && eva2Status);
        status = string.Format("EV-1 DCU-BATT status: {0}\nEV-2 DCU-BATT status: {1}",
                                helper.formatDCUBatt(eva1Status),
                                helper.formatDCUBatt(eva2Status));
    }

    /*
     * Egress procedure step 1.3
     * Switch the DEPRESS PUMP PWR to ON. 
     * Turn on the depress pump power to begin the depress of each suit. 
     */
    protected void step1Task3()
    {

        info = "Turn on the depress pump power to begin the depress of each suit.";

        // TODO fix so that this gets the UIA's depress pump status
        bool depressStatus = false;

        advanceFlag = depressStatus;
        status = string.Format("UIA depress pump power status: {0}",
            helper.formatSwitch(depressStatus));

    }

    /*
     * Egress procedure step 2.1
     * UIA: Switch the OXYGEN/O2 VENT to OPEN.
     * Open the oxygen vent on the UIA. 
     */
    protected void step2Task1()
    {
        info = "Open the oxygen vent on the UIA.";

        // TODO fix so that this gets the UIA's oxy vent status
        bool oxyVentStatus = false;

        advanceFlag = oxyVentStatus == true;
        status = string.Format("UIA O2 vent status: {0}",
            helper.formatSwitch(oxyVentStatus));
    }

    /*
     * Egress procedure step 2.2
     * Wait until both primary and secondary O2 tanks are < 10 psi…
     * Wait until the primary and secondary oxygen tank pressures for both EVs 
     * are under 10 psi.
     */
    protected void step2Task2()
    {
        info = "Wait until the primary and secondary oxygen tank pressures for both EVs are under 10 psi.";

        // TODO fix to get actual readings
        float ev1Primary = 0f;
        float ev1Secondary = 0f;
        float ev2Primary = 0f;
        float ev2Secondary = 0f;

        advanceFlag = (ev1Primary < 10f &&
                        ev1Secondary < 10f &&
                        ev2Primary < 10f &&
                        ev2Secondary < 10f);

        status = string.Format("EV-1 primary O2 tank pressure: {0}\n" +
                                "EV-1 secondary O2 tank pressure: {1}\n" +
                                "EV-2 primary O2 tank pressure: {2}\n" +
                                "EV-2 secondary O2 tank pressure: {3}\n",
                                helper.formatPressure(ev1Primary),
                                helper.formatPressure(ev1Secondary),
                                helper.formatPressure(ev2Primary),
                                helper.formatPressure(ev2Secondary)
                                );
    }

    /*
     * Egress procedure step 2.3
     * UIA: Switch the OXYGEN O2 VENT to CLOSED.
     * Close the oxygen vent on the UIA. 
     */
    protected void step2Task3()
    {
        info = "UIA: Switch the OXYGEN O2 VENT to CLOSED.";

        // TODO fix to get actual readings
        bool oxyVentStatus = false;

        advanceFlag = oxyVentStatus == false;
        status = string.Format("UIA O2 vent status: {0}",
            helper.formatSwitch(oxyVentStatus));

    }

    /*
     * Egress procedure step 3.1
     * Both DCU: Switch OXY to PRI.
     * For each DCU, set the primary oxygen tank as the active tank.
     */
    protected void step3Task1()
    {
        info = "For each DCU, set the primary oxygen tank as the active tank.";

        // TODO fix to get actual readings
        bool ev1Oxy = false;
        bool ev2Oxy = false;

        advanceFlag = (ev1Oxy && ev2Oxy);
        status = string.Format("EV-1 DCU OXY active tank: {0}\n" +
                                "EV-2 DCU OXY active tank: {1}\n",
                                helper.formatDCUOxy(ev1Oxy),
                                helper.formatDCUOxy(ev2Oxy)
                                );
    }

    /*
     * Egress procedure step 3.2
     * UIA: Switch OXYGEN EMU-1, EMU-2 to OPEN.
     * Start to fill the primary oxygen tank. 
     */
    protected void step3Task2()
    {
        info = "Start to fill the primary oxygen tank for each EV.";

        // TODO fix to get actual readings
        bool eva1Oxy = false;
        bool eva2Oxy = false;

        advanceFlag = (eva1Oxy && eva2Oxy);
        status = string.Format("UIA EMU-1 oxygen valve status: {0}\n" +
                                "UIA EMU-1 oxygen valve status: {1}",
                                helper.formatValve(eva1Oxy),
                                helper.formatValve(eva2Oxy)
                                );

    }

    /*
     * Egress procedure step 3.3
     * Wait until the primary O2 tank is > 3000 psi…
     * Wait until the primary oxygen tank pressure for both EVs is over 3000 psi.
     */
    protected void step3Task3()
    {
        info = "Wait until the primary oxygen tank pressure for both EVs is over 3000 psi.";

        // TODO fix to get actual readings
        float ev1Primary = 0f;
        float ev2Primary = 0f;

        advanceFlag = (ev1Primary > 3000f &&
                        ev2Primary > 3000f);

        status = string.Format("EV-1 primary O2 tank pressure: {0}\n" +
                                "EV-2 primary O2 tank pressure: {1}\n",
                                helper.formatPressure(ev1Primary),
                                helper.formatPressure(ev2Primary)
                                );

    }

    /*
     * Egress procedure step 3.4
     * UIA: Switch OXYGEN EMU-1, EMU-2 to CLOSED.
     * Finish filling the primary oxygen tank. 
     */
    protected void step3Task4()
    {
        info = "Finish filling the primary oxygen tank for each EV.";

        // TODO fix to get actual readings
        bool eva1Oxy = false;
        bool eva2Oxy = false;

        advanceFlag = !(eva1Oxy || eva2Oxy);
        status = string.Format("UIA EMU-1 oxygen valve status: {0}\n" +
                                "UIA EMU-1 oxygen valve status: {1}",
                                helper.formatValve(eva1Oxy),
                                helper.formatValve(eva2Oxy)
                                );

    }

    /*
     * Egress procedure step 4.1
     * Both DCU: Switch OXY to SEC.
     * For each DCU, set the secondary oxygen tank as the active tank.
     */
    protected void step4Task1()
    {
        info = "For each DCU, set the secondary oxygen tank as the active tank.";

        // TODO fix to get actual readings
        bool ev1Oxy = false;
        bool ev2Oxy = false;

        advanceFlag = !(ev1Oxy && ev2Oxy);
        status = string.Format("EV-1 DCU OXY active tank: {0}\n" +
                                "EV-2 DCU OXY active tank: {1}\n",
                                helper.formatDCUOxy(ev1Oxy),
                                helper.formatDCUOxy(ev2Oxy)
                                );
    }

    /*
     * Egress procedure step 4.2
     * UIA: Switch OXYGEN EMU-1, EMU-2 to OPEN.
     * Start to fill the secondary oxygen tank. 
     */
    protected void step4Task2()
    {
        info = "Start to fill the secondary oxygen tank for each EV.";

        // TODO fix to get actual readings
        bool eva1Oxy = false;
        bool eva2Oxy = false;

        advanceFlag = (eva1Oxy && eva2Oxy);
        status = string.Format("UIA EMU-1 oxygen valve status: {0}\n" +
                                "UIA EMU-1 oxygen valve status: {1}",
                                helper.formatValve(eva1Oxy),
                                helper.formatValve(eva2Oxy)
                                );
    }

    /*
     * Egress procedure step 4.3
     * Wait until the secondary O2 tank is > 3000 psi…
     * Wait until the secondary oxygen tank pressure for both EVs is over 3000 psi.
     */
    protected void step4Task3()
    {
        info = "Wait until the secondary oxygen tank pressure for both EVs is over 3000 psi.";

        // TODO fix to get actual readings
        float ev1Secondary = 0f;
        float ev2Secondary = 0f;

        advanceFlag = (ev1Secondary > 3000f &&
                        ev2Secondary > 3000f);

        status = string.Format("EV-1 secondary O2 tank pressure: {0}\n" +
                                "EV-2 secondary O2 tank pressure: {1}\n",
                                helper.formatPressure(ev1Secondary),
                                helper.formatPressure(ev2Secondary)
                                );

    }

    /*
     * Egress procedure step 4.4
     * UIA: Switch OXYGEN EMU-1, EMU-2 to CLOSED.
     * Finish filling the secondary oxygen tank. 
     */
    protected void step4Task4()
    {
        info = "Finish filling the secondary oxygen tank for each EV.";

        // TODO fix to get actual readings
        bool eva1Oxy = false;
        bool eva2Oxy = false;

        advanceFlag = !(eva1Oxy || eva2Oxy);
        status = string.Format("UIA EMU-1 oxygen valve status: {0}\n" +
                                "UIA EMU-1 oxygen valve status: {1}",
                                helper.formatValve(eva1Oxy),
                                helper.formatValve(eva2Oxy)
                                );


    }

    /*
     * Egress procedure step 4.5
     * Both DCU: Switch OXY to PRI.
     * For each DCU, set the primary oxygen tank as the active tank.
     */
    protected void step4Task5()
    {
        info = "For each DCU, set the primary oxygen tank as the active tank.";

        // TODO fix to get actual readings
        bool ev1Oxy = false;
        bool ev2Oxy = false;

        advanceFlag = (ev1Oxy && ev2Oxy);
        status = string.Format("EV-1 DCU OXY active tank: {0}\n" +
                                "EV-2 DCU OXY active tank: {1}\n",
                                helper.formatDCUOxy(ev1Oxy),
                                helper.formatDCUOxy(ev2Oxy)
                                );

    }

    /*
     * Egress procedure step 5.1
     * Both DCU: Switch PUMP to OPEN.
     * For each DCU, open the water pump to allow coolant to flow between 
     * the EVs’ suits and the UIA. 
     */
    protected void step5Task1()
    {
        info = "For each DCU, open the water pump to allow coolant to flow " +
            "between the EVs’ suits and the UIA.";

        // TODO fix to get actual readings
        bool eva1Pump = false;
        bool eva2Pump = false;

        advanceFlag = (eva1Pump && eva2Pump);
        status = string.Format("UIA EMU-1 water pump status: {0}\n" +
                                "UIA EMU-1 water pump status: {1}",
                                helper.formatValve(eva1Pump),
                                helper.formatValve(eva2Pump)
                                );
    }

    /*
     * Egress procedure step 5.2
     * UIA: Switch EV-1, EV-2 WASTE WATER to OPEN. 
     * Turn on each EV’s waste water valve using the UIA to begin flushing 
     * the waste water. 
     */
    protected void step5Task2()
    {
        info = "Turn on each EV’s waste water valve using the UIA to begin " +
            "flushing the waste water. ";

        // TODO fix to get actual readings
        bool eva1WasteWater = false;
        bool eva2WasteWater = false;

        advanceFlag = (eva1WasteWater && eva2WasteWater);
        status = string.Format("UIA EMU-1 waste water valve status: {0}\n" +
                                "UIA EMU-1 waste water valve status: {1}",
                                helper.formatValve(eva1WasteWater),
                                helper.formatValve(eva2WasteWater)
                                );
    }

    /*
     * Egress procedure step 5.3
     * Wait until the water coolant tank level is < 5%...
     * Wait until the water coolant level for each EV is almost empty. 
     */
    protected void step5Task3()
    {
        info = "Wait until the water coolant level for each EV is almost empty.";

        // TODO fix to get actual readings
        float ev1Coolant = 0f;
        float ev2Coolant = 0f;

        advanceFlag = (ev1Coolant < 0.05f &&
                        ev2Coolant < 0.05f);

        status = string.Format("EV-1 water coolant level: {0}\n" +
                                "EV-2 water coolant level: {1}\n",
                                helper.formatPercentage(ev1Coolant),
                                helper.formatPercentage(ev2Coolant)
                                );
    }

    /*
     * Egress procedure step 5.4
     * Switch EV-1, EV-2 WASTE WATER to CLOSED. 
     * Close the waste water valves using the UIA. 
     */
    protected void step5Task4()
    {
        info = "Close the waste water valves using the UIA.";

        // TODO fix to get actual readings
        bool eva1WasteWater = false;
        bool eva2WasteWater = false;

        advanceFlag = !(eva1WasteWater || eva2WasteWater);
        status = string.Format("UIA EMU-1 waste water valve status: {0}\n" +
                                "UIA EMU-1 waste water valve status: {1}",
                                helper.formatValve(eva1WasteWater),
                                helper.formatValve(eva2WasteWater)
                                );

    }

    /*
     * Egress procedure step 6.1
     * UIA: Switch EV-1, EV-2 SUPPLY WATER to OPEN.
     * Turn on each EV’s supply water valve using the UIA to begin 
     * refilling the coolant tank. 
     */
    protected void step6Task1()
    {
        info = "Turn on each EV’s supply water valve using the UIA to begin " +
            "refilling the coolant tank. ";

        // TODO fix to get actual readings
        bool eva1SupplyWater = false;
        bool eva2SupplyWater = false;

        advanceFlag = (eva1SupplyWater && eva2SupplyWater);
        status = string.Format("UIA EMU-1 supply water valve status: {0}\n" +
                                "UIA EMU-1 supply water valve status: {1}",
                                helper.formatValve(eva1SupplyWater),
                                helper.formatValve(eva2SupplyWater)
                                );

    }

    /*
     * Egress procedure step 6.2
     * Wait until the water coolant tank level is > 95%...
     * Wait until the water coolant level for each EV is almost full. 
     */
    protected void step6Task2()
    {
        info = "Wait until the water coolant level for each EV is almost full.";

        // TODO fix to get actual readings
        float ev1Coolant = 0f;
        float ev2Coolant = 0f;

        advanceFlag = (ev1Coolant > 0.95f &&
                        ev2Coolant > 0.95f);

        status = string.Format("EV-1 water coolant level: {0}\n" +
                                "EV-2 water coolant level: {1}\n",
                                helper.formatPercentage(ev1Coolant),
                                helper.formatPercentage(ev2Coolant)
                                );

    }

    /*
     * Egress procedure step 6.3
     * UIA: Switch EV-1, EV-2 SUPPLY WATER to CLOSED. 
     * Close the supply water valves using the UIA. 
     */
    protected void step6Task3()
    {
        info = "Close the supply water valves using the UIA.";

        // TODO fix to get actual readings
        bool eva1SupplyWater = false;
        bool eva2SupplyWater = false;

        advanceFlag = !(eva1SupplyWater || eva2SupplyWater);
        status = string.Format("UIA EMU-1 supply water valve status: {0}\n" +
                                "UIA EMU-1 supply water valve status: {1}",
                                helper.formatValve(eva1SupplyWater),
                                helper.formatValve(eva2SupplyWater)
                                );

    }

    /*
     * Egress procedure step 6.4
     * Both DCU: Switch PUMP to CLOSED.
     * For each DCU, stop coolant from flowing between the EVs’ suits 
     * and the UIA.
     */
    protected void step6Task4()
    {
        info = "For each DCU, stop coolant from flowing between " +
            "the EVs’ suits and the UIA.";

        // TODO fix to get actual readings
        bool eva1Pump = false;
        bool eva2Pump = false;

        advanceFlag = !(eva1Pump || eva2Pump);
        status = string.Format("UIA EMU-1 water pump status: {0}\n" +
                                "UIA EMU-1 water pump status: {1}",
                                helper.formatValve(eva1Pump),
                                helper.formatValve(eva2Pump)
                                );
    }

    /*
     * Egress procedure step 7.1
     * Wait until SUIT P is at 4 psi…
     * Wait until the suit pressure for each EV is at 4 psi. 
     */
    protected void step7Task1()
    {
        info = "Wait until the suit pressure for each EV is at 4 psi.";

        // TODO fix to get actual readings
        float ev1SuitPressure = 0f;
        float ev2SuitPressure = 0f;

        advanceFlag = (ev1SuitPressure < 5f &&
                        ev1SuitPressure > 3f &&
                        ev2SuitPressure < 5f &&
                        ev2SuitPressure > 3f);
        status = string.Format("EV-1 suit pressure: {0}\n" +
                                "EV-2 suit pressure: {1}",
                                helper.formatPressure(ev1SuitPressure),
                                helper.formatPressure(ev2SuitPressure)
                                );

    }

    /*
     * Egress procedure step 7.2
     * UIA: Switch the DEPRESS PUMP PWR to OFF. 
     * Turn off the depress pump power to conclude the depress of each suit. 
     */
    protected void step7Task2()
    {
        info = "Turn off the depress pump power to conclude the depress of each suit.";

        // TODO fix to get actual readings
        bool depressStatus = false;

        advanceFlag = !depressStatus;
        status = string.Format("UIA depress pump power status: {0}",
            helper.formatSwitch(depressStatus));

    }

    /*
     * Egress procedure step 7.3
     * Both DCU: Switch BATT to LOCAL.
     * For each DCU, switch the battery mode to local. 
     */
    protected void step7Task3()
    {
        info = "For each DCU, switch the battery mode to local.";

        // TODO fix to get actual readings
        bool eva1Status = false;
        bool eva2Status = false;

        advanceFlag = !(eva1Status || eva2Status);
        status = string.Format("EV-1 DCU-BATT status: {0}\nEV-2 DCU-BATT status: {1}",
                                helper.formatDCUBatt(eva1Status),
                                helper.formatDCUBatt(eva2Status));
    }

    /*
     * Egress procedure step 7.4
     * UIA: Switch EV-1, EV-2 PWR to OFF. 
     * Conclude powering each EV’s suit using the UIA. 
     */
    protected void step7Task4()
    {
        info = "Conclude powering each EV’s suit using the UIA.";

        // TODO fix to get actual readings
        bool eva1Status = false;
        bool eva2Status = false;

        advanceFlag = !(eva1Status || eva2Status);
        status = string.Format("EV-1 PWR status: {0}\nEV-2 PWR status: {1}",
                                helper.formatSwitch(eva1Status),
                                helper.formatSwitch(eva2Status));

    }

    /*
     * Egress procedure step 8.1
     * Double-check the DCU switch states for each EV.
     * Verify that the DCU switch states for each EV are at the following settings:
     *      BATT	LOCAL
     *      OXY		PRI
     *      COMMS	A
     *      FAN		PRI
     *      PUMP	CLOSED
     *      CO2		A
     */
    protected void step8Task1()
    {
        info = "Verify that both EVs are ready to conclude the egress procedure.";

        // TODO fix to get actual readings


    }

}
