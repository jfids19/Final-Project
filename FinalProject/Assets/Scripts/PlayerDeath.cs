using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    private Vector3 respawnPoint;
    public AudioSource deathSoundEffect;
    public AudioSource deathMusic;
    public BoatController boatController;
    public FallingPlatforms fallingPlatforms;
    public GameObject bombSpawn;
    [SerializeField] private GameObject[] hearts;
    [SerializeField] private GameObject boatBattleMusic;
    [SerializeField] private GameObject jungleAmbience;
    [SerializeField] private GameObject jungleMusic;


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
       jungleMusic.SetActive(true);
       jungleAmbience.SetActive(true);
       boatBattleMusic.SetActive(false);

       fallingPlatforms.SetPlatformsActive();

       foreach(GameObject heart in hearts)
       {
            heart.SetActive(true);
       }

       if(boatController != null)
       {
            boatController.ResetBoatPosition();
       }
    }
}
