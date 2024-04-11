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

    public static void PlayClipLoudly(AudioClip clip, Vector3 position, float volume = 1f)
    {
        GameObject audioObject = new GameObject("TemporaryAudio");
        AudioSource audioSource = audioObject.AddComponent<AudioSource>();

        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();

        Destroy(audioObject, clip.length);
    }
}
