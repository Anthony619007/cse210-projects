using System;
using System.Collections.Generic;


class Program
{
    static void Main(string[] args)
    {
        Scripture scripture = GetRandomScripture();

        while (true)
        {
            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine();

            if (scripture.AllWordsHidden())
            {
                break;
            }

            Console.Write("Press enter to continue or type 'quit' to exit: ");
            string input = Console.ReadLine();

            if (string.Equals(input, "quit", StringComparison.OrdinalIgnoreCase))
            {
                break;
            }

            scripture.HideRandomWords(3);
        }
    }

    /// <summary>
    /// Builds a small library of scriptures and returns one at random.
    /// </summary>
    private static Scripture GetRandomScripture()
    {
        List<Scripture> library = new List<Scripture>
        {
            new Scripture(
                new Reference("John", 3, 16),
                "For God so loved the world that he gave his one and only Son, " +
                "that whoever believes in him shall not perish but have eternal life."),

            new Scripture(
                new Reference("Proverbs", 3, 5, 6),
                "Trust in the Lord with all your heart and lean not on your own understanding; " +
                "in all your ways submit to him, and he will make your paths straight."),

            new Scripture(
                new Reference("Philippians", 4, 13),
                "I can do all this through him who gives me strength."),

            new Scripture(
                new Reference("Joshua", 1, 9),
                "Have I not commanded you? Be strong and courageous. Do not be afraid; " +
                "do not be discouraged, for the Lord your God will be with you wherever you go.")
        };

        Random random = new Random();
        int index = random.Next(library.Count);
        return library[index];
    }
}
