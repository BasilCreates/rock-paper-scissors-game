using UnityEngine;

public class RPSGamePlayManager : MonoBehaviour
{
    // Player and CPU GameObjects (set these in the inspector)
    public GameObject playerRock;
    public GameObject playerPaper;
    public GameObject playerScissors;
    public GameObject cpuRock;
    public GameObject cpuPaper;
    public GameObject cpuScissors;

    // Spawn points for Player and CPU
    public Transform playerSpawnPoint;
    public Transform cpuSpawnPoint;

    // Start is called before the first frame update
    private void Start()
    {
        // Turn off all GameObjects in the hierarchy initially
        TurnOffAllObjects();

        // Randomly choose one for Player and CPU
        GameObject playerObject = GetRandomObject(true);  // true indicates Player
        GameObject cpuObject = GetRandomObject(false);    // false indicates CPU

        // Set positions for the Player and CPU objects
        if (playerObject != null)
        {
            playerObject.transform.position = playerSpawnPoint.position;
            playerObject.transform.rotation = playerSpawnPoint.rotation;
        }

        if (cpuObject != null)
        {
            cpuObject.transform.position = cpuSpawnPoint.position;
            cpuObject.transform.rotation = cpuSpawnPoint.rotation;
        }
    }

    // Turn off all GameObjects in the hierarchy
    private void TurnOffAllObjects()
    {
        playerRock.SetActive(false);
        playerPaper.SetActive(false);
        playerScissors.SetActive(false);
        cpuRock.SetActive(false);
        cpuPaper.SetActive(false);
        cpuScissors.SetActive(false);
    }

    // Get a random GameObject for Player or CPU (true for Player, false for CPU)
    private GameObject GetRandomObject(bool isPlayer)
    {
        int randomChoice = Random.Range(0, 3);  // 0: Rock, 1: Paper, 2: Scissors

        if (isPlayer)
        {
            // Player's objects
            switch (randomChoice)
            {
                case 0:
                    playerRock.SetActive(true);
                    return playerRock;
                case 1:
                    playerPaper.SetActive(true);
                    return playerPaper;
                case 2:
                    playerScissors.SetActive(true);
                    return playerScissors;
            }
        }
        else
        {
            // CPU's objects
            switch (randomChoice)
            {
                case 0:
                    cpuRock.SetActive(true);
                    return cpuRock;
                case 1:
                    cpuPaper.SetActive(true);
                    return cpuPaper;
                case 2:
                    cpuScissors.SetActive(true);
                    return cpuScissors;
            }
        }

        return null;  // Default to null if something goes wrong (shouldn't happen)
    }
}
