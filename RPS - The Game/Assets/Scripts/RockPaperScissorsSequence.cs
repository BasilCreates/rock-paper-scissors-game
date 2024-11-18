using UnityEngine;
using UnityEngine.UI;  // Required to access UI elements like Button
using System.Collections.Generic;

public class RockPaperScissorsSequence : MonoBehaviour
{
    public List<GameObject> objectsToDisplay;  // List of Rock, Paper, Scissors objects
    public Button playButton;  // Reference to the play button

    void Start()
    {
        // Initially deactivate all objects
        DeactivateAllObjects();

        // Add a listener to the button to trigger the random selection when clicked
        playButton.onClick.AddListener(ShowRandomObject);
    }

    // Method to randomly select and show an object
    void ShowRandomObject()
    {
        DeactivateAllObjects();  // Deactivate all before showing a new one

        // Get a random index from the list
        int randomIndex = Random.Range(0, objectsToDisplay.Count);

        // Activate the randomly selected object
        objectsToDisplay[randomIndex].SetActive(true);
    }

    // Method to deactivate all objects in the list
    void DeactivateAllObjects()
    {
        foreach (GameObject obj in objectsToDisplay)
        {
            obj.SetActive(false);  // Deactivate each object
        }
    }
}
