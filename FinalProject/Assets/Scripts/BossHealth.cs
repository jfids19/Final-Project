using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 100;
    [SerializeField] private Image bossHealthBarFill;

    [SerializeField] private int currentHealth;
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            //Die();
        }
    }
    
    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }

    public int GetCurrentBossHealth()
    {
        return currentHealth;
    }
}
