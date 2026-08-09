public class CyclingActivity : Activity
{
    // For cycling, I store the speed and calculate the other values from it.
    private double _speed;

    public CyclingActivity(DateTime date, int minutes, double speed) : base(date, minutes)
    {
        _speed = speed;
    }

    public override string GetName()
    {
        return "Cycling";
    }

    public override double GetDistance()
    {
        // Distance = speed * hours, so I convert minutes to part of an hour.
        return _speed * Minutes / 60;
    }

    public override double GetSpeed()
    {
        return _speed;
    }

    public override double GetPace()
    {
        // Pace can also come from speed.
        return 60 / GetSpeed();
    }
}