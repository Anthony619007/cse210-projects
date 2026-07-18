using System;

/// <summary>
/// Represents a single word in a scripture. Knows how to hide itself
/// (replacing its letters with underscores) and how to display itself.
/// </summary>
public class Word
{
    private readonly string _text;
    private bool _isHidden;

    public Word(string text)
    {
        _text = text;
        _isHidden = false;
    }

    /// <summary>
    /// Hides this word so that it will display as underscores.
    /// </summary>
    public void Hide()
    {
        _isHidden = true;
    }

    /// <summary>
    /// Reveals this word again (used for the "show hint" stretch feature).
    /// </summary>
    public void Show()
    {
        _isHidden = false;
    }

    public bool IsHidden()
    {
        return _isHidden;
    }

    /// <summary>
    /// Returns the text to display for this word: the word itself if
    /// visible, or a string of underscores matching its length if hidden.
    /// </summary>
    public string GetDisplayText()
    {
        if (_isHidden)
        {
            return new string('_', _text.Length);
        }

        return _text;
    }
}
