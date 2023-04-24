using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimHandlerBB : MonoBehaviour
{
    Animator animator;
    int isSittingHash, canTransitionHash, routeHash;

    [SerializeField] bool isSitting, canTransition;

    [SerializeField] float randomRoute;
    
    //---------------
    void Start()
    {  
        // Get animator component
        animator = GetComponent<Animator>();

        // set animator hash and start logic
        isSittingHash = Animator.StringToHash("isSitting");
        animator.SetBool(isSittingHash, isSitting);

        canTransitionHash = Animator.StringToHash("canTransition");
        animator.SetBool(canTransitionHash, canTransition);


        routeHash = Animator.StringToHash("StartRoute");
        animator.SetInteger(routeHash, 1);

        randomRoute = Mathf.Round(Random.Range(0, 1));

        //Random.Range(0, 1);

    }

    //-----------------
    void Update()
    {
    
    }

    public void TriggerAnimation()
    {
        animator.SetBool(isSittingHash, true);
        Debug.Log("triggered");
    }
}
