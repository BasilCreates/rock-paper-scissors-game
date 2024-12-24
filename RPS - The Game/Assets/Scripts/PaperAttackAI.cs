using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PaperAttackAI : MonoBehaviour
{
    public Transform[] players;         // Array of player transforms
    public GameObject paperPrefab;      // Prefab of the paper object to spawn
    public Transform objectSpawner;     // Reference to the spawner
    public float detectionRange = 20f;  // Maximum range to detect a player
    public float rotationSpeed = 5f;    // Speed of turning to face the player
    public float attackDelay = 2f;      // Delay between consecutive attacks
    public float moveSpeed = 10f;       // Speed of the paper projectile
    public float lifetime = 4f;         // Lifetime of the projectile

    private NavMeshAgent navAgent;      // Reference to the NavMeshAgent component
    private bool canAttack = true;      // Flag to control attack delay

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>(); // Get the NavMeshAgent component
    }

    void Update()
    {
        if (players.Length == 0) return; // Exit if no players are assigned

        Transform closestPlayer = GetClosestPlayer();

        if (closestPlayer != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, closestPlayer.position);

            // Rotate to face the player
            RotateToFacePlayer(closestPlayer);

            // Attack if within range and allowed
            if (distanceToPlayer <= detectionRange && canAttack)
            {
                StartCoroutine(AttackPlayer());
            }
        }
    }

    Transform GetClosestPlayer()
    {
        Transform closest = null;
        float closestDistance = detectionRange;

        foreach (Transform player in players)
        {
            if (player.gameObject.activeInHierarchy) // Check if the player is active
            {
                float distance = Vector3.Distance(transform.position, player.position);
                if (distance <= detectionRange && distance < closestDistance)
                {
                    closestDistance = distance;
                    closest = player;
                }
            }
        }

        return closest;
    }

    // Rotate the agent to face the target player
    void RotateToFacePlayer(Transform player)
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        directionToPlayer.y = 0; // Keep the rotation on the horizontal plane

        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    // Attack the player by shooting a paper projectile
    private IEnumerator AttackPlayer()
    {
        canAttack = false; // Prevent multiple attacks during the delay

        // Instantiate the paper prefab at the spawner's position and rotation
        GameObject paperObject = Instantiate(paperPrefab, objectSpawner.position, objectSpawner.rotation);

        // Start moving the paper object
        StartCoroutine(MovePaper(paperObject));

        // Start the coroutine to destroy the paper object after a set time
        StartCoroutine(DestroyPaperAfterTime(paperObject));

        // Wait for the attack delay before allowing another attack
        yield return new WaitForSeconds(attackDelay);

        canAttack = true; // Re-enable attacks after the delay
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
}
