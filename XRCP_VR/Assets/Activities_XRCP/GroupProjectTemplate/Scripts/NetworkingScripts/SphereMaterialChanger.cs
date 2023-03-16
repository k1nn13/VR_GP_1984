using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMaterialChanger : MonoBehaviour
{
    // Create array of materials 
    public Material[] materials;
    //public int aqiIndex = 1;
    //public TCPTestClient TCPtest;

    // The renderer component of the object
    private Renderer objectRenderer;
    // Set index of the current material to zero so it has a material on load
    private int currentMaterialIndex = 0;
 //  public float updateInterval = 3.0f;
  //  public bool colourState = false;
 //   private float nextUpdateTime = 0.0f;
    private int colourNum = 0;

    void Start()
    {
       
        // Get the renderer component of the object
        objectRenderer = GetComponent<Renderer>();

        // Set the initial material to the first one in the array
        objectRenderer.material = materials[0];
    }

    void Update()
    {
     //   Debug.Log("TCP TEST VAL IS " + TCPtest.controlState);

    }

    public void changeColour()
    {
        
        Debug.Log("change colour called");
        if (colourNum > 5)
        {
            Debug.Log("resetting colourNum val");
            colourNum = 0;
        }
        currentMaterialIndex = colourNum;
        objectRenderer.material = materials[currentMaterialIndex];
        colourNum += 1;

    }

}
