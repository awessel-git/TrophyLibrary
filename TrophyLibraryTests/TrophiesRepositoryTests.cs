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

    [TestMethod]
    public void Add_ReturnsSameTrophy_WhenNewTrophyIsAdded()
    {
        var newTrophy = new Trophy("Cycling", 2022);

        var addedTrophy = _repository.Add(newTrophy);

        Assert.AreEqual(newTrophy.Id, addedTrophy.Id);
    }

    [TestMethod]
    public void Add_ThrowsArgumentNullException_WhenTrophyIsNull()
    {
        Assert.ThrowsException<ArgumentNullException>(() => _repository.Add(null));
    }

    [TestMethod]
    public void Add_ThrowsInvalidOperationException_WhenTrophyWithSameIdAlreadyExists()
    {
        var newTrophy = new Trophy("Fencing", 2009);
        _repository.Add(newTrophy);

        Assert.ThrowsException<InvalidOperationException>(() => _repository.Add(newTrophy));
    }

    [TestMethod]
    public void Remove_ExistingId_RemovesAndReturnsTrophy()
    {
        Trophy firstTrophy = _repository.Get().First();
        Trophy? removedTrophy = _repository.Remove(firstTrophy.Id);
        Assert.IsNotNull(removedTrophy);
        Assert.AreEqual(removedTrophy.Id, firstTrophy.Id);
        Assert.AreEqual(4, _repository.Get().Count);
        Assert.AreEqual("Chess", _repository.Get().First().Competition); // Chess should now be the first
    }

    [TestMethod]
    public void Remove_NonExistentId_ReturnsNull()
    {
        var nonExistentTrophy = _repository.Remove(int.MaxValue);
        Assert.IsNull(nonExistentTrophy);
    }

    [TestMethod]
    public void Remove_InvalidId_ThrowsArgumentOutOfRange()
    {
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => _repository.Remove(0));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => _repository.Remove(-1));
    }

    [TestMethod]
    public void Update_ThrowsArgumentNullException_WhenValuesIsNull()
    {
        Trophy trophyToUpdate = _repository.Get().First();
        Assert.ThrowsException<ArgumentNullException>(() => _repository.Update(trophyToUpdate.Id, null));
    }

    [TestMethod]
    public void Update_UpdatesTrophy_WhenIdExists()
    {
        Trophy trophyBeforeUpdate = _repository.Get().First();
        Trophy? trophyValues = _repository.Update(trophyBeforeUpdate.Id, new("MMA", 2009));
        Trophy trophyAfterUpdate = _repository.Get().First();
        Assert.IsNotNull(trophyValues);
        Assert.AreEqual("MMA", trophyAfterUpdate.Competition);
        Assert.AreEqual("MMA", trophyValues.Competition);
        Assert.AreEqual(2009, trophyAfterUpdate.Year);
        Assert.AreEqual(2009, trophyValues.Year);
    }

    [TestMethod]
    public void Update_ReturnsNull_WhenIdDoesNotExist()
    {
        Trophy? noneExistingUpdatedTrophy = _repository.Update(int.MaxValue, new Trophy("Horse Riding", 2016));
        Assert.IsNull(noneExistingUpdatedTrophy);
    }

    [TestMethod]
    public void Update_ThrowsArgumentOutOfRangeException_WhenIdIsLessThanOne()
    {
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => _repository.Update(0, new("Football", 2005)));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => _repository.Update(-1, new("UFC", 1999)));
    }
}