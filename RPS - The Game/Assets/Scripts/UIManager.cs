using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    void Start()
    {
        
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
}
