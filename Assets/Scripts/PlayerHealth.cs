using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public Slider healthBar;
    public TextMeshProUGUI healthText;

    void Start()
    {
        currentHealth = maxHealth;
        // Clamp to ensure it doesn't go below zero
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
        UpdateHealthText();
    }

    public void TakeDamage(int damage)
    {
        // Reduce health
        currentHealth -= damage;

        // Update the health text
        UpdateHealthText();
        
        // Update the health bar
        healthBar.value = currentHealth;


        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void Heal(int amount)
    {
        // Increase health
        currentHealth += amount;

        // Clamp to ensure it doesn't exceed max health
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Update the health bar
        healthBar.value = currentHealth;
    }

    void UpdateHealthText()
    {
        healthText.text = $"{currentHealth}";
    }

    private void Die()
    {
       
        SceneManager.LoadScene("Death Screen");

    }
}
