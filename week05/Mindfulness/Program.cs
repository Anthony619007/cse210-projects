// Exceeds requirements: Added spinner animation, random prompts, and item counting.
// Could be extended with logging or ensuring unique prompts per session.
using System;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("\nMindfulness Program Menu");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Quit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                new BreathingActivity().Run();
            }
            else if (choice == "2")
            {
                new ReflectionActivity().Run();
            }
            else if (choice == "3")
            {
                new ListingActivity().Run();
            }
            else if (choice == "4")
            {
                Console.WriteLine("Goodbye!");
                break;
            }
            else
            {
                Console.WriteLine("Invalid choice, try again.");
            }
        }
    }
}