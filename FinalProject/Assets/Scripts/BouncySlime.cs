using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncySlime : MonoBehaviour
{
    [SerializeField] private float baseBounciness = 0.5f;
    [SerializeField] private float bounceIncreaseAmount = 0.1f;

    private Collider slimeCollider;
    private int bounceCount = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        slimeCollider = GetComponent<Collider>();
        slimeCollider.material.bounciness = baseBounciness;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            bounceCount++;
            slimeCollider.material.bounciness += bounceIncreaseAmount;
        }
    }
}
