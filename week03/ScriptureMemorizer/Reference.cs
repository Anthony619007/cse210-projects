using System;

/// <summary>
/// Represents the reference for a scripture passage, such as "John 3:16"
/// or "Proverbs 3:5-6". Handles both single-verse and verse-range references.
/// </summary>
public class Reference
{
    private readonly string _book;
    private readonly int _chapter;
    private readonly int _startVerse;
    private readonly int _endVerse;

    // Constructor for a single verse, e.g. new Reference("John", 3, 16)
    public Reference(string book, int chapter, int verse)
        : this(book, chapter, verse, verse)
    {
    }

    // Constructor for a verse range, e.g. new Reference("Proverbs", 3, 5, 6)
    public Reference(string book, int chapter, int startVerse, int endVerse)
    {
        _book = book;
        _chapter = chapter;
        _startVerse = startVerse;
        _endVerse = endVerse;
    }

    /// <summary>
    /// Returns the formatted display text for the reference, handling
    /// both single verses and verse ranges.
    /// </summary>
    public string GetDisplayText()
    {
        if (_startVerse == _endVerse)
        {
            return $"{_book} {_chapter}:{_startVerse}";
        }

        return $"{_book} {_chapter}:{_startVerse}-{_endVerse}";
    }
}
