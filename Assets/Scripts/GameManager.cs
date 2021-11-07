using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;
    public PlayerInputManager inputManager;
    public const int MAX_PLAYERS = 4; //holds a constant for the Maximum allowed players

    public static int numOfPlayers; //Contains the actual number of players
    public static string map; //Contains the selected map
    public static Vector2[] spawnPoints = new Vector2[4]; //stores the spawn points for 4 players

    public GameObject player;  //gets the player prefab

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

        inputManager.playerPrefab = player;

        spawnPoints[0] = new Vector2(-8.75f, -3f);   //Sets the spawn points for each player to use in an array
        spawnPoints[1] = new Vector2(8.75f, -3f);
        spawnPoints[2] = new Vector2(-8.75f, 3f);
        spawnPoints[3] = new Vector2(8.75f, 3f);
    }

    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled.
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void Update()
    {
        //Gets all connected Gamepads and puts them into a list
        var Gamepads = Gamepad.all;

        numOfPlayers = NumConnectedControllers();
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
        if(scene.name == map)
        {
            CreatePlayers();
        }
    }

    public void CreatePlayers()
    {
        var Gamepads = Gamepad.all;
        for (int i = 0; i < numOfPlayers; i++)  //Creates the required number of player characters
        {
            Debug.Log(Gamepads[i]);
            inputManager.JoinPlayer(i, -1, null, Gamepads[i]);
        }
    }
}