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

    private void Update()
    {

   
  
    }

    //-----------------------------
    IEnumerator launchCoroutine()
    {
        float startPos = 1f;
        float endPos = 0.01f;

        float timeElapsed = 0;
        float lerpTime = 0.5f;

        while (timeElapsed < lerpTime)
        {
            current = Mathf.Lerp(startPos, endPos, (timeElapsed / lerpTime));
            m.SetFloat("_Alpha", current);
            timeElapsed += Time.deltaTime*.08f;
            yield return new WaitForEndOfFrame();

        }

        //current.transform.position = start.transform.position;
    }
}
