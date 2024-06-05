using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
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
            gameObject.GetComponent<PlayerMovement>().enabled = false;
            gameObject.GetComponent<PlayerAttack>().enabled = false;
            
            StartCoroutine(LevelChange());
            print("Player is dead");
        }
    }
    
    IEnumerator LevelChange()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(0); 
    }
}
