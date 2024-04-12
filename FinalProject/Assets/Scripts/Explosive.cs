using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour
{
    [SerializeField] private float explosionRadius = 15f;
    [SerializeField] private float explosionForce = 15f;
    [SerializeField] private float damageAmount = 40f;
    [SerializeField] private AudioClip explosionSound;
    [SerializeField] private AudioClip waterSound;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private GameObject waterSplash;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("River"))
        {
            Explode();
        }
    }

    private void Explode()
    {
        //Play explosion sound
        if(explosionSound != null)
        {
            AudioController.PlayClipLoudly(explosionSound, transform.position, 0.75f);
        }

        //Play water splash
        if(waterSound != null)
        {
            AudioController.PlayClipLoudly(waterSound, transform.position, 0.5f);
        }

        //Instantiate explosion prefab
        if(explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        if(waterSplash != null)
        {
            Instantiate(waterSplash, transform.position, Quaternion.identity);
        }
        
        //Apply explosion force to nearby objects
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach(Collider collider in colliders)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }

            //Check if the collider belongs to the player
            PlayerHealth playerHealth = collider.GetComponent<PlayerHealth>();
            if(playerHealth != null)
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                float damage = Mathf.Lerp(damageAmount, 0f, distance / explosionRadius);
                playerHealth.TakeDamage(Mathf.RoundToInt(damage));
            }
        }
        
        Debug.Log("Bomb exploded on hitting the river");
        Destroy(gameObject);
    }
}
