static void Main(string[] args)
{
    // Setup console colors, making it look nice
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("╔═══════════════════════════════════════════════╗");
    Console.WriteLine("║    📓 WELCOME TO ANTHONY'S JOURNAL          ║");
    Console.WriteLine("║       Record, Reflect, Inspire, Grow        ║");
    Console.WriteLine("╚═══════════════════════════════════════════════╝");
    Console.ResetColor();
    
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine($"\n👤 Welcome back, {ownerName}!");
    Console.ResetColor();
    
    // TODO: Maybe add a feature to let the user change the name later?
    ShowQuote();
    
    bool isRunning = true; // Renamed from 'running' just to be different
    
    while (isRunning)
    {
        ShowMenu();
        string input = Console.ReadLine();
        
        Console.Clear();
        
        // Using a switch, it's cleaner than a bunch of if-elses
        switch (input)
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
                ShowStats(); // Renamed for brevity
                break;
            case "8":
                isRunning = false;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"📖 Thank you for journaling, {ownerName}!");
                Console.WriteLine("   Keep writing and growing. Goodbye! ✨");
                Console.ResetColor();
                break;
            default:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ Invalid option. Please choose 1-8.");
                Console.ResetColor();
                break;
        }
        
        if (isRunning)
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}

static void ShowQuote()
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
    
    Random r = new Random(); // Just 'r' is fine, it's obvious
    string quote = quotes[r.Next(quotes.Length)];
    
    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.WriteLine($"\n💭 {quote}\n");
    Console.ResetColor();
}

static void ShowMenu()
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
    Console.WriteLine($"│  📊 Entries: {myJournal.GetEntryCount(),-4}  │");
    Console.WriteLine($"│  🔥 Streak: {streakCount,-4} days         │");
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
    
    // I'm using a string builder here, but honestly a string += string is fine for small stuff
    // Wait, let's just use string concat for simplicity
    string response = "";
    string line;
    while ((line = Console.ReadLine()) != "done")
    {
        response += line + "\n";
    }
    
    if (string.IsNullOrWhiteSpace(response))
    {
        Console.WriteLine("\n⚠️  You didn't write anything!");
        return;
    }
    
    Console.Write("\n😊 Rate your mood today (1-5, 5 = Excellent): ");
    int moodRating;
    // This loop is a bit annoying but it works
    while (!int.TryParse(Console.ReadLine(), out moodRating) || moodRating < 1 || moodRating > 5)
    {
        Console.Write("❌ Try again (1-5): ");
    }
    
    Entry newEntry = new Entry(prompt, response, moodRating);
    myJournal.AddEntry(newEntry);
    
    // Streak logic
    DateTime today = DateTime.Now.Date;
    if (lastDate == DateTime.MinValue)
    {
        streakCount = 1;
    }
    else if ((today - lastDate).Days == 1)
    {
        streakCount++;
    }
    else if ((today - lastDate).Days > 1)
    {
        streakCount = 1;
    }
    lastDate = today;
    
    Console.WriteLine("\n✅ Saved!");
}

static void DisplayJournal()
{
    // Just calling the method from the journal class
    myJournal.DisplayAllEntries();
}

static void SaveJournal()
{
    Console.Write("\nFilename: ");
    string name = Console.ReadLine();
    if (!name.EndsWith(".csv")) name += ".csv";
    
    try {
        myJournal.SaveToFile(name);
        Console.WriteLine("Saved.");
    } catch (Exception e) {
        Console.WriteLine("Error: " + e.Message);
    }
}

static void LoadJournal()
{
    Console.Write("\nFilename: ");
    string name = Console.ReadLine();
    if (!name.EndsWith(".csv")) name += ".csv";
    
    // I should probably check if the file exists first, but the class might handle it?
    // Better safe than sorry.
    if (File.Exists(name)) {
        myJournal.LoadFromFile(name);
        streakCount = 0; // Resetting streak because we loaded a new file
        Console.WriteLine("Loaded.");
    } else {
        Console.WriteLine("File not found.");
    }
}

static void SearchEntries()
{
    Console.Write("Search for: ");
    string term = Console.ReadLine();
    myJournal.SearchEntries(term);
}

static void ViewByMood()
{
    Console.Write("Mood (1-5): ");
    int mood = int.Parse(Console.ReadLine());
    myJournal.DisplayEntriesByMood(mood);
}

static void ShowStats()
{
    Console.WriteLine("📊 STATISTICS");
    
    // I'm not using LINQ here because I want to see the loops clearly
    // It helps me debug if something goes wrong
    var all = myJournal.GetAllEntries();
    int total = all.Count;
    
    if (total == 0) {
        Console.WriteLine("No entries yet.");
        return;
    }
    
    double totalMood = 0;
    foreach (var e in all) {
        totalMood += e.GetMoodRating();
    }
    
    double avg = totalMood / total;
    
    Console.WriteLine($"Total: {total}");
    Console.WriteLine($"Average Mood: {avg}");
    
    // Let's just print a simple message
    if (avg > 3) Console.WriteLine("Doing good!");
    else Console.WriteLine("Keep going.");
}