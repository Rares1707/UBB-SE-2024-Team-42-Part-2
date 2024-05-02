using NUnit.Framework;
using Moq;
using SuperbetBeclean.Model;
using SuperbetBeclean.Services;
using SuperbetBeclean.Windows;
using SuperbetBeclean;
using System.Data.SqlClient;
using NUnit.Framework.Legacy;

namespace TestingBeclean
{
    [TestFixture]
    public class TableServiceTest
    {
        private TableService _tableService;
        private Mock<CardDeck> _mockCardDeck;
        private Mock<DataBaseService> _mockDatabaseService;
        private Mock<SqlConnection> _mockSqlConnection;
        private Mock<HandRankCalculator> _mockRankCalculator;
        private Mock<MainService> _mockMainService;

        [SetUp]
        public void Setup()
        {
            _mockRankCalculator = new Mock<HandRankCalculator>();
            _mockCardDeck = new Mock<CardDeck>();
            _mockDatabaseService = new Mock<DataBaseService>();
            _mockSqlConnection = new Mock<SqlConnection>();
            _tableService = new TableService(100, 5, 10, "TestTable", _mockDatabaseService.Object); // Pass null for database service
            //_tableService.RankCalculator = _mockRankCalculator.Object;
            _mockMainService = new Mock<MainService>();
        }

        [Test]
        public void GenerateCard_ShouldCallGetRandomCardAndRemoveIt()
        {
            // Arrange
            var expectedCard = new PlayingCard(PlayingCard.CARD_VALUES["A"], PlayingCard.HEART_SYMBOL); // Example card
            _mockCardDeck.Setup(deck => deck.GetDeckSize()).Returns(1); // Mock deck size
            _mockCardDeck.Setup(deck => deck.GetCardFromIndex(It.IsAny<int>())).Returns(expectedCard); // Mock getting card from index
            _mockCardDeck.Setup(deck => deck.RemoveCardFromIndex(It.IsAny<int>())); // Mock removing card from index

            // Act
            var result = _tableService.GenerateCard();

            // Assert
            Assert.That(expectedCard, Is.EqualTo(result)); // Ensure the correct card is returned
            _mockCardDeck.Verify(deck => deck.GetCardFromIndex(It.IsAny<int>()), Times.Once); // Verify GetCardFromIndex was called once
            _mockCardDeck.Verify(deck => deck.RemoveCardFromIndex(It.IsAny<int>()), Times.Once); // Verify RemoveCardFromIndex was called once
        }

        [Test]
        public void DetermineMaxHand_ShouldReturnMaxHandValue()
        {
            // Arrange
            var possibleCards = new List<PlayingCard>
            {
                new PlayingCard(PlayingCard.CARD_VALUES["A"], PlayingCard.HEART_SYMBOL),
                new PlayingCard(PlayingCard.CARD_VALUES["K"],PlayingCard.HEART_SYMBOL),
                // Add more cards as needed
            };
            var expectedMaxHandValue = new Tuple<int, int>(10, 1); // Example expected max hand value
            _mockRankCalculator.Setup(rc => rc.GetValue(It.IsAny<List<PlayingCard>>())).Returns(expectedMaxHandValue); // Mock rank calculator to return expected max hand value

            // Act
            var result = _tableService.DetermineMaxHand(possibleCards);

            // Assert
            Assert.That(expectedMaxHandValue, Is.EqualTo(result)); // Ensure the correct max hand value is returned
            _mockRankCalculator.Verify(rc => rc.GetValue(It.IsAny<List<PlayingCard>>()), Times.AtLeastOnce); // Verify GetValue method was called at least once
        }

        [Test]
        public void DetermineWinners_ShouldReturnWinningPlayers()
        {
            // Arrange
            var activePlayers = new Queue<MenuWindow>();
            var player1 = new User(userID: 1, userName: "Player1", userChips: 1000);
            var player2 = new User(userID: 2, userName: "Player2", userChips: 1500);
            var player3 = new User(userID: 3, userName: "Player3", userChips: 2000);

            activePlayers.Enqueue(new MenuWindow(player1, _mockMainService.Object));
            activePlayers.Enqueue(new MenuWindow(player2, _mockMainService.Object));
            activePlayers.Enqueue(new MenuWindow(player3, _mockMainService.Object));

            var expectedWinners = new List<User> { player2 };
            // Set up player 1 hand
            var player1Hand = new List<PlayingCard> {
            new PlayingCard(value: PlayingCard.CARD_VALUES["A"], suit: PlayingCard.HEART_SYMBOL),
            new PlayingCard(value: PlayingCard.CARD_VALUES["K"], suit: PlayingCard.HEART_SYMBOL)
            };

                    // Set up player 2 hand
                    var player2Hand = new List<PlayingCard> {
            new PlayingCard(value: PlayingCard.CARD_VALUES["Q"], suit: PlayingCard.DIAMOND_SYMBOL),
            new PlayingCard(value: PlayingCard.CARD_VALUES["J"], suit: PlayingCard.DIAMOND_SYMBOL)
            };

                    // Set up player 3 hand
                    var player3Hand = new List<PlayingCard> {
            new PlayingCard(value: PlayingCard.CARD_VALUES["10"], suit: PlayingCard.SPADE_SYMBOL),
            new PlayingCard(value: PlayingCard.CARD_VALUES["9"], suit: PlayingCard.SPADE_SYMBOL)
            };

            // Set up hand values for players 1, 2, and 3
            _mockRankCalculator.SetupSequence(rc => rc.GetValue(It.IsAny<List<PlayingCard>>()))
                .Returns(new Tuple<int, int>(10, 1)) // Hand value for player 1
                .Returns(new Tuple<int, int>(9, 2))  // Hand value for player 2
                .Returns(new Tuple<int, int>(8, 3)); // Hand value for player 3

            // Act
            var result = _tableService.DetermineWinners(activePlayers);

            // Assert
            Assert.That(expectedWinners, Is.EqualTo(result)); // Ensure the correct winners are returned
            _mockRankCalculator.Verify(rc => rc.GetValue(It.IsAny<List<PlayingCard>>()), Times.Exactly(3)); // Verify GetValue method was called exactly 3 times
        }

        [Test]
        public void JoinTable_TableNotFull_PlayerJoined()
        {
            // Arrange
            var mockMenuWindow = new Mock<MenuWindow>();
            var player = new User(userID: 1, userName: "TestPlayer", userChips: 500); // Customize player data as needed

            mockMenuWindow.Setup(mw => mw.Player()).Returns(player);

            // Set up database service behavior
            _mockDatabaseService.Setup(ds => ds.UpdateUserChips(It.IsAny<int>(), It.IsAny<int>())).Verifiable();
            _mockDatabaseService.Setup(ds => ds.UpdateUserStack(It.IsAny<int>(), It.IsAny<int>())).Verifiable();

            // Act
            var sqlConnection = _mockSqlConnection.Object;
            int result = _tableService.JoinTable(mockMenuWindow.Object, ref sqlConnection);

            // Assert
            Assert.That(1, Is.EqualTo(result)); // Check that the return value indicates successful joining
            Assert.That(1, Is.EqualTo(player.UserTablePlace)); // Check that the player was assigned a table place
            Assert.That(1, Is.EqualTo(player.UserStatus)); // Check that the player's status is set to WAITING

            // Verify that database service methods were called
            _mockDatabaseService.Verify(ds => ds.UpdateUserChips(player.UserID, player.UserChips), Times.Once);
            _mockDatabaseService.Verify(ds => ds.UpdateUserStack(player.UserID, player.UserStack), Times.Once);
        }

        [Test]
        public void JoinTable_TableFull_PlayerNotJoined()
        {
            // Arrange
            // Set up TableService instance to be full
            var sqlConnection = _mockSqlConnection.Object;
            for (int i = 1; i <= 8; i++)
            {
                _tableService.JoinTable(new Mock<MenuWindow>().Object, ref sqlConnection);
            }

            var mockMenuWindow = new Mock<MenuWindow>();

            // Act
            int result = _tableService.JoinTable(mockMenuWindow.Object, ref sqlConnection);

            // Assert
            Assert.That(0, Is.EqualTo(result)); // Check that the return value indicates failure to join
        }

        [Test]
        public void JoinTable_InsufficientChips_PlayerNotJoined()
        {
            // Arrange
            var mockMenuWindow = new Mock<MenuWindow>();
            var player = new User(userID: 1, userName: "TestPlayer", userChips: 50); // Player with insufficient chips

            mockMenuWindow.Setup(mw => mw.Player()).Returns(player);

            // Act
            var sqlConnection = _mockSqlConnection.Object;
            int result = _tableService.JoinTable(mockMenuWindow.Object, ref sqlConnection);

            // Assert
            Assert.That(-1, Is.EqualTo(result)); // Check that the return value indicates insufficient chips
        }

        [Test]
        public void IsFull_TableNotFull_ReturnsFalse()
        {
            // Arrange
            // Set up the users list with less than FULL users
            var sqlConnection = _mockSqlConnection.Object;
            _tableService.JoinTable(new Mock<MenuWindow>().Object, ref sqlConnection);
            _tableService.JoinTable(new Mock<MenuWindow>().Object, ref sqlConnection);

            // Act
            bool result = _tableService.IsFull();

            // Assert
            ClassicAssert.IsFalse(result);
        }

        [Test]
        public void IsFull_TableFull_ReturnsTrue()
        {
            // Arrange
            // Set up the users list with FULL users
            var sqlConnection = _mockSqlConnection.Object;
            for (int i = 1; i <= 8; i++)
            {
                _tableService.JoinTable(new Mock<MenuWindow>().Object, ref sqlConnection);
            }

            // Act
            bool result = _tableService.IsFull();

            // Assert
            ClassicAssert.IsTrue(result);
        }

        [Test]
        public void IsEmpty_NoUsers_ReturnsTrue()
        {
            // Arrange

            // Act
            bool result = _tableService.IsEmpty();

            // Assert
            ClassicAssert.IsTrue(result);
        }

        [Test]
        public void IsEmpty_UsersExist_ReturnsFalse()
        {
            // Arrange
            var sqlConnection = _mockSqlConnection.Object;
            _tableService.JoinTable(new Mock<MenuWindow>().Object, ref sqlConnection);

            // Act
            bool result = _tableService.IsEmpty();

            // Assert
            ClassicAssert.IsFalse(result);
        }

        [Test]
        public void Occupied_NoUsers_ReturnsZero()
        {
            // Arrange

            // Act
            int result = _tableService.Occupied();

            // Assert
            ClassicAssert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void Occupied_UsersExist_ReturnsCorrectCount()
        {
            // Arrange
            var sqlConnection = _mockSqlConnection.Object;
            _tableService.JoinTable(new Mock<MenuWindow>().Object, ref sqlConnection);
            _tableService.JoinTable(new Mock<MenuWindow>().Object, ref sqlConnection);

            // Act
            int result = _tableService.Occupied();

            // Assert
            ClassicAssert.That(result, Is.EqualTo(2));
        }
    }
}