using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientAudioSource : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip[] clips;

    private void Start()
    {
        audioSource.loop = false;
    }

    public void Update()
    {
        if(!audioSource.isPlaying)
        {
            audioSource.clip = clips[Random.Range(0, clips.Length)];
            audioSource.Play();
        }
    }
}
