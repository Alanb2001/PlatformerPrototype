using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int currentHealth;
    public int currentMaxHealth;

    private void Start()
    {
        currentHealth = currentMaxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
