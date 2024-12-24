using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ScissorsAttackAI : MonoBehaviour
{
    public Transform[] players;         // Array of player transforms
    public float detectionRange = 20f;  // Maximum range to detect a player
    public float rotationSpeed = 5f;    // Speed of turning to face the player
    public float attackRange = 5f;      // Range within which the AI performs the attack
    public float attackCooldown = 2f;   // Cooldown time before the AI can attack again

    private NavMeshAgent navAgent;      // Reference to the NavMeshAgent component
    private Animator animator;          // Reference to the Animator component
    private float attackTimer;          // Timer to handle attack cooldown

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();   // Get the NavMeshAgent component
        animator = GetComponent<Animator>();       // Get the Animator component
        attackTimer = 0f;                         // Initialize attack cooldown
    }

    void Update()
    {
        if (players.Length == 0) return; // Exit if no players are assigned

        Transform closestPlayer = GetClosestPlayer();

        if (closestPlayer != null)
        {
            // Set the destination directly to the player's position
            navAgent.destination = closestPlayer.position;

            // Rotate to face the player
            RotateToFacePlayer(closestPlayer);

            // If within attack range and cooldown is finished, perform the attack
            if (Vector3.Distance(transform.position, closestPlayer.position) <= attackRange && attackTimer <= 0f)
            {
                PerformAttack();
                attackTimer = attackCooldown; // Reset attack cooldown
            }
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

    // Perform the attack by triggering the scissor attack animation
    void PerformAttack()
    {
        animator.SetTrigger("Scissors"); // Trigger the "ScissorAttack" animation
    }
}
