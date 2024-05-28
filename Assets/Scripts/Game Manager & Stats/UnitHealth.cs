using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHealth : MonoBehaviour
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
