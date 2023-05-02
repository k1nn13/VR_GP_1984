using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlaySound : MonoBehaviour
{
    AudioSource source;
    [SerializeField] AudioClip clip;
    [SerializeField] float volume;


    //-----------------
    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    //-----------------
    public void TriggerSound()
    {
        if (source != null)
        {
            if (!source.isPlaying)
            {

                source.PlayOneShot(clip, volume);
            }
        }

    }

}
