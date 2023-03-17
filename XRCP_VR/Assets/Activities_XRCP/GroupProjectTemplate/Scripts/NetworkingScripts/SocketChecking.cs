using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class SocketChecking : MonoBehaviour
{

    XRSocketInteractor socket;
    public bool socketStare = false;

    void Start()
    {
        socket = GetComponent<XRSocketInteractor>();
    }

    public void socketCheck()
    {

        IXRSelectInteractable objName = socket.GetOldestInteractableSelected();

        Debug.Log(objName.transform.name + " in socket of " + transform.name);
        IXRSelectInteractable screw = socket.GetOldestInteractableSelected();
        GameObject screwCol = screw.transform.gameObject;
        var component = screwCol.GetComponent<CustomComponent>();
        int uniqueValue = component.uniqueValue;
        Debug.Log("tube number " + uniqueValue + " is in the socket");
    }

}