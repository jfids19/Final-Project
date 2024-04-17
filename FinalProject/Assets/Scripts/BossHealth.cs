using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 100;
    [SerializeField] private Image bossHealthBarFill;
    [SerializeField] private GameObject damageAudio;

    [SerializeField] private int currentHealth;

    [SerializeField] private GameObject[] bossPlatforms;
    [SerializeField] private GameObject bossScream;
    [SerializeField] private GameObject victoryMusic;
    [SerializeField] private GameObject bossMusic;
    [SerializeField] private GameObject gloopDead;
    [SerializeField] private GameObject victoryScreen;
    [SerializeField] private GameObject bossBombSpawner;
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        damageAudio.SetActive(false);
        bossScream.SetActive(false);
        victoryMusic.SetActive(false);
        gloopDead.SetActive(false);
        victoryScreen.SetActive(false);
    }
    
    void Update()
    {
        FixTransform();
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        StartCoroutine(DamageNoiseTrigger());

        if(currentHealth <= 0)
        {
            Die();
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

    private IEnumerator DamageNoiseTrigger()
    {
        damageAudio.SetActive(true);

        yield return new WaitForSeconds(3f);

        damageAudio.SetActive(false);
    }

    private void Die()
    {
        gloopDead.transform.position = gameObject.transform.position;
        gloopDead.transform.rotation = gameObject.transform.rotation;
        
        foreach(GameObject platform in bossPlatforms)
        {
            Rigidbody rb = platform.GetComponent<Rigidbody>();
            if(rb == null)
            {
                rb = platform.AddComponent<Rigidbody>();
            }
        }

        bossScream.SetActive(true);
        gloopDead.SetActive(true);
        bossMusic.SetActive(false);
        victoryMusic.SetActive(true);
        gameObject.SetActive(false);
        victoryScreen.SetActive(true);
        bossBombSpawner.SetActive(false);
    }

    private void FixTransform()
    {
        transform.rotation = Quaternion.Euler(0f,88f,0f);
        transform.position = new Vector3(558f,358f,775f);
    }
}
