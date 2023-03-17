// This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
// To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/ 
// or send a letter to Creative Commons, PO Box 1866, Mountain View, CA 94042, USA.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;



public class TCPTestClient : MonoBehaviour {
    public float updateInterval = 3.0f;
    private float nextUpdateTime = 0.0f;
    public bool controlState = false;
    public SphereMaterialChanger callColourChange;
    public SphereMaterialChanger2 callColourChange2;
    public TubeSpawner tubeSpawner;
    public TubeSpawnerMinistry tubeSpawnerMinistry;
    public dynamicText dynamicMessages;
    private string lastAction = "waiting";
    #region private members 	
    private TcpClient socketConnection; 	
	private Thread clientReceiveThread; 	
	#endregion  	
	// Use this for initialization 	
	void Start () {
		ConnectToTcpServer();


    }  	
	
	void Update () {

        // need to call colour change from update because it can't be called from the non-main thread 

        if (controlState == true)
        {
            Debug.Log("control state is true, update launched");
                if (lastAction == "sphere")
                {
                    Debug.Log("calling first sphere colour change");
                    callColourChange.changeColour();
                }
                else if (lastAction == "sphere2")
                {
                    Debug.Log("sphere 2 was triggered");
                    callColourChange2.changeColour2();
            }
                else if (lastAction == "spawn tube")
            {
                Debug.Log("spawn tube called");
                tubeSpawner.spawnTube();
            }
            else if (lastAction == "task complete")
            {
                Debug.Log("worker completed task");
               // dynamicMessages.MessageSubmitted();
              // tubeSpawnerMinistry.spawnTubeMinistry();
            }
            else
                {
                    Debug.Log("last action wasn't recognised");
                }
            }
        controlState = false;
    }
       

    private void ConnectToTcpServer () { 		
		try {  			
			clientReceiveThread = new Thread (new ThreadStart(ListenForData)); 			
			clientReceiveThread.IsBackground = true; 			
			clientReceiveThread.Start();
            Debug.Log("connected");
		} 		
		catch (Exception e) { 			
			Debug.Log("On client connect exception " + e); 		
		} 	
	}  	
	/// <summary> 	
	/// Runs in background clientReceiveThread; Listens for incomming data. 	   
	private void ListenForData() { 		
		try {
            //socketConnection = new TcpClient("51.144.101.111", 8080);  

            socketConnection = new TcpClient("127.0.0.1", 8080);

            Byte[] bytes = new Byte[1024];             
			while (true) { 				
				// Get a stream object for reading 				
				using (NetworkStream stream = socketConnection.GetStream()) { 					
					int length; 					
					// Read incomming stream into byte arrary. 					
					while ((length = stream.Read(bytes, 0, bytes.Length)) != 0) { 						
						var incommingData = new byte[length]; 						
						Array.Copy(bytes, 0, incommingData, 0, length); 						
						// Convert byte array to string message. 						
						string serverMessage = Encoding.UTF8.GetString(incommingData);
                        string[] messages = serverMessage.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string message in messages)
                        {
                            Debug.Log("server message received as: " + message);
                            if (serverMessage == "hello")
                            {
                                Debug.Log("server message was hello");

                            }
                            else
                            {
                                Debug.Log("server message was " + message);
                                lastAction = message;
                                Debug.Log(lastAction);
                                controlState = true;
                            }
                        }
					} 				
				} 			
			}         
		}         
		catch (SocketException socketException) {             
			Debug.Log("Socket exception: " + socketException);         
		}     
	}  	
	/// <summary> 	
	/// Send message to server using socket connection. 	
	/// </summary> 	
	private void SendMessage(string msgToSend) {
        Debug.Log("Sending Message");
		if (socketConnection == null) {             
			return;         
		}  		
		try { 			
			// Get a stream object for writing. 			
			NetworkStream stream = socketConnection.GetStream(); 			
			if (stream.CanWrite) {
                /*
				string clientMessage = "This is a message from one of your clients."; 				
				// Convert string message to byte array.         
                */
				byte[] clientMessageAsByteArray = Encoding.ASCII.GetBytes(msgToSend); 				
				// Write byte array to socketConnection stream.                 
				stream.Write(clientMessageAsByteArray, 0, clientMessageAsByteArray.Length);                 
				Debug.Log("Client sent his message - should be received by server");     

            }         
		} 		
		catch (SocketException socketException) {             
			Debug.Log("Socket exception: " + socketException);         
		}     
	}

[Serializable]
    public class Player
    {
        public string playerId;
        public string playerLoc;
        public string playerNick;
    }

    public void NewAction(string action)
    {
        Debug.Log("received the action " + action);
        SendMessage(action);
    }

    private void changeSphereColour()
    {
        callColourChange.changeColour();
    }
}