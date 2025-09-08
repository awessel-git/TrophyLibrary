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
}
