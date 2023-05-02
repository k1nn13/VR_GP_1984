using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WirePuzzleObjectSpawner : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] Transform startPosition;

    static PlaySoundManager playSoundManager;
    bool canPlay = true;

    //------------------
    private void Start()
    {
        Instantiate(prefab, startPosition.transform.position, startPosition.transform.rotation);

        playSoundManager = GetComponent<PlaySoundManager>();

        //if (playSoundManager != null)
        //{
        //    playSoundManager.TriggerSound(1);
        //}

    }

    //------------------
    private void Update()
    {
        if (canPlay)
        {
            playSoundManager.TriggerSound(1);
            canPlay = false;
        }
    }

    //--------------------
    private void OnEnable()
    {
        EventManager.EventSpawnWirePuzzleObject += EventSpawnWirePuzzleObject;
    }

    //------------------
    private void OnDisable()
    {
        EventManager.EventSpawnWirePuzzleObject -= EventSpawnWirePuzzleObject;
    }

    //------------------
    public void EventSpawnWirePuzzleObject()
    {
        Instantiate(prefab, startPosition.transform.position, startPosition.transform.rotation);
        playSoundManager.TriggerSound(0);
    }
}
