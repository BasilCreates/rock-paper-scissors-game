using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  // Required for scene management

public class RockPlayerSensing : MonoBehaviour
{
    public Text healthTextAI;           // Reference to the UI Text component to show health
    public int startingHealth = 100;    // The starting health of the AI
    public int currentHealth;           // The current health of the AI
    public int paperDamage = 5;         // The damage for Paper (Rock vs Paper loses)
    public int scissorDamage = 10;      // The damage for Scissors (Rock vs Scissors wins)
    public int rockDamage = 2;          // The damage for Rock (Rock vs Rock draw)

    void Start()
    {
        // Initialize health at the start
        currentHealth = startingHealth;
        UpdateHealthText();
    }

    public void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is a projectile
        if (other.CompareTag("Paper Projectile"))
        {
            print("Player hit by Paper Projectile");
            // Paper vs Rock: Paper beats Rock, so Rock takes full damage
            currentHealth -= paperDamage;
            DestroyProjectile(other);
        }
        else if (other.CompareTag("Rock Projectile"))
        {
            print("Player hit by Rock Projectile");
            // Rock vs Rock: It's a draw, so Rock takes minimal damage
            currentHealth -= rockDamage;
            DestroyProjectile(other);
        }
        else if (other.CompareTag("Scissors Projectile"))
        {
            print("Player hit by Scissors Projectile");
            // Scissors vs Rock: Rock beats Scissors, so Rock takes minimal damage
            currentHealth -= scissorDamage;
            DestroyProjectile(other);
        }

        // Ensure health doesn't drop below 0
        if (currentHealth < 0)
            currentHealth = 0;

        // Update the health UI
        UpdateHealthText();

        // Check if player's health is 0 and trigger "You Lose" scene
        if (currentHealth == 0)
        {
            SceneManager.LoadScene("You Lose");  // Replace with your actual scene name
        }
    }

    private void UpdateHealthText()
    {
        healthTextAI.text = "(Rock) Player: " + currentHealth.ToString();
    }

    private void DestroyProjectile(Collider other)
    {
        Destroy(other.gameObject);
    }
}
