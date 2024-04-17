using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleController : MonoBehaviour
{
    [SerializeField] private GameObject bossBombSpawner;
    [SerializeField] private GameObject fireInstructions;
    [SerializeField] private GameObject bossHealthBar;
    [SerializeField] private BossHealth bossHealth;
    [SerializeField] private GameObject bossMusic;
    [SerializeField] private GameObject jungleMusic;
    [SerializeField] private GameObject jungleAmbience;

    void Start()
    {
        fireInstructions.SetActive(false);
        bossHealthBar.SetActive(false);
        bossMusic.SetActive(false);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(bossBombSpawner != null)
            {
                bossBombSpawner.SetActive(true);
            }

            bossHealthBar.SetActive(true);
            bossMusic.SetActive(true);
            jungleMusic.SetActive(false);
            jungleAmbience.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(bossBombSpawner != null)
            {
                bossBombSpawner.SetActive(false);
            }

            bossHealthBar.SetActive(false);

            bossHealth.ResetHealth();
            bossMusic.SetActive(false);
            jungleMusic.SetActive(true);
            jungleAmbience.SetActive(true);
        }
    }
}
