using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerUI_Controller : MonoBehaviour
{

    [SerializeField] Button btn_0;
    [SerializeField] Slider slider_0;
    public float sliderValue = 0;
    ColorBlock cb;


    //---------------
    void Start()
    {
        btn_0.onClick.AddListener(TaskOnClick);
        cb = slider_0.colors;
        cb.normalColor = Color.red;
        slider_0.colors = cb;
    }

    void TaskOnClick()
    {

        sliderValue ++;
  

        if (sliderValue > 1)
        {
            sliderValue = 0;
        }

        slider_0.value = sliderValue;

        if (sliderValue == 0)
        {
            cb.normalColor = Color.red;
        }
        
        if (sliderValue == 1)
        {
            cb.normalColor = Color.green;
        }

        slider_0.colors = cb;
    }

    //---------------
    void Update()
    {
        
    }
}
