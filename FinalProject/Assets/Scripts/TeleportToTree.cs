using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportToTree : MonoBehaviour
{
    [SerializeField] private Transform teleportDestination;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            TeleportPlayer(other.transform);
        }
    }

    private void TeleportPlayer(Transform playerTransform)
    {
        playerTransform.position = teleportDestination.position;
    }
}
