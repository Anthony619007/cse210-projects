using System;
using System.Collections.Generic;
using System.IO;
using EternalQuest;

/*
========================================================================================
CREATIVITY AND EXCEEDING REQUIREMENTS REPORT:
1. Gamification Leveling System: The program gives the player a title based on their
   accumulated score, such as Bronze Adventurer, Silver Knight, or Eternal Hero.
2. Negative Goals (Bad Habits): The program includes a NegativeGoal class. Recording this
   kind of goal subtracts points, which lets the user track habits they are trying to stop.
========================================================================================
*/

class Program
{
    private static List<Goal> _goals = new List<Goal>();
    private static int _score = 0;

    static void Main(string[] args)
    {
        bool quit = false;

        while (!quit)
        {
            Console.WriteLine();
            DisplayPlayerStatus();
            Console.WriteLine();
            Console.WriteLine("Menu Options:");
            Console.WriteLine("  1. Create New Goal");
            Console.WriteLine("  2. List Goals");
            Console.WriteLine("  3. Save Goals");
            Console.WriteLine("  4. Load Goals");
            Console.WriteLine("  5. Record Event");
            Console.WriteLine("  6. Quit");
            Console.Write("Select a choice from the menu: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateGoalMenu();
                    break;
                case "2":
                    ListGoalDetails();
                    break;
                case "3":
                    SaveGoalsToFile();
                    break;
                case "4":
                    LoadGoalsFromFile();
                    break;
                case "5":
                    RecordGoalEvent();
                    break;
                case "6":
                    quit = true;
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    private static void DisplayPlayerStatus()
    {
        string levelBadge = "Novice Questor (Lvl 1)";
        if (_score >= 5000) levelBadge = "Eternal Hero (Lvl 5)";
        else if (_score >= 3000) levelBadge = "Master Paladin (Lvl 4)";
        else if (_score >= 1500) levelBadge = "Silver Knight (Lvl 3)";
        else if (_score >= 500) levelBadge = "Bronze Adventurer (Lvl 2)";

        Console.WriteLine($"--- Current Score: {_score} points ---");
        Console.WriteLine($"--- Current Title: {levelBadge} ---");
    }

    private static void CreateGoalMenu()
    {
        Console.WriteLine();
        Console.WriteLine("The types of Goals are:");
        Console.WriteLine("  1. Simple Goal");
        Console.WriteLine("  2. Eternal Goal");
        Console.WriteLine("  3. Checklist Goal");
        Console.WriteLine("  4. Negative Goal (Bad Habit)");
        Console.Write("Which type of goal would you like to create? ");

        string typeChoice = Console.ReadLine();

        Console.Write("What is the name of your goal? ");
        string name = Console.ReadLine();
        Console.Write("What is a short description of it? ");
        string description = Console.ReadLine();
        int points = ReadInt("What is the amount of points associated with this goal? ");

        if (typeChoice == "1")
        {
            _goals.Add(new SimpleGoal(name, description, points));
        }
        else if (typeChoice == "2")
        {
            _goals.Add(new EternalGoal(name, description, points));
        }
        else if (typeChoice == "3")
        {
            int target = ReadInt("How many times does this goal need to be accomplished for a bonus? ");
            int bonus = ReadInt("What is the bonus for accomplishing it that many times? ");
            _goals.Add(new ChecklistGoal(name, description, points, target, bonus));
        }
        else if (typeChoice == "4")
        {
            _goals.Add(new NegativeGoal(name, description, points));
        }
        else
        {
            Console.WriteLine("Invalid choice. Goal creation cancelled.");
        }
    }

    private static void ListGoalDetails()
    {
        Console.WriteLine();
        Console.WriteLine("The goals are:");
        if (_goals.Count == 0)
        {
            Console.WriteLine("No goals have been created yet.");
            return;
        }

        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetDetailsString()}");
        }
    }

    private static void RecordGoalEvent()
    {
        ListGoalDetails();
        if (_goals.Count == 0) return;

        int goalIndex = ReadInt("\nWhich goal did you accomplish? ") - 1;

        if (goalIndex >= 0 && goalIndex < _goals.Count)
        {
            int pointsEarned = _goals[goalIndex].RecordEvent();
            _score += pointsEarned;
            Console.WriteLine($"You earned {pointsEarned} points.");
            Console.WriteLine($"You now have {_score} points.");
        }
        else
        {
            Console.WriteLine("Invalid goal selection.");
        }
    }

    private static void SaveGoalsToFile()
    {
        Console.Write("What is the filename for the goal file? ");
        string filename = Console.ReadLine();

        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            outputFile.WriteLine(_score);
            foreach (Goal goal in _goals)
            {
                outputFile.WriteLine(goal.GetStringRepresentation());
            }
        }

        Console.WriteLine("Goals successfully saved.");
    }

    private static void LoadGoalsFromFile()
    {
        Console.Write("What is the filename for the goal file? ");
        string filename = Console.ReadLine();

        if (!File.Exists(filename))
        {
            Console.WriteLine("File not found.");
            return;
        }

        string[] lines = File.ReadAllLines(filename);
        _goals.Clear();
        _score = 0;

        if (lines.Length > 0)
        {
            _score = int.Parse(lines[0]);

            for (int i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i])) continue;

                string[] mainParts = lines[i].Split(':', 2);
                string goalType = mainParts[0];
                string[] dataParts = mainParts[1].Split(',');

                if (goalType == "SimpleGoal")
                {
                    _goals.Add(new SimpleGoal(dataParts[0], dataParts[1], int.Parse(dataParts[2]), bool.Parse(dataParts[3])));
                }
                else if (goalType == "EternalGoal")
                {
                    _goals.Add(new EternalGoal(dataParts[0], dataParts[1], int.Parse(dataParts[2])));
                }
                else if (goalType == "ChecklistGoal")
                {
                    _goals.Add(new ChecklistGoal(dataParts[0], dataParts[1], int.Parse(dataParts[2]),
                        int.Parse(dataParts[4]), int.Parse(dataParts[5]), int.Parse(dataParts[3])));
                }
                else if (goalType == "NegativeGoal")
                {
                    _goals.Add(new NegativeGoal(dataParts[0], dataParts[1], int.Parse(dataParts[2])));
                }
            }
        }

        Console.WriteLine("Goals successfully loaded.");
    }

    private static int ReadInt(string prompt)
    {
        Console.Write(prompt);
        int value;
        while (!int.TryParse(Console.ReadLine(), out value))
        {
            Console.Write("Please enter a whole number: ");
        }

        return value;
    }
}