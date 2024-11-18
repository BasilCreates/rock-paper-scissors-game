using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperThrower : MonoBehaviour
{
    public GameObject paperPrefab; // Prefab of the paper object to spawn
    public Transform objectSpawner; // Reference to the empty GameObject (spawner)
    public float moveSpeed = 10f; // Speed at which the object will move

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            // Spawn the paper object and shoot it forward
            SpawnAndShootPaper();
        }
    }

    private void SpawnAndShootPaper()
    {
        // Instantiate the paper prefab at the spawner's position and rotation
        GameObject paperObject = Instantiate(paperPrefab, objectSpawner.position, objectSpawner.rotation);

        // Start moving the paper object forward using a coroutine
        StartCoroutine(MovePaper(paperObject));
    }

    private IEnumerator MovePaper(GameObject paperObject)
    {
        // Calculate the direction to move the paper
        Vector3 moveDirection = objectSpawner.forward; // Forward direction based on the spawner's orientation

        // Move the paper object forward at the specified speed
        while (true)
        {
            // Update position based on move direction
            paperObject.transform.position += moveDirection * moveSpeed * Time.deltaTime;

            // Check for collision with the Dead Zone
            if (paperObject == null) yield break; // If the object was destroyed, exit the coroutine

            yield return null; // Wait for the next frame
        }
    }

    // This method is called when a collider enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dead Zone"))
        {
            // Destroy the paper object
            Destroy(gameObject); // Destroy the paper object when it hits the Dead Zone
        }
    }
}
