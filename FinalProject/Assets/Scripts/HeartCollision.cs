using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartCollision : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private AudioSource audioSource;
     
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Boat"))
        {
            if(playerHealth != null)
            {
                playerHealth.RestoreHealth();
            }
        }

        audioSource.Play();

        gameObject.SetActive(false);
    }
}
