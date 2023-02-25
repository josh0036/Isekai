using UnityEngine;

public class ResourceObject : MonoBehaviour
{
    public string resourceName;
    public float maxHealth = 10f;
    public float currentHealth;

    void Start()
    {
        currentHealth = maxHealth; // Set the current health to the maximum health
    }

    public void Mine(float miningPower)
    {
        // Decrease the object's health based on the mining power
        currentHealth -= miningPower;
        if (currentHealth <= 0)
        {
            // Destroy the object if its health reaches zero
            Destroy(gameObject);
        }
    }
}
