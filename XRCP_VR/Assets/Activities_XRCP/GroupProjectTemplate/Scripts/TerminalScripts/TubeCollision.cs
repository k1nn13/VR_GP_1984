using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeCollision : MonoBehaviour
{
    static PlaySound playSound;
    Rigidbody rb;
    [SerializeField] float mag;

    //-------------------------------------------------
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playSound = GetComponent<PlaySound>();
    }

    private void Update()
    {
        mag = rb.velocity.magnitude;
    }

    //-------------------------------------------------
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag != "Mute")
        {
            playSound.TriggerSound();
        }

    }
}
