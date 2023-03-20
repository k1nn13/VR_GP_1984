using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlaySoundManager : MonoBehaviour
{
    [SerializeField] AudioClip clip;
    [SerializeField] AudioClip[] clipArray;
    AudioSource source;
    [SerializeField] float volume;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void TriggerSound(int i)
    {
        if (!source.isPlaying)
        {
            source.PlayOneShot(clipArray[i], volume);
        }
    }

}
