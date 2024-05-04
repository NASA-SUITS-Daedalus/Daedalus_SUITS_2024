using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class LaunchMenu : MonoBehaviour
{
    public Color selectedColor = Color.green;
    public Color unselectedColor = Color.white;

    public TextMeshPro EV1Text;

    public TextMeshPro EV2Text;

    // TSSc Connection
    public TSScConnection TSSc;

    public EVANumberHandler EVAnum;

    // UI Input
    public TMP_Text InputFieldUrl;

    // Reference to the game objects you want to activate
    public GameObject[] objectsToActivate;

    // Reference to the game object you want to deactivate
    public GameObject objectToDeactivate;

    // Delay in seconds before deactivating the object
    public float deactivationDelay = 1.0f;

    // Maximum time to wait for the connection (in seconds)
    public float connectionTimeout = 5.0f;

    private void Start()
    {
    }
    public void EV1_Pressed()
    {
        EV1Text.text = $"<color=#{ColorUtility.ToHtmlStringRGBA(selectedColor)}>EV1";
        EV2Text.text = $"<color=#{ColorUtility.ToHtmlStringRGBA(unselectedColor)}>EV2";
    }
    public void EV2_Pressed()
    {
        EV2Text.text = $"<color=#{ColorUtility.ToHtmlStringRGBA(selectedColor)}>EV2";
        EV1Text.text = $"<color=#{ColorUtility.ToHtmlStringRGBA(unselectedColor)}>EV1";
    }
    // On Connect Button Press
    public void Connect_Button()
    {
        // Get URL in Text Field
        string host = InputFieldUrl.text;

        // Use this to HARDCODE the server address
        // If you are using remote server,
        // host = "168.4.185.194";

        // If you are using local server,
        host = "127.0.0.1";

        // Print Hostname to Logs
        Debug.Log("Button Pressed: " + host);

        // Connect to TSSc at that Host
        TSSc.ConnectToHost(host, 6);

        // Start a coroutine to wait for the connection and deactivate the object
        StartCoroutine(WaitForConnectionAndDeactivate());
    }

    IEnumerator WaitForConnectionAndDeactivate()
    {
        float elapsedTime = 0f;

        // Wait until the connection is established or the timeout is reached
        while (!TSSc.IsConnected() && elapsedTime < connectionTimeout)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Check if the connection is successful
        if (TSSc.IsConnected())
        {
            while (EVAnum.getEVANumber() == 0)
            {
                yield return null;
            }
            // Start a coroutine to deactivate the object after a delay
            StartCoroutine(DeactivateObjectDelayed());

            // Activate the array of game objects
            ActivateObjects();
            
        }
        else
        {
            Debug.Log("Connection failed. Object will not be deactivated.");
        }
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
        // Loop through the array of game objects and activate each one
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