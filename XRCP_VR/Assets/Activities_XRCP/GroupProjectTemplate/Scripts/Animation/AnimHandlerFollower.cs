using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimHandlerFollower
    : MonoBehaviour
{
    Animator animator;
    int isSittingHash, canTransitionHash;

    [SerializeField] bool isSitting, canTransition;

    [SerializeField] int randomRoute, randomRoute1;

    bool setRoute;
    //---------------
    void Start()
    {

        setRoute = false;
        // Get animator component
        animator = GetComponent<Animator>();

        // set animator hash and start logic
        isSittingHash = Animator.StringToHash("isSitting");
        animator.SetBool(isSittingHash, isSitting);

        canTransitionHash = Animator.StringToHash("canTransition");
        animator.SetBool(canTransitionHash, canTransition);




        randomRoute = (int)Mathf.Round(Random.Range(0, 3));
        animator.SetInteger("StartRoute", randomRoute);



        randomRoute1 = (int)Mathf.Round(Random.Range(0, 4));
        animator.SetInteger("Route", randomRoute1);


        //Random.Range(0, 1);

    }

    //-----------------
    void Update()
    {
        if (!setRoute)
        {
            animator.SetInteger("StartRoute", randomRoute);
            setRoute = true;
        }
    }
}
