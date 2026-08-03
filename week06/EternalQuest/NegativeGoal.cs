using System;

namespace EternalQuest
{
    public class NegativeGoal : Goal
    {
        public NegativeGoal(string name, string description, int points)
            : base(name, description, points)
        {
        }

        public override int RecordEvent()
        {
            int pointsLost = -Math.Abs(_points);
            Console.WriteLine($"Recorded bad habit '{_shortName}'. {pointsLost} points.");
            return pointsLost;
        }

        public override bool IsComplete()
        {
            return false;
        }

        public override string GetDetailsString()
        {
            return $"[ ] {_shortName} ({_description}) -- Bad habit penalty";
        }

        public override string GetStringRepresentation()
        {
            return $"NegativeGoal:{_shortName},{_description},{_points}";
        }
    }
}