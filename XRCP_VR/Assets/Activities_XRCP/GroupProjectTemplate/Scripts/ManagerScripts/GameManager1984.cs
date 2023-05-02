using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-------------------
public enum GameState
{
    Tutorial_0,
    Tutorial_1,
    Tutorial_2,
    Tutorial_3,
    Tutorial_4,
    Level_0,
    Level_1,
    Level_2,
    Level_3,
    Level_4,
    Level_5,
    Level_6,
    Level_7,
    Level_8,
    Level_9,
    Level_10,
    Level_11,
    Level_12
}

public enum PuzzleState
{
    puzzle_0,
    puzzle_1,
    puzzle_2
}

//------------------------------------------
public class GameManager1984 : MonoBehaviour
{
    public static GameManager1984 Instance;
    public static event Action<GameState> OnGameStateChanged;
    public GameState State;

    public static event Action<PuzzleState> OnPuzzleStateChanged;
    public PuzzleState puzzleState;

    //-----------------
    void Awake()
    {
        Instance = this;
    }

    //------------------
    private void Start()
    {
        // initalize starting game state
        UpdateGameState(GameState.Tutorial_0);
        //UpdateGameState(GameState.Level_11);

        // set puzzle state
        UpdatePuzzleState(PuzzleState.puzzle_0);
    }

    //---------------------------------------------
    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.Tutorial_0:
                break;
            case GameState.Tutorial_1:
                break;
            case GameState.Tutorial_2:
                break;
            case GameState.Tutorial_3:
                break;
            case GameState.Tutorial_4:
                break;
            case GameState.Level_0:
                break;
            case GameState.Level_1:
                break;
            case GameState.Level_2:
                break;
            case GameState.Level_3:
                break;
            case GameState.Level_4:
                break;
            case GameState.Level_5:
                break;
            case GameState.Level_6:
                break;
            case GameState.Level_7:
                break;
            case GameState.Level_8:
                break;
            case GameState.Level_9:
                break;
            case GameState.Level_10:
                break;
            case GameState.Level_11:
                break;
            case GameState.Level_12:
                break;
        }

        OnGameStateChanged?.Invoke(newState);
    }



    //---------------------------------------------
    public void UpdatePuzzleState(PuzzleState newState)
    {
        puzzleState = newState;

        switch (newState)
        {
            case PuzzleState.puzzle_0:
                break;
            case PuzzleState.puzzle_1:
                break;
            case PuzzleState.puzzle_2:
                break;

        }

        OnPuzzleStateChanged?.Invoke(newState);
    }

}


