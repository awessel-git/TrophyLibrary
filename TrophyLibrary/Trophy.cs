namespace TrophyLibrary;

public class Trophy
{
    public int Id { get; set; } // There should be a better way of doing this

    private string _competition;
    public string Competition
    {
        get { return _competition; }
        set
        {
            if (value.Length < 3)
                throw new ArgumentException("Competition has to be at least 3 characters");
            else if (value == null)
                throw new NullReferenceException("Competition cannot be null");
            else
                _competition = value;
        }
    }

    private int _year;
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

    public Trophy(int id, string competition, int year)
    {
        Id = id;
        Competition = competition;
        Year = year;
    }

    public override string ToString() => $"Trophy {Id} won in {Year}. Description: {Competition}";
}
