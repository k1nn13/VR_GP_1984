using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeSpawnerMinistry : MonoBehaviour
{
    [Header("Tube Reference")] 
    [SerializeField] Transform startPosition;
    [SerializeField] GameObject prefab;

    [Header("UI")]
    [SerializeField] GameObject obj;
    ComputerUI_Controller ui, uiTest;

    [Header("Output")]
    public bool canSpawnTube = false;
    public int tubeCount = 0;


    static PlaySound playSound;
    //static bool canTriggerSound = false;




    //--------------------
    void Start()
    {
        ui = obj.GetComponent<ComputerUI_Controller>();
        playSound = gameObject.GetComponent<PlaySound>();

        tubeCount = 0;
    }

    //----------------------
    public void AudioTrigger()
    {
   
  
    }

    //---------------------
    public void spawnTubeMinistry()
    {
        uiTest = FindObjectOfType<ComputerUI_Controller>();
        playSound.TriggerSound();
        //canTriggerSound = true;

        //obj = Instantiate(prefab, startPosition.transform.position, Quaternion.Euler(-90f, 0f, 0f));
        //var component = obj.AddComponent<CustomComponent>();
        //component.uniqueValue = tubeCount;

        Instantiate(prefab, startPosition.transform.position, Quaternion.Euler(-90f, 0f, 0f));
        //ui.UpdateTubeCount();
        tubeCount += 1;
        //Debug.Log("tube created with unique id: " + obj.GetComponent<CustomComponent>().uniqueValue);
        Debug.Log("tube count is " + tubeCount);

        uiTest.UpdateTubeCount();

        
    }



}
