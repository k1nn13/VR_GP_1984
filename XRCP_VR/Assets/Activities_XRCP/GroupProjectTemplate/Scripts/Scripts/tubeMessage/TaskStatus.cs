using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskStatus : MonoBehaviour
{
    public bool taskStatus;
    // Start is called before the first frame update
    void Start()
    {
        taskStatus = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateStatus()
{
    taskStatus = true;
}
}
