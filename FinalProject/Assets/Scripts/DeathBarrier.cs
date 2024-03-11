using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBarrier : MonoBehaviour
{
    public PlayerDeath playerDeath;

    private void OnTriggerEnter(Collider other)
    {
        playerDeath.Die();
    }
}
