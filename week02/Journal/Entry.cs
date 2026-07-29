using System;
using System.Globalization; // Needed for more robust date parsing if we ever go there, but good to include.

public class Entry
{
    private string _promptText; // What was the user asked or what triggered this entry?
    private string _userResponse; // The actual content the user wrote.
    private string _entryDateRaw; // Storing date as a string for simplicity, as per initial requirements.
                                  // Format: "yyyy-MM-dd HH:mm:ss". Could be DateTime, but string works for now.
    private int _moodScore;       // A simple rating from 1 (feeling pretty bad) to 5 (feeling great!).

    /// <param name="prompt">The question or topic given to the user.</param>
    /// <param name="response">The user's written response to the prompt.</param>
    /// <param name="moodRating">The user's mood, on a scale of 1 to 5.</param>
    public Entry(string prompt, string response, int moodRating)
    {
        this._promptText = prompt;
        this._userResponse = response;
        this._moodScore = moodRating;
        // Capture the exact moment this entry was created. It's like a digital timestamp!
        this._entryDateRaw = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
    }

    /// <param name="prompt">The original prompt text.</param>
    /// <param name="response">The original user response.</param>
    /// <param name="date">The original date string for the entry.</param>
    /// <param name="moodRating">The original mood rating.</param>
    public Entry(string prompt, string response, string date, int moodRating)
    {
        this._promptText = prompt;
        this._userResponse = response;
        this._entryDateRaw = date; // Just assign the provided date directly.
        this._moodScore = moodRating;
    }

    public string GetPrompt() => _promptText;
    public string GetResponse() => _userResponse;

    public string GetRawDateString() => _entryDateRaw;

    public int GetMoodRating() => _moodScore;

    public string GetFriendlyFormattedDate()
    {
        // Always good to try and parse dates robustly. InvariantCulture helps with consistency.
        if (DateTime.TryParse(this._entryDateRaw, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
        {
            // A common, friendly date format. Looks much better than the raw string!
            return parsedDate.ToString("dddd, MMMM dd, yyyy h:mm tt");
        }
        return this._entryDateRaw;
    }

    public string GetMoodStarsVisual()
    {
        int actualFilledStars = Math.Clamp(this._moodScore, 0, 5);

        string filled = new string('★', actualFilledStars);
        string empty = new string('☆', 5 - actualFilledStars);
        return filled + empty;
    }

    /// <param name="entryIndex">The sequential number of this entry in the journal list.</param>
    public void DisplayEntryToConsole(int entryIndex)
    {
        // A little visual separator and header for each entry.
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"\total--- Entry
        Console.WriteLine($"Date: {GetFriendlyFormattedDate()}   Mood: {GetMoodStarsVisual()} ({_moodScore}/5)");

        // Display the prompt in a distinct color.
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"Prompt: {_promptText}");

        // Reset color for the response to make it easy on the eyes.
        Console.ResetColor();
        Console.WriteLine($"Response: {_userResponse}");

        // Another separator for readability.
        Console.WriteLine(new string('=', 50)); // Using '=' for a slightly different look.
    }
}