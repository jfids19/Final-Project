using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerDeath playerDeath = other.GetComponent<PlayerDeath>();

            if(playerDeath != null)
            {
                playerDeath.SetRespawnPoint(transform.position);
                Debug.Log("Checkpoint set!");
            }
        }
    }
}
