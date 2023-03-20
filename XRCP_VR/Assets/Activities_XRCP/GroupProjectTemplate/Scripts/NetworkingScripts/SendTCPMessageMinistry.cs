using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendTCPMessageMinistry : MonoBehaviour
{
    public TCPClientMinistry tcpTestClient;
    // Call this function every 3 seconds

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
