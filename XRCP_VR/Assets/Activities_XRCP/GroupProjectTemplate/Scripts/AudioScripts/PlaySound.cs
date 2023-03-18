using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlaySound : MonoBehaviour
{
    [SerializeField] AudioClip clip;
    AudioSource source;
    [SerializeField] float volume;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void TriggerSound()
    {
        if (!source.isPlaying)
        {
            source.PlayOneShot(clip, volume);
        }
    }

}
