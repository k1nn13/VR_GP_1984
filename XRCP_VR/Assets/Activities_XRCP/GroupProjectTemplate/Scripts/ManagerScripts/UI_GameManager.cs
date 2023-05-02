using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;


public class UI_GameManager : MonoBehaviour
{
    //------------------------
    [SerializeField] Text textContainer;
    [SerializeField] Button btn;
    [SerializeField] Text btnTxt;
    [SerializeField] GameObject pneumaticTube;
    [SerializeField] GameObject gameManager;
    [SerializeField] Canvas puzzleCanvas;
    [SerializeField] GameObject wirePuzzlePrefab;
    [SerializeField] Transform wirePuzzleLocation;

    Canvas _puzzleCanvas;
    GameManager1984 gameManager1984;

    static ComputerUI_Controller_V2 uiController;
    static TubeSpawner tubeSpawner;
    static PlaySound playSound;
    static PlaySoundManager playSoundManager;
    int numberOfMessagesIncoming;

    GameState newGameState;


    public string[] textInput = new string[2] {
        "Warning.. Due to the recent terrorist attack a state of lock down has been ordered. " +
        "Biological defense units have detected virus strain OCE-XoV-2 causing a recent pandemic. " +
        "All party members must now work remotely. Press Continue to begin your training. ",
        "You will be notified when a message is incoming and must respond promptly. Press continue to view the notification."


    };


    //-----------------
    private void Start()
    {
        //get references
        playSoundManager = GetComponent<PlaySoundManager>();
        tubeSpawner = pneumaticTube.GetComponent<TubeSpawner>();
        uiController = GetComponent<ComputerUI_Controller_V2>();
        gameManager1984 = gameManager.GetComponent<GameManager1984>();
        _puzzleCanvas = puzzleCanvas.GetComponent<Canvas>();
        _puzzleCanvas.enabled = false;

        // set listeners 
        btn.onClick.AddListener(UpdateGameState);

        // set method for event call GameManager
        GameManager1984.OnGameStateChanged += OnGameStateChanged;
    }

    //--------------------
    private void OnEnable()
    {
        EventManager.MessageLaunched += EventUpdateGameState;
    }

    //------------------
    private void OnDisable()
    {
        EventManager.MessageLaunched -= EventUpdateGameState;
    }


    //----------------------------------------------
    //--------------- Logic For Updateing Game State
    private void OnGameStateChanged(GameState state)
    {
        //------------------------------- Tutorial_0 : Introduction
        if (state == GameState.Tutorial_0)
        {
            textContainer.alignment = TextAnchor.MiddleCenter;
            SetButtonVisible(btn, true);
            SetText(textContainer, textInput[0]);
            newGameState = GameState.Tutorial_1;
        }

        //------------------------------- Tutorial_1 : Notification
        if (state == GameState.Tutorial_1)
        {
            SetText(textContainer, textInput[1]);
            newGameState = GameState.Tutorial_2;
        }

        //------------------------------- Tutorial_2 : Incoming Message
        if (state == GameState.Tutorial_2)
        {
            IncomingMessage(textInput[2]);
            newGameState = GameState.Tutorial_3;
            btn.onClick.AddListener(RecieveMessage);
        }

        //------------------------------- Tutorial_3 : Message tutorial
        if (state == GameState.Tutorial_3)
        {
            btn.onClick.RemoveListener(RecieveMessage);
            SetButtonVisible(btn, false);
            SetText(textContainer, textInput[3]);
            newGameState = GameState.Tutorial_4;
        }

        //------------------------------- Tutorial_4 : End of Tutorial
        if (state == GameState.Tutorial_4)
        {
            SetButtonVisible(btn, true);
            SetText(textContainer, textInput[4]);
            newGameState = GameState.Level_0;            
        }

        //-------------------------------
        //------------------------------- Level_0 : Message Loop A
        if (state == GameState.Level_0)
        {
            numberOfMessagesIncoming = 2;
            IncomingMessage(textInput[2]);
            btn.onClick.AddListener(RecieveMultipleMessages);
            newGameState = GameState.Level_1;
        }

        //------------------------------- Level_1 : Message Loop B
        if (state == GameState.Level_1)
        {
            btn.onClick.RemoveListener(RecieveMultipleMessages);
            SetButtonVisible(btn, false);

            if (uiController.tubeCountSent < 2)
            {
                SetText(textContainer, textInput[5]); // set text blank
            } 
            else
            {
                SetText(textContainer, textInput[6]);
                EventManager.OnTriggerBroadcast();
                newGameState = GameState.Level_2;
                uiController.canLogMessage = false;
            }
        }

        //-------------------------------  Level_2 : Post Broadcast Incoming message
        if (state == GameState.Level_2)
        {
            SetText(textContainer, textInput[7]);
            Invoke("UpdateMessage", 2);
            newGameState = GameState.Level_3;
        }


        //------------------------------- Level_3 : called automatically when opacity begins to return to 1f
        if (state == GameState.Level_3)
        {
            IncomingMessage(textInput[2]);
            numberOfMessagesIncoming = 20;
            btn.onClick.AddListener(RecieveMultipleMessages);
            newGameState = GameState.Level_4;
        }

        //------------------------------- Level_4 : wait for tube to be input
        if (state == GameState.Level_4)
        {
            SetButtonVisible(btn, false);
            btn.onClick.RemoveListener(RecieveMultipleMessages);

            SetText(textContainer, textInput[8]);
            uiController.canLogMessage = false;
            newGameState = GameState.Level_5;
        }

        //------------------------------- Level_5 : Error NotifcationLaunch Puzzle Game
        if (state == GameState.Level_5)
        {
            textContainer.alignment = TextAnchor.MiddleLeft;
            SetText(textContainer, textInput[9]);
            newGameState = GameState.Level_6;
            SetButtonVisible(btn, true);
        }

        //------------------------------- Level_6 : Launch Puzzle Game
        if (state == GameState.Level_6)
        {
            _puzzleCanvas.enabled = true;
            SetText(textContainer, textInput[5]);
            newGameState = GameState.Level_7;
            SetButtonVisible(btn, false);

            GameManager1984.Instance.UpdatePuzzleState(PuzzleState.puzzle_1);
        }

        //------------------------------- Level_7 : Exit Puzzle Game prompt to re enter tube
        if (state == GameState.Level_7)
        {
            textContainer.alignment = TextAnchor.MiddleCenter;

            // Trigger Sound
            playSoundManager.TriggerSound(4);

            _puzzleCanvas.enabled = false;
            SetText(textContainer, textInput[10]);
            newGameState = GameState.Level_8;

            uiController.canLogMessage = true;
            GameManager1984.Instance.UpdatePuzzleState(PuzzleState.puzzle_0);
        }

        //------------------------------- Level_8 : wait for tube to be input to load second puzzle event
        if (state == GameState.Level_8)
        {
            SetText(textContainer, textInput[11]);
            uiController.canLogMessage = false;
            newGameState = GameState.Level_9;
        }

        //------------------------------- Level_9 : move to tele screen puzzle
        if (state == GameState.Level_9)
        {
            SetText(textContainer, textInput[12]);
            newGameState = GameState.Level_10;
            SpawnWirePuzzle();
        }

        //------------------------------- Level_10 : finish telescreen puzzle
        if (state == GameState.Level_10)
        {
            SetText(textContainer, textInput[10]);
            uiController.canLogMessage = true;
            playSoundManager.TriggerSound(4);
            newGameState = GameState.Level_11;
        }

        //------------------------------- Level_11 : Brotherhood
        if (state == GameState.Level_11)
        {
            textContainer.alignment = TextAnchor.MiddleLeft;
            SetText(textContainer, textInput[13]);
            SetButtonVisible(btn, true);
            newGameState = GameState.Level_12;
        }

        //------------------------------- Level_12 : End
        if (state == GameState.Level_12)
        {
            textContainer.alignment = TextAnchor.MiddleCenter;
            SetText(textContainer, textInput[14]);
            SetButtonVisible(btn, false);
            //newGameState = GameState.Level_10;
        }

    }

    //---------------------
    void SpawnWirePuzzle()
    {
        Instantiate(wirePuzzlePrefab, wirePuzzleLocation.transform.position, wirePuzzleLocation.transform.rotation);
    }

 
    //-------------------------------------------
    void SetButtonVisible(Button btn, bool Switch)
    {

        btn.interactable = Switch;

        if (Switch)
        {
            btn.image.color = new Color(76, 82, 21, 20);
            btnTxt.color = new Color(0, 0, 0, 255);
        }
        else if (!Switch)
        {
            btn.image.color = new Color(0, 0, 0, 0);
            btnTxt.color = new Color(0, 0, 0, 0);
        }
    }

    //-------------------- Call to trigger Incoming Message Logic
    void IncomingMessage(String textInput)
    {
        textContainer.alignment = TextAnchor.MiddleCenter;
        SetText(textContainer, textInput);
        SetButtonVisible(btn, false);
        Invoke("UpdateMessage", 2);
    }

    //------------------
    void UpdateMessage()
    {
        // Trigger Sound
        playSoundManager.TriggerSound(4);
        SetButtonVisible(btn, true);
    }


    //----------------------
    void RecieveMessage()
    {
        tubeSpawner.spawnTube();
    }

    //--------------------------
    void RecieveMultipleMessages()
    {
        StartCoroutine(RecieveMessages(numberOfMessagesIncoming));
    }

    //------------------------------------
    IEnumerator RecieveMessages(int count)
    {
        int i = 0;
        while (i < count)
        {
            i++;
            tubeSpawner.spawnTube();
            yield return new WaitForSeconds(1.0f);
        }
    }

    //----------------------------------------
    public void CheckDriveSocketState(bool state)
    {
        if (state)
        {
            if(gameManager != null)
            {
                if (gameManager1984.State == GameState.Level_4)
                {
                    UpdateGameState();
                }

                if (gameManager1984.State == GameState.Level_8)
                {
                    UpdateGameState();
                }
            }
        }
    }

    //-----------------------------------------------
    void SetText(Text textContainer, string textInput)
    {
        textContainer.text = textInput;
    }

    //------------------- Event Clicked use listener
    public void UpdateGameState()
    {
        playSoundManager.TriggerSound(2);
        GameManager1984.Instance.UpdateGameState(newGameState);
    }

    //------------------ Event Called by UI Controller
    void EventUpdateGameState()
    {
        GameManager1984.Instance.UpdateGameState(newGameState);
    }


    //----------------------
    void OnDestroy()
    {
        GameManager1984.OnGameStateChanged -= OnGameStateChanged;
    }
}
