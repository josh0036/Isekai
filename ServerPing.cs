using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ServerPing : MonoBehaviour
{
    public string serverIP = "127.0.0.1"; // Change this to the IP address of your server
    public Text pingText; // Assign a Text UI element to this variable in the Inspector

    private void Start()
    {
        StartCoroutine(PingServer());
    }

    IEnumerator PingServer()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://" + serverIP);
        www.timeout = 1; // Set the timeout to 1 second
        yield return www.SendWebRequest();

        if (www.error != null)
        {
            Debug.Log("Error: " + www.error);
        }
        else
        {
            pingText.text = "Server ping: " + www.responseTime + "ms";
        }
    }
}
