using System;
using System.Collections.Generic;

/// <summary>
/// Owns the list of journal prompts and hands back a random one each time
/// the user starts a new entry.
/// </summary>
public class PromptGenerator
{
    private List<string> prompts;
    private Random random;

    public PromptGenerator()
    {
        random = new Random();
        prompts = new List<string>
        {
            "Who was the most interesting person I interacted with today?",
            "What was the best part of my day?",
            "How did I see the hand of the Lord in my life today?",
            "What was the strongest emotion I felt today?",
            "If I had one thing I could do over today, what would it be?",
            "What am I most grateful for today?",
            "What is one small win I had today that I might otherwise overlook?",
            "What is something I'm looking forward to tomorrow?"
        };
    }

    public string GetRandomPrompt() => prompts[random.Next(prompts.Count)];
}
