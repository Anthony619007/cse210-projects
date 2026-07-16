using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

/// <summary>
/// Manages the full collection of journal Entry objects: adding, displaying,
/// searching, filtering, and saving/loading to a real, Excel-compatible CSV
/// file (commas, quotes, and embedded newlines inside a response are all
/// escaped/parsed correctly).
/// </summary>
public class Journal
{
    private List<Entry> entries;

    public Journal()
    {
        entries = new List<Entry>();
    }

    public void AddEntry(Entry entry) => entries.Add(entry);

    public int GetEntryCount() => entries.Count;

    public List<Entry> GetAllEntries() => entries;

    /// <summary>Prints every entry in the journal to the console.</summary>
    public void DisplayAllEntries()
    {
        for (int i = 0; i < entries.Count; i++)
        {
            entries[i].Display(i + 1);
        }
    }

    /// <summary>Prints only entries whose prompt or response contains the given keyword.</summary>
    public void SearchEntries(string keyword)
    {
        int found = 0;
        for (int i = 0; i < entries.Count; i++)
        {
            Entry e = entries[i];
            if (e.GetPrompt().Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                e.GetResponse().Contains(keyword, StringComparison.OrdinalIgnoreCase))
            {
                found++;
                e.Display(i + 1);
            }
        }

        Console.ForegroundColor = found > 0 ? ConsoleColor.Green : ConsoleColor.Yellow;
        Console.WriteLine(found > 0
            ? $"\n✅ Found {found} matching entr{(found == 1 ? "y" : "ies")}."
            : "\n📭 No entries matched that keyword.");
        Console.ResetColor();
    }

    /// <summary>Prints only entries with the given mood rating (1-5).</summary>
    public void DisplayEntriesByMood(int moodRating)
    {
        int found = 0;
        for (int i = 0; i < entries.Count; i++)
        {
            if (entries[i].GetMoodRating() == moodRating)
            {
                found++;
                entries[i].Display(i + 1);
            }
        }

        Console.ForegroundColor = found > 0 ? ConsoleColor.Green : ConsoleColor.Yellow;
        Console.WriteLine(found > 0
            ? $"\n✅ Found {found} entr{(found == 1 ? "y" : "ies")} with a {moodRating}-star mood."
            : "\n📭 No entries found with that mood rating.");
        Console.ResetColor();
    }

    /// <summary>
    /// Saves the journal as a proper CSV file (openable in Excel), correctly
    /// quoting/escaping any commas, quotation marks, or newlines in the content.
    /// </summary>
    public void SaveToFile(string filename)
    {
        using StreamWriter writer = new StreamWriter(filename, false, Encoding.UTF8);
        writer.WriteLine("Date,Prompt,Response,Mood");
        foreach (Entry e in entries)
        {
            writer.WriteLine(string.Join(",",
                CsvEscape(e.GetDate()),
                CsvEscape(e.GetPrompt()),
                CsvEscape(e.GetResponse()),
                e.GetMoodRating().ToString()));
        }
    }

    /// <summary>
    /// Loads the journal from a CSV file previously written by SaveToFile,
    /// replacing whatever entries are currently in memory.
    /// </summary>
    public void LoadFromFile(string filename)
    {
        string content = File.ReadAllText(filename);
        List<List<string>> rows = ParseCsv(content);

        List<Entry> loaded = new List<Entry>();
        // Skip the header row (row 0), and skip any trailing blank row.
        for (int r = 1; r < rows.Count; r++)
        {
            List<string> row = rows[r];
            if (row.Count == 1 && row[0].Length == 0) continue; // trailing blank line
            if (row.Count < 4) continue; // malformed row, skip defensively

            string date = row[0];
            string prompt = row[1];
            string response = row[2];
            int.TryParse(row[3], out int mood);

            loaded.Add(new Entry(prompt, response, date, mood));
        }

        entries = loaded;
    }

    /// <summary>Escapes a single CSV field per RFC 4180 rules.</summary>
    private string CsvEscape(string field)
    {
        if (field == null) return "";
        if (field.Contains(',') || field.Contains('"') || field.Contains('\n') || field.Contains('\r'))
        {
            return "\"" + field.Replace("\"", "\"\"") + "\"";
        }
        return field;
    }

    /// <summary>
    /// A small hand-rolled CSV parser that correctly handles quoted fields,
    /// escaped quotes (""), and embedded commas/newlines inside a quoted field
    /// (important since a journal response can span multiple lines).
    /// </summary>
    private List<List<string>> ParseCsv(string content)
    {
        List<List<string>> rows = new List<List<string>>();
        List<string> currentRow = new List<string>();
        StringBuilder field = new StringBuilder();
        bool inQuotes = false;
        int i = 0;

        while (i < content.Length)
        {
            char c = content[i];

            if (inQuotes)
            {
                if (c == '"')
                {
                    if (i + 1 < content.Length && content[i + 1] == '"')
                    {
                        field.Append('"');
                        i += 2;
                        continue;
                    }
                    inQuotes = false;
                    i++;
                    continue;
                }
                field.Append(c);
                i++;
                continue;
            }

            switch (c)
            {
                case '"':
                    inQuotes = true;
                    i++;
                    break;
                case ',':
                    currentRow.Add(field.ToString());
                    field.Clear();
                    i++;
                    break;
                case '\r':
                    i++;
                    break;
                case '\n':
                    currentRow.Add(field.ToString());
                    field.Clear();
                    rows.Add(currentRow);
                    currentRow = new List<string>();
                    i++;
                    break;
                default:
                    field.Append(c);
                    i++;
                    break;
            }
        }

        // Flush the final field/row if the file doesn't end with a newline.
        if (field.Length > 0 || currentRow.Count > 0)
        {
            currentRow.Add(field.ToString());
            rows.Add(currentRow);
        }

        return rows;
    }
}
