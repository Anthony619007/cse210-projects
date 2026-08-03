using System;

namespace EternalQuest
{
    public class ChecklistGoal : Goal
    {
        private int _amountCompleted;
        private int _target;
        private int _bonus;

        public ChecklistGoal(string name, string description, int points, int target, int bonus)
            : base(name, description, points)
        {
            _amountCompleted = 0;
            _target = target;
            _bonus = bonus;
        }

        public ChecklistGoal(string name, string description, int points, int target, int bonus, int amountCompleted)
            : base(name, description, points)
        {
            _amountCompleted = amountCompleted;
            _target = target;
            _bonus = bonus;
        }

        public override int RecordEvent()
        {
            if (IsComplete())
            {
                Console.WriteLine($"Checklist goal '{_shortName}' is already complete. No points awarded.");
                return 0;
            }

            _amountCompleted++;
            int pointsEarned = _points;

            if (IsComplete())
            {
                pointsEarned += _bonus;
                Console.WriteLine($"Checklist goal '{_shortName}' completed! +{_points} points and +{_bonus} bonus points!");
            }
            else
            {
                Console.WriteLine($"Progress: {_amountCompleted}/{_target}. +{_points} points.");
            }

            return pointsEarned;
        }

        public override bool IsComplete()
        {
            return _amountCompleted >= _target;
        }

        public override string GetStringRepresentation()
        {
            return $"ChecklistGoal:{_shortName},{_description},{_points},{_amountCompleted},{_target},{_bonus}";
        }

        public override string GetDetailsString()
        {
            string checkbox = IsComplete() ? "[X]" : "[ ]";
            return $"{checkbox} {_shortName} ({_description}) -- Completed {_amountCompleted}/{_target} times";
        }
    }
}