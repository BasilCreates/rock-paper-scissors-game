using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    void Start()
    {
        // Make sure cursor is visible and unlocked when the scene is loaded
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    void Update()
    {
        
    }
    
    //press to go to the rock, paper, scissors 
    public void PlayGame()
    {
        //loads up the Rock Paper Scissors screen
        SceneManager.LoadScene("RPS Screen");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("RPS Gameplay");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("RPS - Opening Menu");
    }
}
