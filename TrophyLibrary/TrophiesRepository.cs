namespace TrophyLibrary;

public class TrophiesRepository
{
    public List<Trophy> Trophies { get; set; }

    public TrophiesRepository()
    {
        Trophies = [
            new Trophy("Powerlifting", 2003), new Trophy("Chess", 1999), 
            new Trophy("Swimming", 2011), new Trophy("Dancing", 2010), 
            new Trophy("Kart racing", 1996)];
    }
}
