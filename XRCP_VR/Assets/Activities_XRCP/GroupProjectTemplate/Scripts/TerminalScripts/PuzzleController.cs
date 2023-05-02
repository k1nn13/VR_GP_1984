using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleController : MonoBehaviour
{

    [SerializeField] GameObject btn;
    [SerializeField] GameObject canvas;
    [SerializeField] int numberOfChildren;

    //--------------------
    GameObject[] btnArray;
    PuzzleState newPuzzleState;
    float startOffset = -250;

    //----------------------
    float theta = 0;
    float amplitude = 2f;
    float speed = 0.001f;
    float period = 4f;
    float dx;
    float TWO_PI = Mathf.PI * 2f;
    float[] yValues;
    float x;
    float positionOffset = 55f;

    bool triggerSin;
    //------------------
    private void Start()
    {
        //subscribe to event
        GameManager1984.OnPuzzleStateChanged += OnPuzzleStateChanged;

        triggerSin = false;

        dx = (TWO_PI / period) * 55f;
    }

    //-----------------------------------------
    void OnPuzzleStateChanged(PuzzleState state)
    {
        if (state == PuzzleState.puzzle_1)
        {
            StartPuzzle_1();
        }
    }

    //----------------------
    public void StartPuzzle_1()
    {

        // Get Reference to ui elements
        btnArray = new GameObject[numberOfChildren];
        yValues= new float[numberOfChildren];

        for ( int i = 0; i < numberOfChildren; i++ )
        {
            GameObject newBtn = Instantiate(btn) as GameObject;
            btnArray[i] = newBtn;
            btnArray[i].transform.SetParent(canvas.transform.transform, false);
            btnArray[i].GetComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
            btnArray[i].transform.localPosition = new Vector3((i * positionOffset) + startOffset, 0, 0);
        }

        triggerSin = true;

    }

    //---------------------------------------------------------------------------------------
    private float Map(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    //--------------------------
    private void Update()
    {

        if (triggerSin)
        {
            theta += speed;
            x = theta;

            for (int i = 0; i < numberOfChildren; i++)
            {
                yValues[i] = Map(Mathf.Sin(x) * amplitude, -1f, 1f, -70f, 80f);
                //yValues[i] = Mathf.Sin(x) * amplitude;
                x += dx;

                // update y position of buttons
                btnArray[i].transform.localPosition = new Vector3((i * positionOffset) + startOffset, yValues[i], 0);
            }


            //--------Check if puzzle complete then destroy
            if (PuzzleComplete())
            {
                Debug.Log("Complete");

                // launches tube
                EventManager.OnMessageLaunched(); // trigger event to update game state
                triggerSin = false;

                for (int i = 0; i < numberOfChildren; i++)
                {
                    Destroy(btnArray[i].gameObject);
                }
            }

        }
    }


    //-----------------------------
    bool PuzzleComplete()
    {

        for (int i = 0; i < numberOfChildren; i++)
        {
            if (btnArray[i].GetComponent<Button>().interactable == true)
            {
                return false;
            }
        }

        return true;
    }

    //----------------------
    //----------------------
    void OnDestroy()
    {
        // unsubscribe from event
        GameManager1984.OnPuzzleStateChanged -= OnPuzzleStateChanged;
    }
}
