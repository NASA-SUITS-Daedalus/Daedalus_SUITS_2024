using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Microsoft.MixedReality.Toolkit.UI;

public class TSScUI : Interactable
{
    // TSSc Connection
    public TSScConnection TSSc;

    // UI Input
    public TMP_Text InputFieldUrl;
    public Button ConnectButton;

    // Reference to the game objects you want to activate
    public List<GameObject> objectsToActivate;

    // Reference to the game object you want to deactivate
    public GameObject objectToDeactivate;

    // Delay in seconds before deactivating the object
    public float deactivationDelay = 1.0f;

    protected override void OnEnable()
    {
        base.OnEnable();
        OnClick.AddListener(Connect_Button);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        OnClick.RemoveListener(Connect_Button);
    }

    // On Connect Button Press
    public void Connect_Button()
    {
        // Get URL in Text Field
        string host = InputFieldUrl.text;

        host = "127.0.0.1";

        // Print Hostname to Logs
        Debug.Log("Button Pressed: " + host);

        // Connect to TSSc at that Host
        TSSc.ConnectToHost(host, 6);

        // Start a coroutine to deactivate the object after a delay
        StartCoroutine(DeactivateObjectDelayed());

        // Activate the list of game objects
        ActivateObjects();
    }

    IEnumerator DeactivateObjectDelayed()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(deactivationDelay);

        // Deactivate the object
        if (objectToDeactivate != null)
        {
            objectToDeactivate.SetActive(false);
        }
    }

    void ActivateObjects()
    {
        // Loop through the list of game objects and activate each one
        foreach (GameObject obj in objectsToActivate)
        {
            obj.SetActive(true);
        }
    }

    public void Disconnect_Button()
    {
        // Disconnects from TSS when
        TSSc.DisconnectFromHost();
    }
}
