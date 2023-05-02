using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [SerializeField] Color onColor;
    [SerializeField] Color offColor;
    [SerializeField] Color highlightedColor;

    static PlaySoundManager playSoundManager;

    Button btn;
    ColorBlock cb; 
    Vector2 pos;
    Vector2 dir;

    // ---------------
    void Start()
    {
        btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(ButtonClicked);

        playSoundManager = GetComponent<PlaySoundManager>();

        //cb = btn.colors;
        //cb.normalColor = offColor;
        //btn.colors = cb;
    }

     //-----------------------
    void ButtonClicked()
    {
        //Debug.Log("Button has been clicked");
       
        if (btn.interactable)
        {
            btn.interactable = false;
            playSoundManager.TriggerSound(0);
        }

        //cb.normalColor = onColor;
        //btn.colors = cb;
        //KillButton();
    }

    //---------------
    void KillButton()
    {
        //Debug.Log("Button Has Been Destroyed");
        btn.onClick.RemoveAllListeners();
        Destroy(this.gameObject);
    }

    //--------------------------------------------
    private void OnTriggerEnter2D(Collider2D collider)
    {
 
        if (collider.gameObject.CompareTag("PuzzleBorder"))
        {
            //Debug.Log("Border Collision");
            if (!btn.interactable)
            {
                playSoundManager.TriggerSound(1);
                btn.interactable = true;
            }
        }

    }
}
