using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour
{
    public bool myBool = false;

    public void OnButtonClick()
    {
        myBool = true;
    }
}