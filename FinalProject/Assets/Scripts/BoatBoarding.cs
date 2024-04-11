using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatBoarding : MonoBehaviour
{
    [SerializeField] private Transform boatCentre;
    [SerializeField] private Transform dockPoint;
    [SerializeField] private GameObject player;
    [SerializeField] private KeyCode teleportKey = KeyCode.B;
    [SerializeField] private GameObject bombSpawn;

    private bool playerOnDock = false;
    private bool playerInRange = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(teleportKey))
        {
            if(playerOnDock)
            {
                TeleportPlayerToBoatCentre();
                bombSpawn.SetActive(true);
            }
            else if(playerInRange)
            {
                TeleportPlayerToDock();
                bombSpawn.SetActive(false);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            playerOnDock = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            playerOnDock = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    private void TeleportPlayerToBoatCentre()
    {
        player.transform.position = boatCentre.position;
    }

    private void TeleportPlayerToDock()
    {
        player.transform.position = dockPoint.position;
    }
}
