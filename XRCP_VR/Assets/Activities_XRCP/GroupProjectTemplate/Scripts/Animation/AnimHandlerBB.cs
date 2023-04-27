using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimHandlerBB : MonoBehaviour
{
    Animator animator;
    int isSittingHash, canTransitionHash;

    [SerializeField] bool isSitting, canTransition;


    
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
