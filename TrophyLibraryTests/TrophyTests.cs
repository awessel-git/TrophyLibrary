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
    public void Competition_LessThan3_Throws()
    {
        Assert.ThrowsException<ArgumentException>(() => _trophy.Competition = "Fo");
    }

    [TestMethod]
    public void Competition_GreaterThanOrEqual3_Updates()
    {
        _trophy.Competition = "NBA";
        Assert.AreEqual("NBA", _trophy.Competition);
        _trophy.Competition = "Football";
        Assert.AreEqual("Football", _trophy.Competition);
    }

    [TestMethod]
    public void Competition_WhenNull_Throws()
    {
        Assert.ThrowsException<ArgumentNullException>(() => _trophy.Competition = null);
    }
}
