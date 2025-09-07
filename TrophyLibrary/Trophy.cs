namespace TrophyLibrary;

public class Trophy
{
    private static int _nextId;
    private string _competition;
    private int _year;

    public int Id { get; private init; }

    public string Competition
    {
        get => _competition;
        set
        {
            if (value is null)
                throw new ArgumentNullException(nameof(Competition), "Competition cannot be null");
            if (value.Length < 3)
                throw new ArgumentException(nameof(Competition), "Competition has to be at least 3 characters");
            _competition = value;
        }
    }

    public int Year
    {
        get => _year;
        set
        {
            if (value < 1970 || value > 2025)
                throw new ArgumentOutOfRangeException(nameof(Year), "Year has to be between 1970 and 2025");
            _year = value;
        }
    }

    public Trophy(string competition, int year)
    {
        Id = ++_nextId;
        Competition = competition;
        Year = year;
    }

    public override string ToString() =>
        $"Trophy {Id} won in {Year} at the {Competition} competition";
}
