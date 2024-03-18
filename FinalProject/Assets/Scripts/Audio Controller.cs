using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField]private AudioSource normalMusic;
    [SerializeField]private AudioSource eerieMusic;
    [SerializeField]private AudioSource clapping;

    public void TransitionMusic()
    {
        normalMusic.Stop();
        normalMusic.gameObject.SetActive(false);
        clapping.Stop();
        clapping.gameObject.SetActive(false);
        eerieMusic.gameObject.SetActive(true);
        eerieMusic.Play();
    }
}
