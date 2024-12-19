using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperThrower : MonoBehaviour
{
    public GameObject paperPrefab; // Prefab of the paper object to spawn
    public Transform objectSpawner; // Reference to the empty GameObject (spawner)
    public float moveSpeed = 10f; // Speed at which the object will move
    public float lifetime = 4f; // Time after which the projectile will be destroyed

    private void Update()
    {
        // Check if the "F" key is pressed to shoot the paper object
        if (Input.GetKeyDown(KeyCode.F))
        {
            // Spawn and shoot the paper object
            SpawnAndShootPaper();
        }
    }

    private void SpawnAndShootPaper()
    {
        // Instantiate the paper prefab at the spawner's position and rotation
        GameObject paperObject = Instantiate(paperPrefab, objectSpawner.position, objectSpawner.rotation);

        // Start moving the paper object
        StartCoroutine(MovePaper(paperObject));

        // Start the coroutine to destroy the paper object after a set time
        StartCoroutine(DestroyPaperAfterTime(paperObject));
    }

    private IEnumerator MovePaper(GameObject paperObject)
    {
        // Calculate the direction to move the paper
        Vector3 moveDirection = objectSpawner.forward; // Forward direction based on the spawner's orientation

        // Move the paper object forward at the specified speed
        while (paperObject != null)
        {
            paperObject.transform.position += moveDirection * moveSpeed * Time.deltaTime;

            yield return null; // Wait for the next frame
        }
    }

    private IEnumerator DestroyPaperAfterTime(GameObject paperObject)
    {
        // Wait for the specified lifetime
        yield return new WaitForSeconds(lifetime);

        // Destroy the paper object after the set time
        Destroy(paperObject);
    }

    // This method is called when a collider enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        // Check if the paper object collides with the Dead Zone
        if (other.CompareTag("Dead Zone"))
        {
            // Destroy the paper object when it hits the Dead Zone
            Destroy(other.gameObject); // Destroy the paper object when it hits the Dead Zone
        }
    }
}
