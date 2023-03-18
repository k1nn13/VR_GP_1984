using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class ComputerUI_Controller : MonoBehaviour
{
    [Header("Object Reference")]
    [SerializeField] GameObject pTube;
    [SerializeField] GameObject socketOBJ;
    [SerializeField] GameObject socketOutOBJ;

    [Header("UI Component")]

    [SerializeField] GameObject tmpRecievedOBJ;
    [SerializeField] GameObject tmpSentOBJ;
    [SerializeField] GameObject inputDetectedOBJ;
    [SerializeField] GameObject messageLoggedOBJ;
    [SerializeField] GameObject messageOutCanvas;
    [SerializeField] Button btn_0, logBtn, launchBtn;
    [SerializeField] TMP_Text warningText, launchText;
    [SerializeField] Transform current, start, end;

    [Header("Public Values")]
    public float sliderValue = 0;
    public bool socketStateNow, socketOutState;

    ////------------------------ Update UI
    dynamicText dt;
    TubeSpawner ts;
    TextMeshProUGUI textRecieved;
    TextMeshProUGUI textSent;
    TextMeshProUGUI inputDetected;
    TextMeshProUGUI messageLogged;
    ColorBlock cb;
    int tubeCount;
    int tubeCountSent;
    Rigidbody rb;

    //------------------------ Socket IN
    XRSocketInteractor socket;
    GameObject tubeMessage;
    TubeLog tubeLog;

    //------------------------ Socket Out
    XRSocketInteractor socketOut;
    GameObject tubeMessageOut;
    TubeLog tubeLogOut;

    //---------------
    void Start()
    {
        // Access TMP variables on Game Object
        textRecieved = tmpRecievedOBJ.GetComponent<TextMeshProUGUI>();
        textSent = tmpSentOBJ.GetComponent<TextMeshProUGUI>();

        // Get Access to number of tubes spawned
        ts = pTube.GetComponent<TubeSpawner>();

        // Listen to when ui button is pressed
        btn_0.onClick.AddListener(TaskOnClick); // main button debugging
        logBtn.onClick.AddListener(LogMessage);
        launchBtn.onClick.AddListener(LaunchMessage);


        // access input detected tmp to change colour
        inputDetected = inputDetectedOBJ.GetComponent<TextMeshProUGUI>();
        inputDetected.color = new Color(0, 0, 0, 0);

        // access message log tmp to change colour
        messageLogged = messageLoggedOBJ.GetComponent<TextMeshProUGUI>();
        messageLogged.color = new Color(0, 0, 0, 0);

        // Check whats in the socket
        socket = socketOBJ.GetComponent<XRSocketInteractor>();
        socketOut = socketOutOBJ.GetComponent<XRSocketInteractor>();

        // keep track of number of tubes
        tubeCount = 0;
        tubeCountSent = 0;

        // Display button to update message log on tube
        logBtn.interactable = false;
        logBtn.image.color = new Color(0,0,0,0);

        launchBtn.interactable = false;
        launchBtn.image.color = new Color(0, 0, 0, 0);
        launchText.color = new Color(0, 0, 0, 0);

        // warning message to prompt user on what to do
        messageOutCanvas.SetActive(false);
    }




    //------------------------ Input Socket
    public void socketCheck()
    {
        //--------------------------------------------------------------------
        // Get current message game object in the socket
        IXRSelectInteractable objName = socket.GetOldestInteractableSelected();
        tubeMessage = objName.transform.gameObject;
        tubeLog = tubeMessage.GetComponent<TubeLog>();
    }

    //------------------------------------------
    public void updateSocketState(bool state)
    {
        // check state of socket and check if message has been logged
        // changes colour of UI text

        socketStateNow = state;

        //----------------
        if (socketStateNow)
        {
            // set input detected colour -> Yellow
            inputDetected.color = new Color(255, 255, 0, 255);

            if (tubeLog != null)
            {
                if (tubeLog.isLogged)
                {
                    // if message logged change color to green
                    messageLogged.color = new Color(0, 255, 0, 255);

                    // hide button to log message
                    logBtn.interactable = false;
                    logBtn.image.color = new Color(255, 255, 255, 0);

                    // display warning message
                    messageOutCanvas.SetActive(true);
                    warningText.text = "Remove message from P drive and place under output pipe!";
                }

                if (!tubeLog.isLogged)
                {
                    // displays message red - not logged
                    messageLogged.color = new Color(255, 0, 0, 255);

                    // displays button allowing user to log message
                    logBtn.interactable = true;
                    logBtn.image.color = new Color(255, 255, 255, 255);
                }
            }

        }

        //-----------------
        if (!socketStateNow)
        {
            // set colour -> Alpha to transparent
            inputDetected.color = new Color(0, 0, 0, 0);
            messageLogged.color = new Color(0, 0, 0, 0);


            logBtn.interactable = false;
            logBtn.image.color = new Color(0, 0, 0, 0);

            messageOutCanvas.SetActive(false);
        }

        Debug.Log("Update Socket");
    }

    //-------------------------- Output Socket
    public void socketOutCheck()
    {
        //--------------------------------------------------------------------
        // Get current message game object in the socket
        IXRSelectInteractable objName = socketOut.GetOldestInteractableSelected();
        tubeMessageOut = objName.transform.gameObject;
        tubeLogOut = tubeMessageOut.GetComponent<TubeLog>();
        rb = tubeMessageOut.GetComponent<Rigidbody>();

        if (tubeLogOut.isLogged)
        {
            launchBtn.interactable = true;
            launchBtn.image.color = new Color(255, 255, 255, 255);
            launchText.color = new Color(255, 255, 255, 255);

            // display warning message
            messageOutCanvas.SetActive(true);
            warningText.text = "Press the button to send the message";
        }
       
    }

    //------------------------------------
    public void SocketOutState(bool state)
    {
        socketOutState = state;

        if (!socketOutState)
        {
            launchBtn.interactable = false;
            launchBtn.image.color = new Color(255, 255, 255, 0);
            launchText.color = new Color(255, 255, 255, 0);

            // display warning message
            messageOutCanvas.SetActive(false);
            
        }
    }

    //------------------
    //------------------
    void LaunchMessage()
    {
       
        // launches tube
        //Destroy(tubeMessageOut);
        //current.transform.position = end.transform.position;
        tubeCountSent++;
        textSent.text = (tubeCountSent).ToString();
        StartCoroutine(launchCoroutine());

    }

    //-----------------------------
    IEnumerator launchCoroutine()
    {

        
        Vector3 startPos = start.transform.position;
        Vector3 endPos = end.transform.position;

        float timeElapsed = 0;
        float lerpTime = 0.5f;

        while (timeElapsed < lerpTime)
        {
            current.transform.position = Vector3.Lerp(startPos, endPos, (timeElapsed / lerpTime));

            timeElapsed += Time.deltaTime;
            yield return new WaitForEndOfFrame();

        }
        Destroy(tubeMessageOut);
        current.transform.position = start.transform.position;
    }

    //------------------
    void LogMessage()
    {
        // updates game objects log state
        if (tubeLog != null)
        {
            if (!tubeLog.isLogged)
            {
                tubeLog.isLogged = true;
                updateSocketState(true);
            }
        }
    }

    //---------------------------
    public void UpdateTubeCount()
    {
        // listen to tube spawn function to update number of tubes spawned
        tubeCount++;    
        textRecieved.text = (tubeCount).ToString();
    }

    //----------------
    public void TaskOnClick()
    {

    }


}
