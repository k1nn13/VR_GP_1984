using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dynamicText : MonoBehaviour
{
    public Text textContainer;
    public bool socketStateNow;
    // Array of short jokes
    public string[] jokes = new string[20] {
        "Why don’t scientists trust atoms? Because they make up everything.",
        "What do you call an alligator in a vest? An investigator.",
        "I’m reading a book on the history of glue – I just can’t seem to put it down.",
        "Why did the tomato turn red? Because it saw the salad dressing!",
        "What’s orange and sounds like a parrot? A carrot.",
        "Why don’t scientists trust atoms? Because they make up everything.",
    "What do you call a fake noodle? An impasta.",
    "I’m on a whiskey diet. I’ve lost three days already.",
    "Why don’t oysters give to charity? Because they’re shellfish.",
    "What’s the difference between a poorly dressed man on a trampoline and a well-dressed man on a trampoline? Attire.",
    "Why was the math book sad? Because it had too many problems.",
    "What did one hat say to the other? You stay here, I’ll go on ahead.",
    "Why did the coffee file a police report? It got mugged.",
    "Why do seagulls fly over the sea? Because if they flew over the bay, they’d be bagels.",
    "I told my wife she was drawing her eyebrows too high. She looked surprised.",
    "Why don’t eggs tell jokes? Because they’d crack each other up.",
    "Why did the scarecrow win an award? Because he was outstanding in his field.",
    "What’s orange and sounds like a parrot? A carrot.",
    "What do you call an alligator in a vest? An investigator.",
    "I’m reading a book on the history of glue – I just can’t seem to put it down."
    };

    // Variable to store the random joke
    public string currentJoke;
    public SendTCPMessage sendTCP;


    void Start()
    {
        socketStateNow = false;
        // Initialize textContainer with starting text
        textContainer.text = "You have entered the Ministry of Truth. Complete your assigned tasks by inserting the tubes into the P Drive.";
    }

    void Update()
    {
  
    }

    public void updateSocketState(bool state)
    {
        socketStateNow = state;
    }

    public void MessageSubmitted()
    {
        if (socketStateNow == true)
        {
            textContainer.text = "Task completed";
            sendTCP.SendMessage("task complete");

        } else
        {
            textContainer.text = "You have not completed the task. Put the tube in the P Drive.";
        }
       
    }

    public void displayMessage(string input)
    {
        int index = Random.Range(0, jokes.Length);
        currentJoke = jokes[index];
        // textContainer.text = input;
        textContainer.text = currentJoke;
    }

    

}
