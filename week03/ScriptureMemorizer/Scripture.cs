using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Represents a scripture passage: a Reference plus the text of the
/// passage, broken up into individual Word objects.
/// </summary>
public class Scripture
{
    private readonly Reference _reference;
    private readonly List<Word> _words;
    private static readonly Random _random = new Random();

    /// <summary>
    /// Builds a Scripture from a Reference and the raw text of the verse(s).
    /// The Scripture class is responsible for splitting the text into
    /// individual Word objects, so callers never need to know about that
    /// internal detail.
    /// </summary>
    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = text
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(word => new Word(word))
            .ToList();
    }

    /// <summary>
    /// Hides a given number of randomly chosen words that are not
    /// already hidden. If fewer than that many words remain visible,
    /// all remaining visible words are hidden.
    /// </summary>
    public void HideRandomWords(int numberOfWordsToHide)
    {
        List<Word> visibleWords = _words.Where(w => !w.IsHidden()).ToList();

        int amountToHide = Math.Min(numberOfWordsToHide, visibleWords.Count);

        for (int i = 0; i < amountToHide; i++)
        {
            int randomIndex = _random.Next(visibleWords.Count);
            visibleWords[randomIndex].Hide();
            visibleWords.RemoveAt(randomIndex);
        }
    }

    /// <summary>
    /// Returns true once every word in the scripture is hidden.
    /// </summary>
    public bool AllWordsHidden()
    {
        return _words.All(w => w.IsHidden());
    }

    /// <summary>
    /// Returns the full display text of the scripture: the reference
    /// followed by the words (hidden or visible as appropriate).
    /// </summary>
    public string GetDisplayText()
    {
        string wordsText = string.Join(" ", _words.Select(w => w.GetDisplayText()));
        return $"{_reference.GetDisplayText()}\n{wordsText}";
    }
}
