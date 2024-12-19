using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("Detection Settings")]
    public float detectionRadius = 10f;
    public LayerMask targetLayer; // Set to the player or other targetable layers
    private Transform target;

    [Header("Movement Settings")]
    public NavMeshAgent navMeshAgent; // Drag and drop the NavMeshAgent component here

    [Header("Parent Transform")]
    public Transform forwardDirection; // Assign the parent object for AI direction

    void Start()
    {
        if (!navMeshAgent)
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }
    }

    void Update()
    {
        DetectTarget();

        if (target != null)
        {
            TurnToFaceTarget();
            MoveTowardTarget();
        }
        else
        {
            navMeshAgent.ResetPath(); // Stop moving if no target
        }
    }

    private void DetectTarget()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, targetLayer);
        if (hitColliders.Length > 0)
        {
            target = hitColliders[0].transform; // Assume the first detected object is the target
        }
        else
        {
            target = null;
        }
    }

    private void TurnToFaceTarget()
    {
        if (!target) return;

        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        forwardDirection.rotation = Quaternion.Slerp(forwardDirection.rotation, lookRotation, Time.deltaTime * navMeshAgent.angularSpeed);
    }

    private void MoveTowardTarget()
    {
        if (!target) return;

        navMeshAgent.SetDestination(target.position);
    }
}
