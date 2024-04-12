using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartCollision : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float spinSpeed = 50f;

    void Update()
    {
        transform.Rotate(0f , 0f, spinSpeed * Time.deltaTime);
    }
     
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
