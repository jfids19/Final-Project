using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private float shootForce = 10f;
    [SerializeField] private Transform shootingDirection;
    [SerializeField] private float cooldownDuration = 5f;
    [SerializeField] private GameObject platformParent;
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private Transform heartSpawnLocation;

    private bool isWithinTrigger = false;
    private bool canShoot = true;
    private int shotCount = 0;

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isWithinTrigger = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isWithinTrigger = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
       if(isWithinTrigger && Input.GetKeyDown(KeyCode.R) && canShoot) 
       {
            ShootBomb();
       }
    }

    void ShootBomb()
    {
        if(canShoot)
        {
            GameObject bomb = Instantiate(bombPrefab, transform.position, transform.rotation);
            Rigidbody bombRigidbody = bomb.GetComponent<Rigidbody>();
            if(bombRigidbody != null)
            {
                Vector3 shootDirection = shootingDirection.forward;
            
                bombRigidbody.AddForce(shootDirection * shootForce, ForceMode.Impulse);
            }

            Instantiate(heartPrefab, heartSpawnLocation.position, heartSpawnLocation.rotation);
        }

        shotCount++;
        canShoot = false;

        Invoke("StartCooldown", 2f);
    }

    void StartCooldown()
    {   
        foreach(Transform child in platformParent.transform)
        {
            Collider[] colliders = child.GetComponentsInChildren<Collider>();
            foreach(Collider collider in colliders)
            {
                collider.enabled = false;
            }
        }

        Invoke("ResetCanShoot", cooldownDuration);
    }

    void ResetCanShoot()
    {
        foreach(Transform child in platformParent.transform)
        {
            Collider[] colliders = child.GetComponentsInChildren<Collider>();
            foreach(Collider collider in colliders)
            {
                collider.enabled = true;
            }
        }
        
        canShoot = true;
    }

    public int ShotCounter()
    {
        return shotCount;
    }
}
