using UnityEngine;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using Newtonsoft.Json;

/*
Container for data from web socket stream.
*/
public class Message
{
    public float batt_time_left;
    public float oxy_pri_storage;
    public float oxy_sec_storage;
    public float oxy_pri_pressure;
    public float oxy_sec_pressure;
    public float oxy_time_left;
    public float heart_rate;
    public float oxy_consumption;
    public float co2_production;
    public float suit_pressure_oxy;
    public float suit_pressure_co2; // Note: Corrected typo in the JSON
    public float suit_pressure_other;
    public float suit_pressure_total;
    public float fan_pri_rpm;
    public float fan_sec_rpm;
    public float helmet_pressure_co2;
    public float scrubber_a_co2_storage;
    public float scrubber_b_co2_storage;
    public float temperature;
    public float coolant_ml;
    public float coolant_gas_pressure;
    public float coolant_liquid_pressure;
}

public class JsonServer : MonoBehaviour {
    private TcpListener tcpListener;
    private Thread tcpListenerThread;
    private List<TcpClient> connectedClients = new List<TcpClient>();

    void Start() {
        tcpListenerThread = new Thread(new ThreadStart(ListenForIncomingRequests));
        tcpListenerThread.IsBackground = true;
        tcpListenerThread.Start();
    }

    private void ListenForIncomingRequests() {
        try {
            tcpListener = new TcpListener(IPAddress.Any, 8052);
            tcpListener.Start();
            Debug.Log("Server is listening");

            while (true) {
                TcpClient client = tcpListener.AcceptTcpClient();
                connectedClients.Add(client);

                // Send "hi" to client right after establishing the connection
                SendMessage(client, "Starting the connection!");

                // Start a new thread to handle the client's incoming messages
                Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClientComm));
                clientThread.Start(client);
            }
        } catch (SocketException socketException) {
            Debug.Log("SocketException " + socketException.ToString());
        }
    }

    private void HandleClientComm(object clientObj) {
        TcpClient client = (TcpClient)clientObj;
        NetworkStream stream = client.GetStream();

        byte[] message = new byte[4096];
        Message deserializedMessage;
        int bytesRead;

        while (true) {
            bytesRead = 0;

            try {
                // Block until a client sends a message
                bytesRead = stream.Read(message, 0, 4096);
            } catch {
                // A socket error has occurred
                break;
            }

            if (bytesRead == 0) {
                // The client has disconnected from the server
                break;
            }

            // Message has successfully been received
            UTF8Encoding encoder = new UTF8Encoding();
            Debug.Log("Client message received: " + encoder.GetString(message, 0, bytesRead));


            try {
                // Convert the byte array to a string
                string jsonMessage = System.Text.Encoding.UTF8.GetString(message);
                Debug.Log("the message received is: " + jsonMessage);

                // Deserialize the JSON string into an object
                deserializedMessage = JsonConvert.DeserializeObject<Message>(jsonMessage);
            }
            catch (Exception e)
            {
                Debug.LogError("Error deserializing JSON: " + e.Message);
            }
            // Here, you could also handle the received message
        }

        stream.Close();
        client.Close();
        connectedClients.Remove(client);
    }

    private void SendMessage(TcpClient client, string message) {
        if (client == null) return;
        try {
            NetworkStream stream = client.GetStream();
            if (stream.CanWrite) {
                byte[] serverMessageAsByteArray = Encoding.ASCII.GetBytes(message);
                stream.Write(serverMessageAsByteArray, 0, serverMessageAsByteArray.Length);
            }
        } catch (Exception e) {
            Debug.Log("Error sending message to client: " + e.Message);
        }
    }
    void Update() {
        // Example usage: Send "hi" to all connected clients every 5 seconds
        // This could be triggered by some game event or condition instead
    }

    private void OnDestroy() {
        if (tcpListenerThread != null) {
            tcpListenerThread.Abort();
            tcpListener.Stop();
        }
        foreach (var client in connectedClients) {
            if (client.Connected) {
                client.Close();
            }
        }
    }
}

