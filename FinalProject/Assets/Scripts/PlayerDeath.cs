using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private Vector3 respawnPoint;
    
    // Start is called before the first frame update
    void Start()
    {
        respawnPoint = transform.position;
    }

    public void SetRespawnPoint(Vector3 newRespawnPoint)
    {
        respawnPoint = newRespawnPoint;
    }
    public void Die()
    {
        transform.position = respawnPoint;
    }
}
