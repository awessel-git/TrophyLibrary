using TrophyLibrary;

namespace TrophyLibraryTests;

[TestClass]
public class TrophyTests
{
    private Trophy _trophy;

    [TestInitialize]
    public void Setup()
    {
        _trophy = new("Olympic Weightlifting", 2003);
    }

    [TestMethod]
    public void Competition_LessThan3_Throws() =>
        Assert.ThrowsException<ArgumentException>(() => _trophy.Competition = "Fo");

    [TestMethod]
    public void Competition_GreaterThanOrEqual3_Updates()
    {
        _trophy.Competition = "NBA";
        Assert.AreEqual("NBA", _trophy.Competition);
        _trophy.Competition = "Football";
        Assert.AreEqual("Football", _trophy.Competition);
    }

    [TestMethod]
    public void Competition_WhenNull_Throws() =>
        Assert.ThrowsException<ArgumentNullException>(() => _trophy.Competition = null);

    [TestMethod]
    public void Constructor_InvalidCompetition_Throws()
    {
        Assert.ThrowsException<ArgumentNullException>(() => new Trophy(null, 2000));
        Assert.ThrowsException<ArgumentException>(() => new Trophy("AB", 2005));
    }

    [TestMethod]
    public void Constructor_InvalidYear_Throws() =>
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Trophy("Wrestling", 1950));

    [TestMethod]
    public void Year_OutOfRange_Throws()
    {
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => _trophy.Year = 1969);
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => _trophy.Year = 2026);
    }

    [TestMethod]
    public void Year_WithinRange_Updates()
    {
        _trophy.Year = 1970;
        Assert.AreEqual(1970, _trophy.Year);
        _trophy.Year = 2025;
        Assert.AreEqual(2025, _trophy.Year);
        _trophy.Year = 2000;
        Assert.AreEqual(2000, _trophy.Year);
    }

    [TestMethod]
    public void ToString_ReturnsExpectedResult() =>
        Assert.AreEqual($"Trophy {_trophy.Id} won in {_trophy.Year} at the {_trophy.Competition} competition", _trophy.ToString());
}
