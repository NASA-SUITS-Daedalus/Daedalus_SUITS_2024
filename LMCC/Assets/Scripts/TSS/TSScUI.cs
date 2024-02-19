using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class TSScUI : MonoBehaviour
{
    // TSSc Connection
    public TSScConnection TSSc;

    // UI Input
    public TMP_InputField InputFieldUrl;
    public Button ConnectButton;

    // Start is called before the first frame update
    void Start()
    {

    }

    // On Connect Button Press
    public void Connect_Button()
    {
        // Get URL in Text Field
        string host = InputFieldUrl.text;

        // Print Hostname to Logs
        Debug.Log("Button Pressed: " + host);

        // Connect to TSSc at that Host
        TSSc.ConnectToHost(host, 6);
    }

    public void Disconnect_Button()
    {
        // Disconnects from TSS when
        TSSc.DisconnectFromHost();
    }

}
