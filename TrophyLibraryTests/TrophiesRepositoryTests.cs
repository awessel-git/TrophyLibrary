using TrophyLibrary;

namespace TrophyLibraryTests;

[TestClass]
public class TrophiesRepositoryTests
{
    private TrophiesRepository _repository;

    [TestInitialize]
    public void SetupTrophiesRepository()
    {
        _repository = new();
    }

    [TestMethod]
    public void Get_ReturnsAllTrophiesInOriginalOrder()
    {
        Assert.AreEqual(5, _repository.Get().Count);
        Assert.AreEqual("Powerlifting", _repository.Get()[0].Competition);
        Assert.AreEqual("Chess", _repository.Get()[1].Competition);
        Assert.AreEqual("Swimming", _repository.Get()[2].Competition);
        Assert.AreEqual("Dancing", _repository.Get()[3].Competition);
        Assert.AreEqual("Kart racing", _repository.Get()[4].Competition);
    }

    [TestMethod]
    public void Get_FiltersByYearCorrectly()
    {
        List<Trophy> trophiesFrom2003 = _repository.Get(year: 2003);
        Assert.AreEqual(1, trophiesFrom2003.Count);
        List<Trophy> trophiesFrom2005 = _repository.Get(year: 2005);
        Assert.AreEqual(0, trophiesFrom2005.Count);
    }

    [TestMethod]
    public void Get_SortsByCompetitionAscending()
    {
        List<Trophy> sortedByCompetition = _repository.Get(sortBy: TrophiesRepository.SortBy.Competition);
        Assert.AreEqual("Chess", sortedByCompetition[0].Competition);
        Assert.AreEqual("Dancing", sortedByCompetition[1].Competition);
        Assert.AreEqual("Kart racing", sortedByCompetition[2].Competition);
        Assert.AreEqual("Powerlifting", sortedByCompetition[3].Competition);
        Assert.AreEqual("Swimming", sortedByCompetition[4].Competition);
    }

    [TestMethod]
    public void Get_SortsByCompetitionDescending()
    {
        List<Trophy> sortedByCompetition = _repository.Get(sortBy: TrophiesRepository.SortBy.Competition, descending: true);
        Assert.AreEqual("Swimming", sortedByCompetition[0].Competition);
        Assert.AreEqual("Powerlifting", sortedByCompetition[1].Competition);
        Assert.AreEqual("Kart racing", sortedByCompetition[2].Competition);
        Assert.AreEqual("Dancing", sortedByCompetition[3].Competition);
        Assert.AreEqual("Chess", sortedByCompetition[4].Competition);
    }

    [TestMethod]
    public void Get_SortsByYearAscending()
    {
        List<Trophy> sortedByYear = _repository.Get(sortBy: TrophiesRepository.SortBy.Year);
        Assert.AreEqual(1996, sortedByYear[0].Year);
        Assert.AreEqual(1999, sortedByYear[1].Year);
        Assert.AreEqual(2003, sortedByYear[2].Year);
        Assert.AreEqual(2010, sortedByYear[3].Year);
        Assert.AreEqual(2011, sortedByYear[4].Year);
    }

    [TestMethod]
    public void Get_SortsByYearDescending()
    {
        List<Trophy> sortedByYear = _repository.Get(sortBy: TrophiesRepository.SortBy.Year, descending: true);
        Assert.AreEqual(2011, sortedByYear[0].Year);
        Assert.AreEqual(2010, sortedByYear[1].Year);
        Assert.AreEqual(2003, sortedByYear[2].Year);
        Assert.AreEqual(1999, sortedByYear[3].Year);
        Assert.AreEqual(1996, sortedByYear[4].Year);
    }

    [TestMethod]
    public void GetById_Throws_WhenIdIsLessThanOne()
    {
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => _repository.GetById(0));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => _repository.GetById(-1));
    }

    [TestMethod]
    public void GetById_ReturnsNull_WhenTrophyDoesNotExist()
    {
        var trophy1 = _repository.GetById(int.MaxValue);
        var trophy2 = _repository.GetById(_repository.Get().Last().Id + 1); // Id beyond known range

        Assert.IsNull(trophy1);
        Assert.IsNull(trophy2);
    }

    [TestMethod]
    public void GetById_ReturnsTrophy_WhenIdExists()
    {
        int firstTrophyId = _repository.Get().First().Id;

        var trophy = _repository.GetById(firstTrophyId);

        Assert.IsNotNull(trophy);
        Assert.AreEqual(firstTrophyId, trophy.Id);
    }
}