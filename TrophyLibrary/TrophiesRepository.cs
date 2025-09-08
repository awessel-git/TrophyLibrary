namespace TrophyLibrary;

public class TrophiesRepository
{
    public enum SortBy
    {
        None,
        Year,
        Competition
    }

    private readonly List<Trophy> _trophies;

    public TrophiesRepository()
    {
        _trophies =
        [
            new("Powerlifting", 2003),
            new("Chess", 1999),
            new("Swimming", 2011),
            new("Dancing", 2010),
            new("Kart racing", 1996)
        ];
    }

    /// <summary>
    /// Returns a copy of the trophies.
    /// Can filter by year and sort by year or competition.
    /// </summary>
    /// <param name="year">Optional year filter.</param>
    /// <param name="sortBy">Sorting option.</param>
    /// <param name="descending">Sort descending if true.</param>
    public List<Trophy> Get(int? year = null, SortBy sortBy = SortBy.None, bool descending = false)
    {
        List<Trophy> trophies = _trophies;

        if (year is not null)
            trophies = [.. trophies.Where(t => t.Year == year)];

        if (sortBy == SortBy.Competition)
        {
            if (descending)
                trophies = [.. trophies.OrderByDescending(t => t.Competition)];
            else
                trophies = [.. trophies.OrderBy(t => t.Competition)];
        }
        else if (sortBy == SortBy.Year)
        {
            if (descending)
                trophies = [.. trophies.OrderByDescending(t => t.Year)];
            else
                trophies = [.. trophies.OrderBy(t => t.Year)];
        }

        return [.. trophies.Select(t => new Trophy(t))]; // Use copy constructor
    }
}
