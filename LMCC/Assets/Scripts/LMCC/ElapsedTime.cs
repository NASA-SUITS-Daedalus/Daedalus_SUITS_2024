using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*
 * Referencing this tutorial by Coco Code:
 * https://www.youtube.com/watch?v=HLz_k6DSQvU 
 * 
 * This script provides basic functionality to display the time elapsed
 * upon playing a project. 
 * 
 * This script will need updating for the functionality described in 
 * the Team Daedalus NASA SUITS proposal. 
 * 
 */

public class ElapsedTime : MonoBehaviour
{
    // The current time
    private float currentTime;

    // The text mesh that displays the elapsed time
    public TextMeshProUGUI timeText;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
        timeText.text = "Mission Elapsed Time: " + GetFormattedTime();
    }

    public float CurrentTime
    {
        get { return currentTime; }
    }

    // Update is called once per frame
    void Update()
    {
        // Update the current time 
        currentTime = currentTime + Time.deltaTime;

        // Change the text display
        timeText.text = "Mission Elapsed Time: " + GetFormattedTime();
    }

    public string GetFormattedTime()
    {
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        return time.ToString(@"hh\:mm\:ss");
    }
}
