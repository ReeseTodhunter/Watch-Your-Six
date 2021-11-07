using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager GM;
    private const int MAX_PLAYERS = 4; //holds a constant for the Maximum allowed players
    public static int numOfPlayers; //Contains the selected number of players
    public static string map;

    public static GameObject[] players = new GameObject[4]; //Sets up the players array to hold all created player objects
    public static Vector2[] spawnPoints = new Vector2[4]; //Sets up the array to store spawn points in
    public static Quaternion[] spawnRotations = new Quaternion[4]; //Sets up the array to store spawn rotations in


    [SerializeField]
    public GameObject player;

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

        spawnPoints[0] = new Vector2(-8.3f, -3f);
        spawnPoints[1] = new Vector2(8.3f, -3f);
        spawnPoints[2] = new Vector2(-8.3f, 3f);
        spawnPoints[3] = new Vector2(8.3f, 3f);

        spawnRotations[0] = Quaternion.Euler(0, 0, 315);
        spawnRotations[1] = Quaternion.Euler(0, 0, 45);
        spawnRotations[2] = Quaternion.Euler(0, 0, 225);
        spawnRotations[3] = Quaternion.Euler(0, 0, 135);

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
        for (int i = 0; i < numOfPlayers; i++)
        {
            players[i] = Instantiate(player, spawnPoints[i], spawnRotations[i]);
            players[i].GetComponent<Movement>().playerNum = i;
        }
    }
}