using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamestateManager : MonoBehaviour
{


    private delegate void StateChange(GameState state);

    private static StateChange OnChangeGameState;


    /// <summary>
    /// Assign Name of next scene to be loaded
    /// </summary>
    [SerializeField] GameState gameState;
    [SerializeField] GameState previousGameState;


    public GameState CurrentGameState   // property
    {
        get { return gameState; }   // get method
        set { gameState = value; }  // set method
    }
    public GameState PreviousGameState   // property
    {
        get { return previousGameState; }   // get method
        set { previousGameState = value; }  // set method
    }
    public void OnStateChange(GameState newstate)
    {
        switch (newstate)
        {

            case GameState.Startup:
                Console.WriteLine("Game is starting");
                break;
            case GameState.InGame:
                Console.WriteLine("in play state");
                break;
            case GameState.Dialogue:
                Console.WriteLine("showing dialogue");
                break;
            case GameState.End:
                Console.WriteLine("game is ending");
                break;



        }
    }

}
