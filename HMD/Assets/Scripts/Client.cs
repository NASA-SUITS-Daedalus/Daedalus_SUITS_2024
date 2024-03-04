using UnityEngine;
using System;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;

// /*
// Container for data from web socket stream.
// */
// public class Message
// {
//     public float batt_time_left;
//     public float oxy_pri_storage;
//     public float oxy_sec_storage;
//     public float oxy_pri_pressure;
//     public float oxy_sec_pressure;
//     public float oxy_time_left;
//     public float heart_rate;
//     public float oxy_consumption;
//     public float co2_production;
//     public float suit_pressure_oxy;
//     public float suit_pressure_co2; // Note: Corrected typo in the JSON
//     public float suit_pressure_other;
//     public float suit_pressure_total;
//     public float fan_pri_rpm;
//     public float fan_sec_rpm;
//     public float helmet_pressure_co2;
//     public float scrubber_a_co2_storage;
//     public float scrubber_b_co2_storage;
//     public float temperature;
//     public float coolant_ml;
//     public float coolant_gas_pressure;
//     public float coolant_liquid_pressure;
// }


public class JsonClient : MonoBehaviour
{
    public string serverIP = "127.0.0.1";
    public int serverPort = 8052;
    private TcpClient tcpClient;
    private NetworkStream stream;

    void Start()
    {
        ConnectToServer();
    }

    void ConnectToServer()
    {
        try
        {
            tcpClient = new TcpClient(serverIP, serverPort);
            stream = tcpClient.GetStream();
            Debug.Log("Connected to server");

            // Optionally send a message to the server upon connection
            SendMessageToServer("Hello from Client!");

            // Start reading data from the server
            ReadDataFromServer();
        }
        catch (Exception e)
        {
            Debug.LogError("Error connecting to server: " + e.Message);
        }
    }

    void ReadDataFromServer()
    {
        byte[] data = new byte[4096];

        while (tcpClient.Connected)
        {
            if (stream.DataAvailable)
            {
                int bytesRead = stream.Read(data, 0, data.Length);
                string message = Encoding.UTF8.GetString(data, 0, bytesRead);
                Debug.Log("Received from server: " + message);

                // Deserialize and handle the message
                Message receivedMessage = JsonConvert.DeserializeObject<Message>(message);
                // Handle the receivedMessage here
            }
        }
    }

    void SendMessageToServer(string message)
    {
        if (stream.CanWrite)
        {
            byte[] clientMessageAsByteArray = Encoding.ASCII.GetBytes(message);
            stream.Write(clientMessageAsByteArray, 0, clientMessageAsByteArray.Length);
        }
    }

    void OnDestroy()
    {
        if (tcpClient != null)
        {
            if (tcpClient.Connected)
            {
                tcpClient.Close();
            }
        }
    }
}