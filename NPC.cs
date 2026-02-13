using System;
using System.Collections.Generic;

class NPC
{
    public string Name { get; }
    public string Greeting { get; }

    public List<DialogueOption> Options { get; }



    public NPC(string name, string greeting)
    {
        Name = name;
        Greeting = greeting;
        Options = new List<DialogueOption>();
    }


    public void AddOption(string optionText, Action<Player> effect)
    {
        Options.Add(new DialogueOption(optionText, effect));
    }
  
}

class DialogueOption
{
    public string Text { get; private set; }
    public Action<Player> Effect { get; }
    public bool Used { get; private set; } = false;
 
    public DialogueOption(string text, Action<Player> effect)
    {
        Text = text;
        Effect = effect;
    }

public void MarkUsed(string newText = null)
    {
        Used = true;
        if (newText != null)
        {
            Text = newText;
        }
    }

}
