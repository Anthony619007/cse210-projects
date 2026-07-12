using System;

class Program
{
    private static Journal journal = new Journal();
    private static string journalOwner = "Anthony Anusiem";
    private static DateTime lastEntryDate = DateTime.MinValue;
    private static int streakDays = 0;
    
    static void Main(string[] args)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔═══════════════════════════════════════════════╗");
        Console.WriteLine("║    📓 WELCOME TO ANTHONY'S JOURNAL          ║");
        Console.WriteLine("║       Record, Reflect, Inspire, Grow        ║");
        Console.WriteLine("╚═══════════════════════════════════════════════╝");
        Console.ResetColor();
        
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"\n👤 Welcome back, {journalOwner}!");
        Console.ResetColor();
        
        // Display inspirational quote
        DisplayInspirationalQuote();
        
        bool running = true;
        
        while (running)
        {
            DisplayMenu();
            string choice = Console.ReadLine();
            
            Console.Clear();
            
            switch (choice)
            {
                case "1":
                    WriteNewEntry();
                    break;
                case "2":
                    DisplayJournal();
                    break;
                case "3":
                    SaveJournal();
                    break;
                case "4":
                    LoadJournal();
                    break;
                case "5":
                    SearchEntries();
                    break;
                case "6":
                    ViewByMood();
                    break;
                case "7":
                    DisplayStatistics();
                    break;
                case "8":
                    running = false;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"📖 Thank you for journaling, {journalOwner}!");
                    Console.WriteLine("   Keep writing and growing. Goodbye! ✨");
                    Console.ResetColor();
                    Console.WriteLine();
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("❌ Invalid option. Please choose 1-8.");
                    Console.ResetColor();
                    break;
            }
            
            if (running)
            {
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
    
    static void DisplayInspirationalQuote()
    {
        string[] quotes = {
            "✨ \"The pen is mightier than the sword.\" - Edward Bulwer-Lytton",
            "🌟 \"Write what should not be forgotten.\" - Isabel Allende",
            "📝 \"Journal writing is a voyage to the interior.\" - Christina Baldwin",
            "💫 \"Fill your paper with the breathings of your heart.\" - William Wordsworth",
            "🌈 \"Writing is the painting of the voice.\" - Voltaire",
            "⭐ \"A journal is a mirror of the soul.\" - Anthony Anusiem",
            "🌅 \"Every day is a new page in the story of your life.\" - Unknown",
            "🎯 \"The act of writing is the act of discovering what you believe.\" - David Hare"
        };
        
        Random rand = new Random();
        string quote = quotes[rand.Next(quotes.Length)];
        
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine($"\n💭 {quote}\n");
        Console.ResetColor();
    }
    
    static void DisplayMenu()
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("┌──────────────────────────────────────────────┐");
        Console.WriteLine("│              📋 MAIN MENU                  │");
        Console.WriteLine("├──────────────────────────────────────────────┤");
        Console.WriteLine($"│  1. ✏️  Write a New Entry                  │");
        Console.WriteLine($"│  2. 📖 Display All Entries                │");
        Console.WriteLine($"│  3. 💾 Save Journal to File               │");
        Console.WriteLine($"│  4. 📂 Load Journal from File             │");
        Console.WriteLine($"│  5. 🔍 Search Entries                     │");
        Console.WriteLine($"│  6. 😊 View by Mood Rating                │");
        Console.WriteLine($"│  7. 📊 Statistics Dashboard               │");
        Console.WriteLine($"│  8. 🚪 Quit                              │");
        Console.WriteLine("├──────────────────────────────────────────────┤");
        Console.WriteLine($"│  📊 Entries: {journal.GetEntryCount(),-4}  │");
        Console.WriteLine($"│  🔥 Streak: {streakDays,-4} days         │");
        Console.WriteLine("└──────────────────────────────────────────────┘");
        Console.ResetColor();
        Console.Write("\nChoose an option (1-8): ");
    }
    
    static void WriteNewEntry()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("✏️  NEW JOURNAL ENTRY");
        Console.WriteLine("═══════════════════════════════════════════════");
        Console.ResetColor();
        
        PromptGenerator promptGen = new PromptGenerator();
        string prompt = promptGen.GetRandomPrompt();
        
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"\n📌 Today's Prompt:");
        Console.ResetColor();
        Console.WriteLine($"   \"{prompt}\"\n");
        
        Console.WriteLine("✍️  Write your response (type 'done' on a new line to finish):");
        Console.ForegroundColor = ConsoleColor.Gray;
        
        string response = "";
        string line;
        while ((line = Console.ReadLine()) != "done")
        {
            response += line + "\n";
        }
        response = response.TrimEnd('\n');
        Console.ResetColor();
        
        if (string.IsNullOrWhiteSpace(response))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n⚠️  Entry cannot be empty. Please write something meaningful.");
            Console.ResetColor();
            return;
        }
        
        Console.Write("\n😊 Rate your mood today (1-5, 5 = Excellent): ");
        int moodRating;
        while (!int.TryParse(Console.ReadLine(), out moodRating) || moodRating < 1 || moodRating > 5)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("❌ Please enter a valid number between 1 and 5: ");
            Console.ResetColor();
        }
        
        Entry newEntry = new Entry(prompt, response, moodRating);
        journal.AddEntry(newEntry);
        
        // Update streak
        DateTime today = DateTime.Now.Date;
        if (lastEntryDate == DateTime.MinValue)
        {
            streakDays = 1;
        }
        else if ((today - lastEntryDate).Days == 1)
        {
            streakDays++;
        }
        else if ((today - lastEntryDate).Days > 1)
        {
            streakDays = 1;
        }
        lastEntryDate = today;
        
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n✅ ENTRY SAVED SUCCESSFULLY!");
        Console.ResetColor();
        Console.WriteLine($"📅 Date: {newEntry.GetFormattedDate()}");
        Console.WriteLine($"😊 Mood: {newEntry.GetMoodStars()} ({moodRating}/5)");
        Console.WriteLine($"📝 Entry #: {journal.GetEntryCount()}");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"🔥 Current Streak: {streakDays} days!");
        Console.ResetColor();
        
        // Show encouragement based on mood
        if (moodRating >= 4)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("🎉 That's wonderful! Keep the positive energy flowing!");
            Console.ResetColor();
        }
        else if (moodRating <= 2)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("💪 Remember, journaling is a great way to process emotions. You've got this!");
            Console.ResetColor();
        }
    }
    
    static void DisplayJournal()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("📖 ANTHONY'S JOURNAL ENTRIES");
        Console.WriteLine("═══════════════════════════════════════════════");
        Console.ResetColor();
        
        if (journal.GetEntryCount() == 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n📭 Your journal is empty.");
            Console.WriteLine("   Start writing your first entry! Option 1 ✏️\n");
            Console.ResetColor();
            return;
        }
        
        journal.DisplayAllEntries();
    }
    
    static void SaveJournal()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("💾 SAVE ANTHONY'S JOURNAL");
        Console.WriteLine("═══════════════════════════════════════════════");
        Console.ResetColor();
        
        if (journal.GetEntryCount() == 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n⚠️  Your journal is empty. Nothing to save.");
            Console.ResetColor();
            return;
        }
        
        Console.Write("\n📁 Enter filename to save (e.g., anthony_journal): ");
        string filename = Console.ReadLine();
        
        if (string.IsNullOrWhiteSpace(filename))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n❌ Invalid filename. Save cancelled.");
            Console.ResetColor();
            return;
        }
        
        if (!filename.EndsWith(".csv"))
            filename += ".csv";
        
        if (File.Exists(filename))
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"\n⚠️  File '{filename}' already exists. Overwrite? (y/n): ");
            Console.ResetColor();
            string confirm = Console.ReadLine().ToLower();
            if (confirm != "y" && confirm != "yes")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n❌ Save cancelled.");
                Console.ResetColor();
                return;
            }
        }
        
        try
        {
            journal.SaveToFile(filename);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n✅ Journal successfully saved to '{filename}'!");
            Console.WriteLine($"📊 {journal.GetEntryCount()} entries saved.");
            Console.WriteLine($"📁 Location: {Path.GetFullPath(filename)}");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n❌ Error saving journal: {ex.Message}");
            Console.ResetColor();
        }
    }
    
    static void LoadJournal()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("📂 LOAD ANTHONY'S JOURNAL");
        Console.WriteLine("═══════════════════════════════════════════════");
        Console.ResetColor();
        
        Console.Write("\n📁 Enter filename to load: ");
        string filename = Console.ReadLine();
        
        if (string.IsNullOrWhiteSpace(filename))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n❌ Invalid filename. Load cancelled.");
            Console.ResetColor();
            return;
        }
        
        if (!filename.EndsWith(".csv"))
            filename += ".csv";
        
        if (!File.Exists(filename))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n❌ File '{filename}' not found.");
            Console.WriteLine("   Please check the filename and try again.");
            Console.ResetColor();
            return;
        }
        
        try
        {
            FileInfo fileInfo = new FileInfo(filename);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"\n📄 File: {filename}");
            Console.WriteLine($"📦 Size: {fileInfo.Length} bytes");
            Console.WriteLine($"📅 Modified: {fileInfo.LastWriteTime}");
            Console.ResetColor();
            
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"\n⚠️  Loading will replace current journal. Continue? (y/n): ");
            Console.ResetColor();
            string confirm = Console.ReadLine().ToLower();
            if (confirm != "y" && confirm != "yes")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n❌ Load cancelled.");
                Console.ResetColor();
                return;
            }
            
            journal.LoadFromFile(filename);
            
            // Reset streak when loading new journal
            streakDays = 0;
            lastEntryDate = DateTime.MinValue;
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n✅ Journal successfully loaded from '{filename}'!");
            Console.WriteLine($"📊 {journal.GetEntryCount()} entries loaded.");
            Console.WriteLine($"👤 Welcome back, {journalOwner}!");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n❌ Error loading journal: {ex.Message}");
            Console.ResetColor();
        }
    }
    
    static void SearchEntries()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("🔍 SEARCH ENTRIES");
        Console.WriteLine("═══════════════════════════════════════════════");
        Console.ResetColor();
        
        if (journal.GetEntryCount() == 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n📭 Your journal is empty. Nothing to search.");
            Console.ResetColor();
            return;
        }
        
        Console.Write("\n🔎 Enter keyword to search for: ");
        string keyword = Console.ReadLine();
        
        if (string.IsNullOrWhiteSpace(keyword))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n❌ No keyword entered. Search cancelled.");
            Console.ResetColor();
            return;
        }
        
        journal.SearchEntries(keyword);
    }
    
    static void ViewByMood()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("😊 VIEW ENTRIES BY MOOD");
        Console.WriteLine("═══════════════════════════════════════════════");
        Console.ResetColor();
        
        if (journal.GetEntryCount() == 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n📭 Your journal is empty.");
            Console.ResetColor();
            return;
        }
        
        Console.WriteLine("\n📊 Mood Rating Guide:");
        Console.WriteLine("   ⭐⭐⭐⭐⭐ (5) - Excellent day! 😄");
        Console.WriteLine("   ⭐⭐⭐⭐  (4) - Good day! 🙂");
        Console.WriteLine("   ⭐⭐⭐   (3) - Okay day 😐");
        Console.WriteLine("   ⭐⭐    (2) - Not great 😕");
        Console.WriteLine("   ⭐     (1) - Terrible day 😞\n");
        
        Console.Write("Enter mood rating to view (1-5): ");
        int moodRating;
        while (!int.TryParse(Console.ReadLine(), out moodRating) || moodRating < 1 || moodRating > 5)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("❌ Please enter a number between 1 and 5: ");
            Console.ResetColor();
        }
        
        journal.DisplayEntriesByMood(moodRating);
    }
    
    static void DisplayStatistics()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("📊 STATISTICS DASHBOARD");
        Console.WriteLine("═══════════════════════════════════════════════");
        Console.ResetColor();
        
        if (journal.GetEntryCount() == 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n📭 No data to display. Start journaling!");
            Console.ResetColor();
            return;
        }
        
        int totalEntries = journal.GetEntryCount();
        var entries = journal.GetAllEntries();
        
        // Mood statistics
        var moodCounts = entries.GroupBy(e => e.GetMoodRating())
                               .Select(g => new { Mood = g.Key, Count = g.Count() })
                               .OrderByDescending(g => g.Mood);
        
        double avgMood = entries.Average(e => e.GetMoodRating());
        
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"\n👤 Journal Owner: Anthony Anusiem");
        Console.WriteLine($"📝 Total Entries: {totalEntries}");
        Console.WriteLine($"🔥 Current Streak: {streakDays} days");
        Console.WriteLine($"📅 Last Entry: {(lastEntryDate != DateTime.MinValue ? lastEntryDate.ToString("yyyy-MM-dd") : "No entries yet")}");
        Console.WriteLine($"😊 Average Mood: {avgMood:F2} / 5.0\n");
        Console.ResetColor();
        
        Console.WriteLine("📊 Mood Distribution:");
        foreach (var group in moodCounts)
        {
            string moodEmoji = group.Mood >= 4 ? "😄" : 
                              group.Mood == 3 ? "😐" : "😞";
            string bar = new string('█', group.Count);
            string padding = new string(' ', 10 - group.Count);
            Console.WriteLine($"   {group.Mood}⭐ {moodEmoji}: {bar}{padding} ({group.Count} entries)");
        }
        
        Console.ForegroundColor = ConsoleColor.Green;
        if (avgMood >= 4.0)
        {
            Console.WriteLine("\n🎉 Excellent! You're having a great week!");
        }
        else if (avgMood >= 3.0)
        {
            Console.WriteLine("\n🙂 Things are going well! Keep it up!");
        }
        else
        {
            Console.WriteLine("\n💪 Remember, journaling helps process challenges. You're doing great!");
        }
        Console.ResetColor();
        
        Console.WriteLine("\n📈 Writing Progress:");
        Console.WriteLine($"   Entries this month: {entries.Count(e => e.GetDate().StartsWith(DateTime.Now.ToString("yyyy-MM")))}");
        Console.WriteLine($"   Total entries: {totalEntries}");
        
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("\n💡 Motivational Message:");
        string[] messages = {
            "Every word you write is a step toward self-discovery.",
            "Your journal is the story of your journey. Keep writing!",
            "Consistency is key. Each entry is a victory!",
            "Remember, progress, not perfection.",
            $"You're creating a legacy, {journalOwner}. Keep going!"
        };
        Random rand = new Random();
        Console.WriteLine($"   {messages[rand.Next(messages.Length)]}");
        Console.ResetColor();
    }
}