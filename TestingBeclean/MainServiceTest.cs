using SuperbetBeclean.Model;
using SuperbetBeclean.Services;
using SuperbetBeclean.Windows;
namespace TestingBeclean.MainServiceTests
{
    [Apartment(ApartmentState.STA)]
    public class MainServiceTests
    {
        private IMainService mainService;
        private User userToday;
        private User userFiveDaysFromNow;
        [SetUp]
        public void Setup()
        {
            mainService = new MainService();
            userToday = new (1, "player1", 1, 1, "path", 10000, 500, 10, 200, 11, 10, DateTime.Now.Date.AddDays(-1));
            userFiveDaysFromNow = new (2, "player2", 1, 1, "path", 10000, 500, 10, 200, 11, 10, DateTime.Now.Date.AddDays(5));
        }

        [TestCase(0)]
        public void OccupiedIntern_ClickingOnInternTable_ReturnsTrue(int expectedValue)
        {
            int occupiedIntern = mainService.OccupiedIntern();
            Assert.That(occupiedIntern, Is.EqualTo(expectedValue));
        }

        [TestCase(0)]
        public void OccupiedJunior_ClickingOnInternTable_ReturnsTrue(int expectedValue)
        {
            int occupiedIntern = mainService.OccupiedJunior();
            Assert.That(occupiedIntern, Is.EqualTo(expectedValue));
        }

        [TestCase(0)]
        public void OccupiedSenior_ClickingOnInternTable_ReturnsTrue(int expectedValue)
        {
            int occupiedIntern = mainService.OccupiedSenior();
            Assert.That(occupiedIntern, Is.EqualTo(expectedValue));
        }

        [Test]
        public void NewUserLogin_UserGainsOneMoreStreak_ReturnsTrue()
        {
            int userTodayStreak = userToday.UserStreak;
            mainService.NewUserLogin(userToday);
            Assert.That(userToday.UserStreak, Is.EqualTo(userTodayStreak + 1));
        }

        [Test]
        public void NewUserLogin_UserLosesAllStreaks_ReturnsTrue()
        {
            mainService.NewUserLogin(userFiveDaysFromNow);
            Assert.That(userFiveDaysFromNow.UserStreak, Is.EqualTo(1));
        }
    }
}