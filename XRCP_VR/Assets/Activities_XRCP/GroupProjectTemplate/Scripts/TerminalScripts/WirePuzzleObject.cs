using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WirePuzzleObject : MonoBehaviour
{
    //--------------------------------------------
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("WirePuzzle"))
        {
            EventManager.OnSpawnWirePuzzleObject();

            Destroy(this.gameObject);
        }
    }
}
