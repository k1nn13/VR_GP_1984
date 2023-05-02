using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class ComputerUI_Controller_V2 : MonoBehaviour
{
    //-------------------------
    [Header("Object Reference")]
    [SerializeField] GameObject tubeIn;
    [SerializeField] GameObject tubeOut;
    [SerializeField] GameObject socketInOBJ;
    [SerializeField] GameObject socketOutOBJ;

    Rigidbody rb;
    TubeSpawner ts;

    static PlaySound playSound;
    static PlaySoundManager playSoundManager;

    int tubeCount;
    public int tubeCountSent;

    public bool canLogMessage;
    public bool canLaunchMessage;


    //----------------------
    [Header("UI Component")]
    [SerializeField] GameObject numberRecievedOBJ;
    [SerializeField] GameObject numberSentOBJ;
    [SerializeField] GameObject inputDetectedOBJ;
    [SerializeField] GameObject messageLoggedOBJ;
    [SerializeField] GameObject messageOutOBJ;

    [Header("Buttons")]
    //[SerializeField] Button logBtn;
    [SerializeField] Button launchBtn;

    [Header("Update Text")]
    [SerializeField] TMP_Text warningText;
    [SerializeField] TMP_Text launchText;
   //[SerializeField] TMP_Text logText;

    [Header("Tube Output Transforms")]
    [SerializeField] Transform current;
    [SerializeField] Transform start;
    [SerializeField] Transform end;

    //-----------------------
    [Header("Public Values")]
    public float sliderValue = 0;
    public bool socketStateNow, socketOutState;

    //----------- UI
    dynamicText dt;
    ColorBlock cb;
    TextMeshProUGUI textRecieved;
    TextMeshProUGUI textSent;
    TextMeshProUGUI inputDetected;
    TextMeshProUGUI messageLogged;

    //---------------- Socket IN
    XRSocketInteractor socketIn;
    GameObject tubeMessageIn;
    TubeLog tubeLogIn;

    //--------------- Socket Out
    XRSocketInteractor socketOut;
    GameObject tubeMessageOut;
    TubeLog tubeLogOut;



    //---------------
    //--------- START
    void Start()
    {
        // Access TMP variables on Game Object
        textRecieved = numberRecievedOBJ.GetComponent<TextMeshProUGUI>();
        textSent = numberSentOBJ.GetComponent<TextMeshProUGUI>();

        // Get Access to number of tubes spawned
        ts = tubeIn.GetComponent<TubeSpawner>();

        // access input detected tmp to change colour
        inputDetected = inputDetectedOBJ.GetComponent<TextMeshProUGUI>();
        inputDetected.color = new Color(0, 0, 0, 0);

        // access message log tmp to change colour
        messageLogged = messageLoggedOBJ.GetComponent<TextMeshProUGUI>();
        messageLogged.color = new Color(0, 0, 0, 0);

        // Check whats in the socket
        socketIn = socketInOBJ.GetComponent<XRSocketInteractor>();
        socketOut = socketOutOBJ.GetComponent<XRSocketInteractor>();

        // keep track of number of tubes
        tubeCount = 0;
        tubeCountSent = 0;

        launchBtn.interactable = false;
        launchBtn.image.color = new Color(0, 0, 0, 0);
        launchText.color = new Color(0, 0, 0, 0);

        // warning message to prompt user on what to do
        messageOutOBJ.SetActive(false);

        //get audio component on tube output
        playSound = tubeOut.GetComponent<PlaySound>();
        playSoundManager = GetComponent<PlaySoundManager>();

        canLogMessage = true;
        canLaunchMessage = true;
    }

    //---------------------
    public void RemoveTube()
    {
        // trigger output sound - P Drive
        playSoundManager.TriggerSound(1);
    }

    //---------- Input Socket
    public void socketCheck()
    {
        //----------------------------------------------
        // Get current message game object in the socket
        IXRSelectInteractable objName = socketIn.GetOldestInteractableSelected();
        tubeMessageIn = objName.transform.gameObject;
        tubeLogIn = tubeMessageIn.GetComponent<TubeLog>();

        // trigger input sound
        playSoundManager.TriggerSound(0);
    }

    //---------------------------------------
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
            if (tubeLogOut == null)
            {
                if (canLogMessage)
                {
                    if (tubeLogIn != null)
                    {
                        if (tubeLogIn.isLogged)
                        {
                            launchBtn.onClick.RemoveListener(LogMessage);
                            launchBtn.onClick.RemoveListener(LaunchMessage);

                            // if message logged change color to green
                            messageLogged.color = new Color(0, 255, 0, 255);

                            launchBtn.interactable = false;
                            launchBtn.image.color = new Color(255, 255, 255, 0);
                            launchText.color = new Color(255, 255, 255, 0);

                            // display warning message
                            messageOutOBJ.SetActive(true);
                            warningText.text = "Remove message from P drive and place under output pipe!";

                            // Trigger Sound
                            playSoundManager.TriggerSound(3);
                        }

                        if (!tubeLogIn.isLogged)
                        {
                            // displays message red - not logged
                            messageLogged.color = new Color(255, 0, 0, 255);

                            //--------------------------
                            // Listen to when ui button is pressed
                            launchBtn.onClick.AddListener(LogMessage);
                            launchBtn.onClick.RemoveListener(LaunchMessage);
                            launchBtn.interactable = true;
                            launchBtn.image.color = new Color(255, 255, 255, 255);
                            launchText.color = new Color(255, 255, 255, 255);
                            launchText.text = "Log Message";
                        }
                    }
                }

            }
            
        }

        //-----------------
        if (!socketStateNow)
        {
            // set colour -> Alpha to transparent
            inputDetected.color = new Color(0, 0, 0, 0);
            messageLogged.color = new Color(0, 0, 0, 0);

            launchBtn.interactable = false;
            launchBtn.image.color = new Color(255, 255, 255, 0);
            launchText.color = new Color(255, 255, 255, 0);

            messageOutOBJ.SetActive(false);
        }

    }

    //------------- Output Socket
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
            playSoundManager.TriggerSound(3);

            // Listen to when ui button is pressed
            // update listener event 
            launchBtn.onClick.AddListener(LaunchMessage);
            launchBtn.onClick.RemoveListener(LogMessage);
            launchBtn.interactable = true;
            launchBtn.image.color = new Color(255, 255, 255, 255);
            launchText.color = new Color(255, 255, 255, 255);
            launchText.text = "Launch";

            // display warning message
            messageOutOBJ.SetActive(true);
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
            messageOutOBJ.SetActive(false); 
        }
    }

    //------------------
    void LaunchMessage()
    {
        // launches tube
        EventManager.OnMessageLaunched(); // trigger event to update game state

        playSoundManager.TriggerSound(2);
        playSound.TriggerSound();
        tubeCountSent++;
        textSent.text = (tubeCountSent).ToString();
        launchBtn.onClick.RemoveListener(LaunchMessage);
        StartCoroutine(launchCoroutine());
    }

    //---------------------------
    IEnumerator launchCoroutine()
    {
        // launches the meesage up the pipe
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

    //---------------
    void LogMessage()
    {
        // updates game objects log state
        if (tubeLogIn != null)
        {
            if (!tubeLogIn.isLogged)
            {
                playSoundManager.TriggerSound(2);
                tubeLogIn.isLogged = true;
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

}
