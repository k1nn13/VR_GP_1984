using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableTelescreenBroadcast : MonoBehaviour
{

    static PlaySound sound;
    [SerializeField] GameObject animationHandler;
    [SerializeField] GameObject opacityControl;
    OpacityControl opacityScript;
    AnimHandlerBB animHandler;
    bool canTrigger;
    bool canRetrigger;
    
    //---------------
    void Start()
    {
        sound = GetComponent<PlaySound>();
        opacityScript = opacityControl.GetComponent<OpacityControl>();
        animHandler = animationHandler.GetComponent<AnimHandlerBB>();
        canTrigger = false;
        canRetrigger = true;
    }

    //--------------------
    private void OnEnable()
    {
        EventManager.MessageLaunched += EventUpdateBroadcastState;
    }

    //------------------
    private void OnDisable()
    {
        EventManager.MessageLaunched -= EventUpdateBroadcastState;
    }

    //-----------------------------
    void EventUpdateBroadcastState()
    {
        // allows the animation state to be triggered, called in UI_GameManager
        canTrigger = true;
    }

    //------------------------------
    public void TriggerBroadcastEvent()
    {

        if (canRetrigger)
        {
            if (canTrigger)
            {
                sound.TriggerSound();
                opacityScript.TriggerOpacity(true);
                animHandler.TriggerAnimation();
                canRetrigger=false; // prevent animation scene from retriggering
            }
        }

    }
}
