using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Quit()
    {
        Application.Quit(); //Closes the Application
        Debug.Log("Quit");
    }

    public void Play()
    {
        GameManager.numOfPlayers = GameManager.NumConnectedControllers();
        GameManager.time = 120f;
        SceneManager.LoadScene("Level 1"); //Opens the level
        Debug.Log("Play");
    }
}
