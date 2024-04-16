using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncySlime : MonoBehaviour
{
    [SerializeField] private float slimeJumpForceMultiplier = 3f;
    [SerializeField] private bool isSmallSlime = false;

    void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            PlayerMovement playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
            if(playerMovement != null)
            {
                if(isSmallSlime && playerMovement.spiritFormCheck())
                {
                    playerMovement.AdjustJumpForce(slimeJumpForceMultiplier);
                }
                else if(!isSmallSlime)
                {
                    playerMovement.AdjustJumpForce(slimeJumpForceMultiplier);
                }
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
