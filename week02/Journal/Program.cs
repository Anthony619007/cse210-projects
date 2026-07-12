using System;

class Program
{
    private static Journal journal = new Journal();
    
    static void Main(string[] args)
    {
        Console.WriteLine("╔═══════════════════════════════════════╗");
        Console.WriteLine("║    📓 WELCOME TO YOUR JOURNAL        ║");
        Console.WriteLine("║       Record, Reflect, Grow          ║");
        Console.WriteLine("╚═══════════════════════════════════════╝\n");
        
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
                    running = false;
                    Console.WriteLine("📖 Thank you for journaling! Keep writing and growing. Goodbye!\n");
                    break;
                default:
                    Console.WriteLine("❌ Invalid option. Please choose 1-7.\n");
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
    
    static void DisplayMenu()
    {
        Console.WriteLine("┌─────────────────────────────────────────┐");
        Console.WriteLine("│            📋 MAIN MENU                 │");
        Console.WriteLine("├─────────────────────────────────────────┤");
        Console.WriteLine($"│  1. ✏️  Write a New Entry               │");
        Console.WriteLine($"│  2. 📖 Display All Entries             │");
        Console.WriteLine($"│  3. 💾 Save Journal to File            │");
        Console.WriteLine($"│  4. 📂 Load Journal from File          │");
        Console.WriteLine($"│  5. 🔍 Search Entries                  │");
        Console.WriteLine($"│  6. 😊 View by Mood Rating             │");
        Console.WriteLine($"│  7. 🚪 Quit                           │");
        Console.WriteLine("├─────────────────────────────────────────┤");
        Console.WriteLine($"│  📊 Total Entries: {journal.GetEntryCount(),-4}                  │");
        Console.WriteLine("└─────────────────────────────────────────┘");
        Console.Write("\nChoose an option (1-7): ");
    }
    
    static void WriteNewEntry()
    {
        Console.WriteLine("✏️  NEW JOURNAL ENTRY\n");
        Console.WriteLine("═══════════════════════════════════════════");
        
        PromptGenerator promptGen = new PromptGenerator();
        string prompt = promptGen.GetRandomPrompt();
        
        Console.WriteLine($"\n📌 Today's Prompt:");
        Console.WriteLine($"   \"{prompt}\"\n");
        Console.WriteLine("✍️  Write your response (type 'done' on a new line to finish):");
        
        string response = "";
        string line;
        while ((line = Console.ReadLine()) != "done")
        {
            response += line + "\n";
        }
        response = response.TrimEnd('\n');
        
        if (string.IsNullOrWhiteSpace(response))
        {
            Console.WriteLine("\n⚠️  Entry cannot be empty. Please write something to save.");
            return;
        }
        
        Console.Write("\n😊 Rate your mood on this day (1-5, 5 = Excellent): ");
        int moodRating;
        while (!int.TryParse(Console.ReadLine(), out moodRating) || moodRating < 1 || moodRating > 5)
        {
            Console.Write("❌ Please enter a valid number between 1 and 5: ");
        }
        
        Entry newEntry = new Entry(prompt, response, moodRating);
        journal.AddEntry(newEntry);
        
        Console.WriteLine("\n✅ ENTRY SAVED SUCCESSFULLY!");
        Console.WriteLine($"📅 Date: {newEntry.GetFormattedDate()}");
        Console.WriteLine($"😊 Mood: {newEntry.GetMoodStars()} ({moodRating}/5)");
        Console.WriteLine($"📝 Entry #: {journal.GetEntryCount()}");
    }
    
    static void DisplayJournal()
    {
        Console.WriteLine("📖 YOUR JOURNAL ENTRIES\n");
        Console.WriteLine("═══════════════════════════════════════════");
        
        if (journal.GetEntryCount() == 0)
        {
            Console.WriteLine("\n📭 Your journal is empty. Start writing your first entry!");
            Console.WriteLine("   Go to option 1 to begin.\n");
            return;
        }
        
        journal.DisplayAllEntries();
    }
    
    static void SaveJournal()
    {
        Console.WriteLine("💾 SAVE JOURNAL TO FILE\n");
        Console.WriteLine("═══════════════════════════════════════════");
        
        if (journal.GetEntryCount() == 0)
        {
            Console.WriteLine("\n⚠️  Your journal is empty. Nothing to save.");
            return;
        }
        
        Console.Write("\n📁 Enter filename to save (e.g., my_journal): ");
        string filename = Console.ReadLine();
        
        if (string.IsNullOrWhiteSpace(filename))
        {
            Console.WriteLine("\n❌ Invalid filename. Save cancelled.");
            return;
        }
        
        if (!filename.EndsWith(".csv"))
            filename += ".csv";
        
        if (File.Exists(filename))
        {
            Console.Write($"\n⚠️  File '{filename}' already exists. Overwrite? (y/n): ");
            string confirm = Console.ReadLine().ToLower();
            if (confirm != "y" && confirm != "yes")
            {
                Console.WriteLine("\n❌ Save cancelled.");
                return;
            }
        }
        
        try
        {
            journal.SaveToFile(filename);
            Console.WriteLine($"\n✅ Journal successfully saved to '{filename}'!");
            Console.WriteLine($"📊 {journal.GetEntryCount()} entries saved.");
            Console.WriteLine($"📁 File location: {Path.GetFullPath(filename)}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n❌ Error saving journal: {ex.Message}");
        }
    }
    
    static void LoadJournal()
    {
        Console.WriteLine("📂 LOAD JOURNAL FROM FILE\n");
        Console.WriteLine("═══════════════════════════════════════════");
        
        Console.Write("\n📁 Enter filename to load: ");
        string filename = Console.ReadLine();
        
        if (string.IsNullOrWhiteSpace(filename))
        {
            Console.WriteLine("\n❌ Invalid filename. Load cancelled.");
            return;
        }
        
        if (!filename.EndsWith(".csv"))
            filename += ".csv";
        
        if (!File.Exists(filename))
        {
            Console.WriteLine($"\n❌ File '{filename}' not found.");
            Console.WriteLine("   Please check the filename and try again.");
            return;
        }
        
        try
        {
            // Show file info before loading
            FileInfo fileInfo = new FileInfo(filename);
            Console.WriteLine($"\n📄 File: {filename}");
            Console.WriteLine($"📦 Size: {fileInfo.Length} bytes");
            Console.WriteLine($"📅 Modified: {fileInfo.LastWriteTime}");
            
            Console.Write($"\n⚠️  Loading will replace current journal. Continue? (y/n): ");
            string confirm = Console.ReadLine().ToLower();
            if (confirm != "y" && confirm != "yes")
            {
                Console.WriteLine("\n❌ Load cancelled.");
                return;
            }
            
            journal.LoadFromFile(filename);
            Console.WriteLine($"\n✅ Journal successfully loaded from '{filename}'!");
            Console.WriteLine($"📊 {journal.GetEntryCount()} entries loaded.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n❌ Error loading journal: {ex.Message}");
        }
    }
    
    static void SearchEntries()
    {
        Console.WriteLine("🔍 SEARCH ENTRIES\n");
        Console.WriteLine("═══════════════════════════════════════════");
        
        if (journal.GetEntryCount() == 0)
        {
            Console.WriteLine("\n📭 Your journal is empty. Nothing to search.");
            return;
        }
        
        Console.Write("\n🔎 Enter keyword to search for: ");
        string keyword = Console.ReadLine();
        
        if (string.IsNullOrWhiteSpace(keyword))
        {
            Console.WriteLine("\n❌ No keyword entered. Search cancelled.");
            return;
        }
        
        journal.SearchEntries(keyword);
    }
    
    static void ViewByMood()
    {
        Console.WriteLine("😊 VIEW ENTRIES BY MOOD\n");
        Console.WriteLine("═══════════════════════════════════════════");
        
        if (journal.GetEntryCount() == 0)
        {
            Console.WriteLine("\n📭 Your journal is empty.");
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
            Console.Write("❌ Please enter a number between 1 and 5: ");
        }
        
        journal.DisplayEntriesByMood(moodRating);
    }
}