public class RunningActivity : Activity
{
    // For running, I store the distance directly.
    private double _distance;

    public RunningActivity(DateTime date, int minutes, double distance) : base(date, minutes)
    {
        _distance = distance;
    }

    public override string GetName()
    {
        return "Running";
    }

    public override double GetDistance()
    {
        return _distance;
    }

    public override double GetSpeed()
    {
        // Speed = distance / time * 60 because time is in minutes.
        return GetDistance() / Minutes * 60;
    }

    public override double GetPace()
    {
        // Pace means how many minutes it takes for 1 mile.
        return Minutes / GetDistance();
    }
}