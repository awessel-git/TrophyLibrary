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

    /// <summary>
    /// Finds a trophy by its Id.
    /// </summary>
    /// <param name="id">The Id to look for. Must be greater than 0.</param>
    /// <returns>The trophy if found, or <c>null</c> if not.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="id"/> is less than 1.</exception>
    public Trophy? GetById(int id)
    {
        CheckId(id);

        return _trophies.FirstOrDefault(trophy => trophy.Id == id);
    }

    /// <summary>
    /// Adds a trophy to the repository.
    /// </summary>
    /// <param name="trophy">The trophy to add. Cannot be null.</param>
    /// <returns>The added <see cref="Trophy"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="trophy"/> is null.</exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if a trophy with the same Id already exists in the repository.
    /// </exception>
    public Trophy Add(Trophy trophy)
    {
        ArgumentNullException.ThrowIfNull(trophy);

        // Avoid duplicates
        if (_trophies.Any(t => t.Id == trophy.Id))
            throw new InvalidOperationException($"A trophy with Id {trophy.Id} already exists");

        _trophies.Add(trophy);
        return trophy;
    }

    /// <summary>
    /// Removes a trophy with the given id.
    /// </summary>
    /// <param name="id">The id of the trophy to remove. Must be greater than 0.</param>
    /// <returns>
    /// The removed trophy if found, or <c>null</c> if no trophy with that id exists.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if <paramref name="id"/> is less than 1.
    /// </exception>
    public Trophy? Remove(int id)
    {
        CheckId(id);

        var query = _trophies.FirstOrDefault(trophy => trophy.Id == id);

        if (query is not null)
            _trophies.Remove(query);

        return query;
    }

    /// <summary>
    /// Updates the trophy with the given id using the provided values.
    /// </summary>
    /// <param name="id">The id of the trophy to update. Must be greater than 0.</param>
    /// <param name="values">The new values for competition and year. Cannot be null.</param>
    /// <returns>
    /// The updated trophy if found, or <c>null</c> if no trophy with that id exists.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Thrown if <paramref name="id"/> is less than 1.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="values"/> is null.
    /// </exception>
    public Trophy? Update(int id, Trophy values)
    {
        CheckId(id);
        ArgumentNullException.ThrowIfNull(values);

        Trophy? trophyToUpdate = GetById(id);
        if (trophyToUpdate is not null)
        {
            trophyToUpdate.Competition = values.Competition;
            trophyToUpdate.Year = values.Year;
        }

        return trophyToUpdate;
    }

    private static void CheckId(int id)
    {
        if (id < 1)
            throw new ArgumentOutOfRangeException(nameof(id), "Id has to be bigger than 0");
    }
}
