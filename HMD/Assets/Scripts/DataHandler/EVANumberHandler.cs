using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EVANumberHandler : MonoBehaviour
{
    public int EVANumber = 0;

    // Update is called once per frame
    public void EV1_Pressed()
    {
        EVANumber = 1;
    }
    public void EV2_Pressed()
    {
        EVANumber = 2;
    }
    public int getEVANumber()
    {
        return EVANumber;
    }
}
