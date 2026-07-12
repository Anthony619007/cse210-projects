using System;

class Program
{
    private static Resume resume = new Resume();
    private static string ownerName = "Anthony Anusiem";
    
    static void Main(string[] args)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔═══════════════════════════════════════════════╗");
        Console.WriteLine("║    📄 ANTHONY'S RESUME BUILDER              ║");
        Console.WriteLine("║      
        Console.WriteLine("╚═══════════════════════════════════════════════╝");
        Console.ResetColor();
        
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"\n👤 Welcome, {ownerName}!");
        Console.ResetColor();
        
        bool running = true;
        
        while (running)
        {
            DisplayMenu();
            string choice = Console.ReadLine();
            
            Console.Clear();
            
            switch (choice)
            {
                case "1":
                    AddPersonalInfo();
                    break;
                case "2":
                    AddEducation();
                    break;
                case "3":
                    AddWorkExperience();
                    break;
                case "4":
                    AddSkills();
                    break;
                case "5":
                    AddProject();
                    break;
                case "6":
                    AddCertification();
                    break;
                case "7":
                    ViewResume();
                    break;
                case "8":
                    ExportResume();
                    break;
                case "9":
                    running = false;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"📄 Thank you, {ownerName}! Your resume is ready.");
                    Console.WriteLine("   Good luck with your career journey! 🚀");
                    Console.ResetColor();
                    Console.WriteLine();
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("❌ Invalid option. Please choose 1-9.");
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
    
    static void DisplayMenu()
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("┌──────────────────────────────────────────────┐");
        Console.WriteLine("│              📋 RESUME BUILDER              │");
        Console.WriteLine("├──────────────────────────────────────────────┤");
        Console.WriteLine($"│  1. 👤 Add Personal Information             │");
        Console.WriteLine($"│  2. 🎓 Add Education                       │");
        Console.WriteLine($"│  3. 💼 Add Work Experience                 │");
        Console.WriteLine($"│  4. 🛠️  Add Skills                         │");
        Console.WriteLine($"│  5. 📁 Add Project                         │");
        Console.WriteLine($"│  6. 🏆 Add Certification                   │");
        Console.WriteLine($"│  7. 👁️  View Resume                        │");
        Console.WriteLine($"│  8. 💾 Export Resume                       │");
        Console.WriteLine($"│  9. 🚪 Quit                               │");
        Console.WriteLine("├──────────────────────────────────────────────┤");
        Console.WriteLine($"│  📊 Completeness: {resume.GetScore():F0}%                     │");
        Console.WriteLine("└──────────────────────────────────────────────┘");
        Console.ResetColor();
        Console.Write("\nChoose an option (1-9): ");
    }
    
    static void AddPersonalInfo()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("👤 PERSONAL INFORMATION");
        Console.WriteLine("═══════════════════════════════════════════════");
        Console.ResetColor();
        
        Console.Write("📧 Email: ");
        string email = Console.ReadLine();
        
        Console.Write("📱 Phone: ");
        string phone = Console.ReadLine();
        
        Console.Write("📍 Location: ");
        string location = Console.ReadLine();
        
        Console.Write("🔗 LinkedIn (optional): ");
        string linkedin = Console.ReadLine();
        
        Console.Write("🌐 Portfolio (optional): ");
        string portfolio = Console.ReadLine();
        
        resume.SetPersonalInfo(ownerName, email, phone, location, linkedin, portfolio);
        
        Console.ForegroundColor = ConsoleColor