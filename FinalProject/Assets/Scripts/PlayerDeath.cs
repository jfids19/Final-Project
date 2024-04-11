using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private Vector3 respawnPoint;
    public AudioSource deathSoundEffect;
    public AudioSource deathMusic;
    public BoatController boatController;
    public GameObject bombSpawn;

    public Animator playerAnimator;
    public Animator spiritAnimator;
    
    // Start is called before the first frame update
    void Start()
    {
        respawnPoint = transform.position;
    }

    public void SetRespawnPoint(Vector3 newRespawnPoint)
    {
        respawnPoint = newRespawnPoint;
    }
    public void Die()
    {
        deathSoundEffect.Play();
        deathMusic.Play();
       
       transform.position = respawnPoint;
       bombSpawn.SetActive(false);

       if(boatController != null)
       {
            boatController.ResetBoatPosition();
       }
    }
}
