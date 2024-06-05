using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private bool canHit;
    private EnemyHealth healthComponent;

    private void Update()
    {
        if (canHit)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (healthComponent != null)
                {
                    healthComponent.TakeDamage(1);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            healthComponent = other.GetComponent<EnemyHealth>();
            canHit = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            canHit = false;
        }
    }
}

