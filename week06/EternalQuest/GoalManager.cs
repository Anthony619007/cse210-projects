using System;
using System.Collections.Generic;
using System.IO;

namespace EternalQuest
{
    public class GoalManager
    {
        private List<Goal> _goals = new List<Goal>();
        private int _score = 0;

        public void Start()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Eternal Quest Menu");
                Console.WriteLine($"Current Score: {_score}");
                Console.WriteLine("1. Create New Goal");
                Console.WriteLine("2. List Goals");
                Console.WriteLine("3. Record Event");
                Console.WriteLine("4. Save Goals");
                Console.WriteLine("5. Load Goals");
                Console.WriteLine("6. Quit");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateGoal();
                        break;
                    case "2":
                        ListGoalDetails();
                        break;
                    case "3":
                        RecordEvent();
                        break;
                    case "4":
                        SaveGoals();
                        break;
                    case "5":
                        LoadGoals();
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        private void CreateGoal()
        {
            Console.WriteLine("Choose goal type: 1=Simple, 2=Eternal, 3=Checklist");
            string type = Console.ReadLine();

            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Write("Description: ");
            string description = Console.ReadLine();
            Console.Write("Points: ");
            int points = int.Parse(Console.ReadLine());

            if (type == "1")
            {
                _goals.Add(new SimpleGoal(name, description, points));
            }
            else if (type == "2")
            {
                _goals.Add(new EternalGoal(name, description, points));
            }
            else if (type == "3")
            {
                Console.Write("Target count: ");
                int target = int.Parse(Console.ReadLine());
                Console.Write("Bonus points: ");
                int bonus = int.Parse(Console.ReadLine());
                _goals.Add(new ChecklistGoal(name, description, points, target, bonus));
            }
        }

        private void ListGoalDetails()
        {
            Console.WriteLine();
            Console.WriteLine("Goals:");
            for (int i = 0; i < _goals.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_goals[i].GetDetailsString()}");
            }
        }

        private void RecordEvent()
        {
            ListGoalDetails();
            Console.Write("Enter goal number to record: ");
            int index = int.Parse(Console.ReadLine()) - 1;
            if (index >= 0 && index < _goals.Count)
            {
                _score += _goals[index].RecordEvent();
            }
        }

        private void SaveGoals()
        {
            using (StreamWriter outputFile = new StreamWriter("goals.txt"))
            {
                outputFile.WriteLine(_score);
                foreach (Goal goal in _goals)
                {
                    outputFile.WriteLine(goal.GetStringRepresentation());
                }
            }

            Console.WriteLine("Goals saved!");
        }

        private void LoadGoals()
        {
            string[] lines = File.ReadAllLines("goals.txt");
            _score = int.Parse(lines[0]);
            _goals.Clear();

            for (int i = 1; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(':', 2);
                string type = parts[0];
                string[] details = parts[1].Split(',');

                if (type == "SimpleGoal")
                {
                    _goals.Add(new SimpleGoal(details[0], details[1], int.Parse(details[2]), bool.Parse(details[3])));
                }
                else if (type == "EternalGoal")
                {
                    _goals.Add(new EternalGoal(details[0], details[1], int.Parse(details[2])));
                }
                else if (type == "ChecklistGoal")
                {
                    _goals.Add(new ChecklistGoal(details[0], details[1], int.Parse(details[2]),
                        int.Parse(details[4]), int.Parse(details[5]), int.Parse(details[3])));
                }
            }

            Console.WriteLine("Goals loaded!");
        }
    }
}