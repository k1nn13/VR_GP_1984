using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendTCPMessage : MonoBehaviour
{
    public TCPTestClient tcpTestClient;
    // Call this function every 3 seconds
    void CallFunctionRepeatedly()
    {
        Debug.Log("Calling function repeatedly");
        tcpTestClient.NewAction("hello");
        // add code here to perform the function you want to call
    }

    // Start is called before the first frame update
    void Start()
    {
       // InvokeRepeating("CallFunctionRepeatedly", 0f, 3f);
    }

    void SendMessage(string messageToSend)
    {
        tcpTestClient.NewAction(messageToSend);
    }
}
