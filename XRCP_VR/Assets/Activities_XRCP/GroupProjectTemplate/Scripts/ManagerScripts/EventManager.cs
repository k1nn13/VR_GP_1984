using UnityEngine;
using UnityEngine.Events;
using System;


public class EventManager : MonoBehaviour
{
    //----------------------------------------
    public static event Action MessageLaunched;
    public static void OnMessageLaunched() => MessageLaunched?.Invoke();

    //----------------------------------------
    public static event Action TriggerBroadcast;
    public static void OnTriggerBroadcast() => TriggerBroadcast?.Invoke();

    //----------------------------------------
    public static event Action EventUpdateSocketState;
    public static void OnEventUpdateSocketState() => EventUpdateSocketState?.Invoke();

    //------------------------------------------------
    public static event Action EventSpawnWirePuzzleObject;
    public static void OnSpawnWirePuzzleObject() => EventSpawnWirePuzzleObject?.Invoke();

}
