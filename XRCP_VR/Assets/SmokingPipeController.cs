using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokingPipeController : MonoBehaviour
{
    [SerializeField] GameObject particleSmoke;
    ParticleSystem ps;
  
    //-----------------
    void Start()
    {
        ps = particleSmoke.GetComponent<ParticleSystem>();
        var em = ps.emission;
        em.enabled = false;
    }

    //----------------------
    public void UpdateSmokeState(bool state)
    {
        if (state)
        {
            var em = ps.emission;
            em.enabled = true;
        }

        if (!state)
        {
            var em = ps.emission;
            em.enabled = false;
        }

    }

    //----------------------
    public void DisableSmoke()
    {

    }

}
