using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class ScissorsAttackAI : MonoBehaviour
{
    public Transform[] players;         // Array of player transforms
    public Transform[] randomPoints;    // Array of random walk points
    public float detectionRange = 20f;  // Maximum range to detect a player
    public float rotationSpeed = 5f;    // Speed of turning to face the player
    public float wanderCooldown = 5f;   // Time between picking random wander points
    public float attackCooldown = 2f;   // Cooldown time before the AI can attack again
    public float attackRange = 5f;      // Range within which the AI performs the attack

    private NavMeshAgent navAgent;      // Reference to the NavMeshAgent component
    private Animator animator;          // Reference to the Animator component
    private float wanderTimer;          // Timer for wandering
    private float attackTimer;          // Timer to handle attack cooldown
    public int yPosition = 0;

    void Start()
    {

        navAgent = GetComponent<NavMeshAgent>();   // Get the NavMeshAgent component
        animator = GetComponent<Animator>();       // Get the Animator component
        attackTimer = 0f;                         // Initialize attack cooldown
        wanderTimer = wanderCooldown;             // Initialize the wander timer
        transform.position = new Vector3(0, yPosition, 0);
    }

    void Update()
    {
        if (players.Length == 0 || randomPoints.Length == 0) return; // Exit if no players or random points are assigned

        Transform closestPlayer = GetClosestPlayer();

        if (closestPlayer != null)
        {
            navAgent.SetDestination(closestPlayer.position); // Move towards the player

            // Rotate to face the player before attacking
            RotateToFacePlayer(closestPlayer);

            // Only attack if within range and attack cooldown is finished
            if (Vector3.Distance(transform.position, closestPlayer.position) <= attackRange && attackTimer <= 0f)
            {
                PerformAttack();
                attackTimer = attackCooldown; // Reset attack cooldown
            }
        }
        else
        {
            Wander(); // Wander if no player is in range
        }

        // Update the attack cooldown timer
        if (attackTimer > 0f)
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
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance <= detectionRange && distance < closestDistance)
            {
                closestDistance = distance;
                closest = player;
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

    // Perform the attack by triggering the scissor attack animation
    void PerformAttack()
    {
        animator.SetTrigger("ScissorAttack"); // Trigger the "ScissorAttack" animation
    }

    // Wander to a random point
    void Wander()
    {
        wanderTimer -= Time.deltaTime;

        if (wanderTimer <= 0)
        {
            Transform randomPoint = randomPoints[Random.Range(0, randomPoints.Length)];
            navAgent.SetDestination(randomPoint.position); // Move to a random point
            wanderTimer = wanderCooldown; // Reset the wander timer
        }
    }
}
