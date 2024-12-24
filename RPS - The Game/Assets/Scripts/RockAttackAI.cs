using UnityEngine;
using UnityEngine.AI;

public class RockAttackAI : MonoBehaviour
{
    public Transform[] players;         // Array of player transforms
    public float detectionRange = 20f;  // Maximum range to detect a player
    public float rotationSpeed = 5f;    // Speed of turning to face the player
    public GameObject rockPrefab;       // Prefab of the rock object to spawn
    public Transform objectSpawner;     // Reference to the empty GameObject (spawner)
    public float launchAngle = 45f;     // Angle in degrees for the arc
    public float launchSpeed = 10f;     // Initial speed of the throw
    public float gravityMultiplier = 1f; // Controls strength of gravity for shallower or steeper arcs
    public float attackCooldown = 2f;   // Time in seconds between attacks

    private NavMeshAgent navAgent;      // Reference to the NavMeshAgent component
    private float attackTimer;          // Timer to track cooldown between attacks

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>(); // Get the NavMeshAgent component
        attackTimer = attackCooldown;            // Initialize the attack cooldown timer
    }

    void Update()
    {
        if (players.Length == 0) return; // Exit if no players are assigned

        Transform closestPlayer = GetClosestPlayer();

        if (closestPlayer != null)
        {
            navAgent.SetDestination(closestPlayer.position); // Move towards the player
            RotateToFacePlayer(closestPlayer);               // Rotate to face the player

            // Attack if within range and cooldown is finished
            if (Vector3.Distance(transform.position, closestPlayer.position) <= detectionRange && attackTimer <= 0)
            {
                ThrowRock();
                attackTimer = attackCooldown; // Reset the attack cooldown timer
            }
        }

        // Reduce the attack timer if it's above 0
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }
    }

    // Finds the closest player within the detection range
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

    // Automatically throw a rock at the player
    void ThrowRock()
    {
        GameObject rockObject = Instantiate(rockPrefab, objectSpawner.position, objectSpawner.rotation);
        StartCoroutine(MoveRockInArc(rockObject, objectSpawner.forward)); // Launch rock
    }

    private System.Collections.IEnumerator MoveRockInArc(GameObject rockObject, Vector3 launchDirection)
    {
        // Convert launch angle to radians
        float angleInRadians = launchAngle * Mathf.Deg2Rad;

        // Calculate initial velocity components based on the fixed launch direction
        float initialVx = Mathf.Cos(angleInRadians) * launchSpeed;
        float initialVy = Mathf.Sin(angleInRadians) * launchSpeed;

        // Capture the starting position of the rock
        Vector3 startPosition = objectSpawner.position;
        Vector3 fixedLaunchDirection = launchDirection.normalized;

        // Time-tracking variable
        float time = 0;

        while (rockObject != null)
        {
            // Calculate horizontal and vertical displacements
            float x = initialVx * time; // Horizontal displacement
            float y = initialVy * time - 0.5f * Mathf.Abs(Physics.gravity.y) * gravityMultiplier * time * time; // Vertical with custom gravity

            // Update the position using the start position, fixed direction, and calculated displacements
            rockObject.transform.position = startPosition + fixedLaunchDirection * x + Vector3.up * y;

            // Increment time
            time += Time.deltaTime;

            yield return null; // Wait for the next frame
        }
    }
}
