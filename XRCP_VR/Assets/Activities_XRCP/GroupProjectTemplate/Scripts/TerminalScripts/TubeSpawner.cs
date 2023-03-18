using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeSpawner : MonoBehaviour
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
    //--------------------
    void Start()
    {
        ui = obj.GetComponent<ComputerUI_Controller>();
        tubeCount = 0;

        //Debug.Log(ui);
    }

    //---------------------
    void Update()
    {
        //------------Debugging using UI--------------------
        //if (ui.sliderValue == 1 && canSpawnTube)
        //{
        //    Instantiate(prefab, startPosition.transform.position, Quaternion.Euler(-90f, 0f, 0f));

        //    canSpawnTube = false;
        //    ui.sliderValue = 0;
        //    canSpawnTube = true;
        //    tubeCount++;
        //}


    }

    public void Test()
    {
   
    }



    public void spawnTube()
    {
        uiTest = FindObjectOfType<ComputerUI_Controller>();

        
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
