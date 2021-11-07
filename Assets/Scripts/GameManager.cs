using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;
    public const int MAX_PLAYERS = 4; //holds a constant for the Maximum allowed players
    public static int numOfPlayers; //Contains the selected number of players
    public static string map;

    void Awake()
    {
        if (GM == null)
        {
            DontDestroyOnLoad(gameObject);
            GM = this;
        }
        else if (GM != this)
        {
            Destroy(gameObject);
        }
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    public static int NumConnectedControllers()
    {
        int gamepadNum = 0;
        var Gamepads = Gamepad.all;
        for (int i = 0; i < MAX_PLAYERS; i++)
        {
            try
            {
                if (Gamepads[i] != null)
                {
                }
            }
            catch
            {
                gamepadNum = i;
                i = MAX_PLAYERS;
            }
        }
        return gamepadNum;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        for(int i = 0; i < numOfPlayers; i++)
        {

        }
    }
}