public abstract class Activity
{
    // These values are shared by all activities.
    private DateTime _date;
    private int _minutes;

    // The base constructor sets the common information.
    protected Activity(DateTime date, int minutes)
    {
        _date = date;
        _minutes = minutes;
    }

    // Child classes can use the minutes for their own calculations.
    protected int Minutes => _minutes;

    // Each child class must return its own activity name.
    public abstract string GetName();

    // Each child class must calculate distance.
    public abstract double GetDistance();

    // Each child class must calculate speed.
    public abstract double GetSpeed();

    // Each child class must calculate pace.
    public abstract double GetPace();

    // This makes one summary line for any activity.
    public string GetSummary()
    {
        return $"{_date:dd MMM yyyy} {GetName()} ({_minutes} min): Distance {GetDistance():0.0} miles, Speed {GetSpeed():0.0} mph, Pace: {GetPace():0.0} min per mile";
    }
}