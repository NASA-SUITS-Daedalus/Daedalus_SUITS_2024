using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateNavigation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Assuming you have a button component attached to this GameObject
        Button button = GetComponent<Button>();

        // Add a listener to the button click event
        button.onClick.AddListener(PrintYourMama);
    }

    // Function to be called when the button is clicked
    void PrintYourMama()
    {
        Debug.Log("your mama");
    }
}
