using System;
using System.Threading;

public class BreathingActivity : Activity
{
    public BreathingActivity()
        : base("Breathing", "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.") { }

    public override void Run()
    {
        StartActivity();
        int duration = GetDuration();
        int elapsed = 0;

        while (elapsed < duration)
        {
            Console.WriteLine("Breathe in...");
            PauseWithSpinner(3);
            elapsed += 3;

            if (elapsed >= duration) break;

            Console.WriteLine("Breathe out...");
            PauseWithSpinner(3);
            elapsed += 3;
        }

        EndActivity();
    }
}