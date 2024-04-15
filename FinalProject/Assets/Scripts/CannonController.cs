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

    private bool isWithinTrigger = false;
    private bool canShoot = true;

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
        }

        Invoke("StartCooldown", 2f);
    }

    void StartCooldown()
    {
        canShoot = false;

        if(platformParent != null)
        {
            platformParent.SetActive(false);
        }

        Invoke("ResetCanShoot", cooldownDuration);
    }

    void ResetCanShoot()
    {
        canShoot = true;

        if(platformParent != null)
        {
            platformParent.SetActive(true);
        }
    }
}
