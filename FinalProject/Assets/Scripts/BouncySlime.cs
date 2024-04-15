using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncySlime : MonoBehaviour
{
    [SerializeField] private float slimeJumpForceMultiplier = 3f;

    void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            PlayerMovement playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
            if(playerMovement != null)
            {
                playerMovement.AdjustJumpForce(slimeJumpForceMultiplier);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            PlayerMovement playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
            if(playerMovement != null)
            {
                playerMovement.ResetJumpForce();
            }
        }
    }
}
