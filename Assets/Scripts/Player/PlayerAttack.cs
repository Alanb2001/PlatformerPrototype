using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        var healthComponent = other.GetComponent<UnitHealth>();
        
        if (Input.GetMouseButtonDown(0) && other.gameObject.CompareTag("Enemy"))
        {
            if (healthComponent != null)
            {
                healthComponent.TakeDamage(1);
            }
        }
    }
}

