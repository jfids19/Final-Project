using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour
{
    [SerializeField] private float explosionRadius = 20f;
    [SerializeField] private float explosionForce = 25f;
    [SerializeField] private float damageAmount = 50f;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("River"))
        {
            Explode();
        }
    }

    private void Explode()
    {
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
