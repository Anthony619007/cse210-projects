using System;
using System.Threading;

public abstract class Activity
{
    private string _name;
    private string _description;
    private int _duration;

    public Activity(string name, string description)
    {
        _name = name;
        _description = description;
    }

    public void StartActivity()
    {
        Console.WriteLine($"Starting {_name} Activity");
        Console.WriteLine(_description);
        Console.Write("Enter duration in seconds: ");
        _duration = int.Parse(Console.ReadLine());
        Console.WriteLine("Prepare to begin...");
        PauseWithSpinner(3);
    }

    public void EndActivity()
    {
        Console.WriteLine("Well done!");
        PauseWithSpinner(2);
        Console.WriteLine($"You completed {_name} for {_duration} seconds.");
        PauseWithSpinner(3);
    }

    public int GetDuration()
    {
        return _duration;
    }

    protected void PauseWithSpinner(int seconds)
    {
        for (int i = 0; i < seconds; i++)
        {
            foreach (char c in @"|/-\")
            {
                Console.Write(c);
                Thread.Sleep(250);
                Console.Write("\b");
            }
        }
    }

    public abstract void Run();
}