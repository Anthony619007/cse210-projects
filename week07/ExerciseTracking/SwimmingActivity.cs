public class SwimmingActivity : Activity
{
    // For swimming, I store the number of laps.
    private int _laps;

    public SwimmingActivity(DateTime date, int minutes, int laps) : base(date, minutes)
    {
        _laps = laps;
    }

    public override string GetName()
    {
        return "Swimming";
    }

    public override double GetDistance()
    {
        // Each lap is 50 meters, then I convert to miles.
        return _laps * 50 / 1000.0 * 0.62;
    }

    public override double GetSpeed()
    {
        // Speed uses the distance I calculated above.
        return GetDistance() / Minutes * 60;
    }

    public override double GetPace()
    {
        // Pace is minutes divided by distance.
        return Minutes / GetDistance();
    }
}