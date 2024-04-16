using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleController : MonoBehaviour
{
    [SerializeField] private GameObject bossBombSpawner;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(bossBombSpawner != null)
            {
                bossBombSpawner.SetActive(true);
            }
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
        }
    }
}
