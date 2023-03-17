using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeSpawnerMinistry : MonoBehaviour
{
    [SerializeField] Transform startPosition;
    ComputerUI_Controller ui;
    [SerializeField] GameObject obj;
    [SerializeField] GameObject prefab;

    public bool canSpawnTube = true;
    public int tubeCount = 0;
    //--------------------
    void Start()
    {
        ui = obj.GetComponent<ComputerUI_Controller>();
        
    }
    public void spawnTubeMinistry()
    {
        obj = Instantiate(prefab, startPosition.transform.position, Quaternion.Euler(-90f, 0f, 0f));
        var component = obj.AddComponent<CustomComponent>();
        component.uniqueValue = tubeCount;
        tubeCount += 1;
        Debug.Log("tube created with unique id: " + obj.GetComponent<CustomComponent>().uniqueValue);
        Debug.Log("tube count is " + tubeCount);
    }


    //---------------------
    void Update()
    {
        if (ui.sliderValue == 1 && canSpawnTube)
        {
            Instantiate(prefab, startPosition.transform.position, Quaternion.Euler(-90f, 0f, 0f));

            canSpawnTube = false;
        }
    }




}
