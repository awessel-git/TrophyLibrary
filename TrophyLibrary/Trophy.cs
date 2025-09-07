namespace TrophyLibrary;

public class Trophy
{
    private static int _nextId;
    private string _competition;
    private int _year;

    public int Id { get; private init; }

    public string Competition
    {
        get { return _competition; }
        set
        {
            if (value?.Length < 3)
                throw new ArgumentException("Competition has to be at least 3 characters");
            else if (value == null)
                throw new ArgumentNullException("Competition cannot be null");
            else
                _competition = value;
        }
    }

    public int Year
    {
        get { return _year; }
        set
        {
            if (value >= 1970 && value <= 2025)
                _year = value;
            else
                throw new ArgumentOutOfRangeException("Year has to be between 1970 and 2025");
        }
    }

    public Trophy(string competition, int year)
    {
        Id = ++_nextId;
        Competition = competition;
        Year = year;
    }

    public override string ToString() => $"Trophy {Id} won in {Year} at the {Competition} competition";
}
