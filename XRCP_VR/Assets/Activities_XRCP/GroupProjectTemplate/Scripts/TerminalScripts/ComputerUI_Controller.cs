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

    [Header("UI Component")]
    [SerializeField] Button btn_0;
    [SerializeField] Slider slider_0;
    [SerializeField] GameObject tmpRecievedOBJ;
    [SerializeField] GameObject inputDetectedOBJ;
    [SerializeField] GameObject messageLoggedOBJ;

    [Header("Public Values")]
    public float sliderValue = 0;
    public bool socketStateNow;

    ////------------------------ Update UI
    dynamicText dt;
    TubeSpawner ts;
    TextMeshProUGUI textRecieved;
    TextMeshProUGUI inputDetected;
    TextMeshProUGUI messageLogged;
    ColorBlock cb;

    //------------------------ Socket Test
    XRSocketInteractor socket;
    GameObject tubeMessage;
    TubeLog tubeLog;

    //---------------
    void Start()
    {
        // Access TMP variables on Game Object
        textRecieved = tmpRecievedOBJ.GetComponent<TextMeshProUGUI>();

        // Get Access to number of tubes spawned
        ts = pTube.GetComponent<TubeSpawner>();

        // Listen to when ui button is pressed
        btn_0.onClick.AddListener(TaskOnClick);

        // access input detected tmp to change colour
        inputDetected = inputDetectedOBJ.GetComponent<TextMeshProUGUI>();
        inputDetected.color = new Color(0, 0, 0, 0);

        // access message log tmp to change colour
        messageLogged = messageLoggedOBJ.GetComponent<TextMeshProUGUI>();
        messageLogged.color = new Color(0, 0, 0, 0);

        // Check whats in the socket
        socket = socketOBJ.GetComponent<XRSocketInteractor>();

        // Variables to change text colour
        cb = slider_0.colors;
        cb.normalColor = Color.red;
        slider_0.colors = cb;
    }

    //------------------------------------
    public void socketCheck()
    {
        //--------------------------------------------------------------------
        // Get current message game object in the socket
        IXRSelectInteractable objName = socket.GetOldestInteractableSelected();
        tubeMessage = objName.transform.gameObject;
        tubeLog = tubeMessage.GetComponent<TubeLog>();
    }

    //----------------
    void TaskOnClick()
    {
        //------------------------------------------------------------------------------
        //-- Use for debugging, when ui is pressed a new tube is spawned in tube spawner
        sliderValue ++;

        if (sliderValue > 1)
        {
            sliderValue = 0;
        }

        slider_0.value = sliderValue;

        //--------------------------------
        // change colour of text, debugging
        if (sliderValue == 0)
        {
            cb.normalColor = Color.red;
        }
        
        if (sliderValue == 1)
        {
            cb.normalColor = Color.green;
        }

        slider_0.colors = cb;


        //----------------------------------------------
        // update tmp "number of messages recieved"
        // this will need to work with ministry input!!!!!!!
        textRecieved.text = (ts.tubeCount + 1).ToString();
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
                if(tubeLog.isLogged)
                {
                    messageLogged.color = new Color(0, 255, 0, 255);
                }

                if (!tubeLog.isLogged)
                {
                    messageLogged.color = new Color(255, 0, 0, 255);
                }
            }
          
        }
        
        //-----------------
        if (!socketStateNow)
        {
            // set colour -> Alpha to transparent
            inputDetected.color = new Color(0, 0, 0, 0);
            messageLogged.color = new Color(0, 0, 0, 0);
        }
    }
}
