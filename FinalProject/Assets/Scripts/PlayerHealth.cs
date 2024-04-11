using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    [SerializeField] private int currentHealth;
    [SerializeField] private AudioSource deathSoundEffect;

    public PlayerDeath playerDeath;
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        deathSoundEffect.Play();
        if(currentHealth <0)
        {
            Die();
        }
    }

    public void RestoreHealth()
    {
        currentHealth = maxHealth;
    }

    void Die()
    {
        playerDeath.Die();
        currentHealth = maxHealth;
    }
}
