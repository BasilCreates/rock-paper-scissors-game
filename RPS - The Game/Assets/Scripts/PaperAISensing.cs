using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  // Required for scene management

public class PaperAISensing : MonoBehaviour
{
    public Text healthTextAI;           // Reference to the UI Text component to show health
    public int startingHealth = 100;    // The starting health of the AI
    public int currentHealth;           // The current health of the AI
    public int paperDamage = 5;         // The damage for Paper
    public int scissorDamage = 10;      // The damage for Scissors
    public int rockDamage = 2;          // The damage for Rock

    void Start()
    {
        // Initialize health at the start
        currentHealth = startingHealth;
        UpdateHealthText();
    }

    // This function is triggered when another collider enters the AI's collider
    public void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is a projectile
        if (other.CompareTag("Paper Projectile"))
        {
            print("AI hit by Paper Projectile");
            // Paper vs Paper: Paper wins, so AI takes reduced damage
            currentHealth -= paperDamage;  // Adjust damage based on the tag (e.g., paper damage)

            // Update health and destroy the projectile
            DestroyProjectile(other);
        }
        else if (other.CompareTag("Rock Projectile"))
        {
            print("AI hit by Rock Projectile");
            // Rock vs Paper: Rock beats Paper, AI takes full damage
            currentHealth -= rockDamage;

            // Update health and destroy the projectile
            DestroyProjectile(other);
        }
        else if (other.CompareTag("Scissors Projectile"))
        {
            print("AI hit by Scissors Projectile");
            // Scissors vs Paper: Paper beats Scissors, AI takes full damage
            currentHealth -= scissorDamage;

            // Update health and destroy the projectile
        }

        // Ensure health doesn't drop below 0
        if (currentHealth < 0)
            currentHealth = 0;

        // Update the health UI
        UpdateHealthText();

        // Check if AI's health is 0 and trigger "You Win" scene
        if (currentHealth == 0)
        {
            SceneManager.LoadScene("You Win");  // Replace with your actual scene name
        }
    }

    // This function updates the health text UI
    private void UpdateHealthText()
    {
        healthTextAI.text = "(Paper) Computer: " + currentHealth.ToString();
    }

    // Helper function to destroy the projectile after damage is applied
    private void DestroyProjectile(Collider other)
    {
        // Destroy the projectile after it collides with the AI
        Destroy(other.gameObject);
    }
}
