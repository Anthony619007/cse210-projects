using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // I can keep different activity objects in one list
        // because they all inherit from Activity.
        List<Activity> activities = new List<Activity>
        {
            new RunningActivity(new DateTime(2022, 11, 3), 30, 3.0),
            new CyclingActivity(new DateTime(2022, 11, 4), 45, 12.0),
            new SwimmingActivity(new DateTime(2022, 11, 5), 40, 40)
        };

        // This loop prints the summary for each activity.
        // Polymorphism makes the correct class method run automatically.
        foreach (Activity activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}