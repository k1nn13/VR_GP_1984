using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpacityControl : MonoBehaviour
{
    Material m;
    public float current;
    public bool canTrigger;

    //-------------------------------
    void Start()
    {
        m = GetComponent<MeshRenderer>().sharedMaterial;
        m.SetFloat("_Alpha", 1);
    }

    //-----------------------------
    public void TriggerOpacity(bool state)
    {
        canTrigger = state;
        if (canTrigger)
        {
            StartCoroutine(launchCoroutine());
        }
    }  

    //-----------------------------
    IEnumerator launchCoroutine()
    {
        float startPos = 1f;
        float endPos = 0.04f;

        float timeElapsed = 0;
        float lerpTime = 0.5f;

        while (timeElapsed < lerpTime)
        {
            current = Mathf.Lerp(startPos, endPos, (timeElapsed / lerpTime));
            m.SetFloat("_Alpha", current);
            timeElapsed += Time.deltaTime*.05f;
            yield return new WaitForEndOfFrame();

        }


        // trigger event to update game state
        EventManager.OnMessageLaunched(); 
        
        // reverse opacity
        StartCoroutine(reverselaunchCoroutine());
    }


    //-----------------------------
    IEnumerator reverselaunchCoroutine()
    {
        float startPos = 0.04f;
        float endPos = 0.94f;

        float timeElapsed = 0;
        float lerpTime = 0.5f;

        yield return new WaitForSeconds(10.0f);

        while (timeElapsed < lerpTime)
        {
            current = Mathf.Lerp(startPos, endPos, (timeElapsed / lerpTime));
            m.SetFloat("_Alpha", current);
            timeElapsed += Time.deltaTime * .02f;
            yield return new WaitForEndOfFrame();

        }
    }
}
