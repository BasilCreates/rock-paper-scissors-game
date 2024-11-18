using System.Collections;
using UnityEngine;

public class RockThrower : MonoBehaviour
{
    public GameObject rockPrefab; // Prefab of the rock object to spawn
    public Transform objectSpawner; // Reference to the empty GameObject (spawner)
    public float launchAngle = 45f; // Angle in degrees for the arc
    public float launchSpeed = 10f; // Initial speed of the throw
    public float gravityMultiplier = 1f; // Controls strength of gravity for shallower or steeper arcs

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            // Spawn the rock object and start the coroutine to move it in an arc
            GameObject rockObject = Instantiate(rockPrefab, objectSpawner.position, objectSpawner.rotation);
            StartCoroutine(MoveRockInArc(rockObject, objectSpawner.forward));
        }
    }

    private IEnumerator MoveRockInArc(GameObject rockObject, Vector3 launchDirection)
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
