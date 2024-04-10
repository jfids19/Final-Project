using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawn : MonoBehaviour
{
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float spawnInterval = 3f;
    [SerializeField] private float spawnRadius = 10f;
    [SerializeField] private float spawnHeight = 30f;
    
    private float timer;

    // Update is called once per frame
    void Update()
    {
       timer += Time.deltaTime;
       if (timer >= spawnInterval)
       {
        SpawnBomb();
        timer = 0f;
       } 
    }

    private void SpawnBomb()
    {
        Vector3 spawnPosition = GetRandomSpawnPosition();

        // Define raycast parameters
        Vector3 raycastOrigin = spawnPosition + Vector3.up * 100f;
        Vector3 raycastDirection = Vector3.down;
        float raycastLength = 1000f;
        
        RaycastHit hit;
        if(Physics.Raycast(raycastOrigin, raycastDirection, out hit, raycastLength, groundLayer))
        {
            // Debug Raycast
            Debug.DrawRay(spawnPosition, Vector3.down * raycastLength, Color.red, 5f);
            
            if(hit.collider.CompareTag("River"))
            {
                //Bomb falls if it hits the river
                GameObject bomb = Instantiate(bombPrefab, spawnPosition, Quaternion.identity);
                Rigidbody bombRb = bomb.GetComponent<Rigidbody>();
                if(bombRb != null)
                {
                    bombRb.velocity = Vector3.down * Random.Range(5f, 10f);
                }

                bomb.AddComponent<Explosive>();
            }
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;
        Vector3 spawnPosition = playerTransform.position + new Vector3(randomCircle.x, spawnHeight, randomCircle.y);
        return spawnPosition;
    }

    private void DestroyBomb(GameObject bomb)
    {
        bomb.SetActive(false);
        Debug.Log("Bomb destroyed before falling");
    }
}
