using System;
public class Entry
{
    private string prompt;
    private string response;
    private string date;      // Stored as a string (per assignment simplification), format: yyyy-MM-dd HH:mm:ss
    private int moodRating;   // 1 (worst) - 5 (best)

    /// <summary>
    /// Creates a brand new entry, automatically stamped with the current date/time.
    /// </summary>
    public Entry(string prompt, string response, int moodRating)
    {
        this.prompt = prompt;
        this.response = response;
        this.moodRating = moodRating;
        this.date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }

    /// <summary>
    /// Recreates an entry with an explicit date, used when loading entries back
    /// in from a saved file so the original date is preserved.
    /// </summary>
    public Entry(string prompt, string response, string date, int moodRating)
    {
        this.prompt = prompt;
        this.response = response;
        this.date = date;
        this.moodRating = moodRating;
    }

    public string GetPrompt() => prompt;

    public string GetResponse() => response;

    /// <summary>Raw stored date string, e.g. "2026-07-16 14:32:05".</summary>
    public string GetDate() => date;

    public int GetMoodRating() => moodRating;

    /// <summary>A friendly, human-readable version of the date for display.</summary>
    public string GetFormattedDate()
    {
        if (DateTime.TryParse(date, out DateTime parsed))
        {
            return parsed.ToString("dddd, MMMM dd, yyyy h:mm tt");
        }
        return date;
    }

    /// <summary>Renders the mood rating as filled/empty stars, e.g. "★★★☆☆".</summary>
    public string GetMoodStars()
    {
        int filled = Math.Clamp(moodRating, 0, 5);
        return new string('★', filled) + new string('☆', 5 - filled);
    }

    /// <summary>Prints this entry nicely formatted to the console.</summary>
    public void Display(int index)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"\nEntry #{index} — {GetFormattedDate()}   {GetMoodStars()} ({moodRating}/5)");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"❓ {prompt}");
        Console.ResetColor();
        Console.WriteLine($"   {response}");
        Console.WriteLine(new string('-', 50));
    }
}
