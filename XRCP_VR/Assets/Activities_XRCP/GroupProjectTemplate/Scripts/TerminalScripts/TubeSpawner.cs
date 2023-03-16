using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeSpawner : MonoBehaviour
{
    [SerializeField] Transform startPosition;
    ComputerUI_Controller ui;
    [SerializeField] GameObject obj;
    [SerializeField] GameObject prefab;

    public bool canSpawnTube = true;
    //--------------------
    void Start()
    {
        ui = obj.GetComponent<ComputerUI_Controller>();
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
