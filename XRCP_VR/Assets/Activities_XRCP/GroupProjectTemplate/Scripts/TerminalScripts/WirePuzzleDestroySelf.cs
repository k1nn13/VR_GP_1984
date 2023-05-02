using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WirePuzzleDestroySelf : MonoBehaviour
{
    public void DestroySelf()
    {
        // launches tube
        EventManager.OnMessageLaunched(); // trigger event to update game state
        Destroy(this.gameObject);
    }
}
