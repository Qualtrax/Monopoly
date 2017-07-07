using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly;
using Monopoly.Factories;
using Monopoly.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Spaces;
using Monopoly.Strategies;

namespace MonopolyTests.Services
{
    [TestClass]
    public class TurnServiceTests
    {
        private const Int32 GameBoardLength = MonopolyConstants.BoardLength;

        private GameBoard gameBoard;
        private Player player;
        private TurnService turnService;
        private Mock<ILandOnSpaceStrategyFactory> mockLandOnSpaceStrategyFactory;
        private Mock<IEnterSpaceStrategyFactory> mockEnterSpaceStrategyFactory;

        public TurnServiceTests()
        {
            player = new Player("Tim");
            mockLandOnSpaceStrategyFactory = new Mock<ILandOnSpaceStrategyFactory>();
            mockEnterSpaceStrategyFactory = new Mock<IEnterSpaceStrategyFactory>();
            SetUpMocks();
            gameBoard = new GameBoard(GameBoardLength);
            turnService = new TurnService(gameBoard, mockEnterSpaceStrategyFactory.Object, mockLandOnSpaceStrategyFactory.Object);
        }

        private void SetUpMocks()
        {
            var emptySpaceActionStrategy = new EmptySpaceActionStrategy();
            var goEnterSpaceStrategy = new GoEnterSpaceStrategy();
            var goToJailLandOnSpaceStrategy = new GoToJailLandOnSpaceStrategy();

            mockEnterSpaceStrategyFactory.Setup(e => e.Create(It.IsAny<GenericSpace>(), player)).Returns(emptySpaceActionStrategy);
            mockLandOnSpaceStrategyFactory.Setup(e => e.Create(It.IsAny<GenericSpace>(), player)).Returns(emptySpaceActionStrategy);
            mockEnterSpaceStrategyFactory.Setup(e => e.Create(It.IsAny<GoSpace>(), player)).Returns(goEnterSpaceStrategy);
            mockLandOnSpaceStrategyFactory.Setup(e => e.Create(It.IsAny<GoSpace>(), player)).Returns(emptySpaceActionStrategy);
            mockEnterSpaceStrategyFactory.Setup(e => e.Create(It.IsAny<GoToJailSpace>(), player)).Returns(emptySpaceActionStrategy);
            mockLandOnSpaceStrategyFactory.Setup(e => e.Create(It.IsAny<GoToJailSpace>(), player)).Returns(goToJailLandOnSpaceStrategy);
            mockEnterSpaceStrategyFactory.Setup(e => e.Create(It.IsAny<JustVisitingSpace>(), player)).Returns(emptySpaceActionStrategy);
            mockLandOnSpaceStrategyFactory.Setup(e => e.Create(It.IsAny<JustVisitingSpace>(), player)).Returns(emptySpaceActionStrategy);
        }

        [TestMethod]
        public void MovePlayerByOneIncreasesPlayerLocationByOne()
        {
            var spacesToMove = 1;

            turnService.TakeTurn(player, spacesToMove);

            Assert.AreEqual(spacesToMove, player.Location);
            mockEnterSpaceStrategyFactory.Verify(e => e.Create(It.IsAny<GenericSpace>(), player), Times.Once());
            mockLandOnSpaceStrategyFactory.Verify(e => e.Create(It.IsAny<GenericSpace>(), player), Times.Once());
        }

        [TestMethod]
        public void MovePlayerOnLastSquareByFiveSetsLocationToFour()
        {
            var spacesToMove = 5;
            player.Location = GameBoardLength - 1;

            turnService.TakeTurn(player, spacesToMove);

            Assert.AreEqual(4, player.Location);
        }

        [TestMethod]
        public void PlayerMovesToSpace38ThenMovesLandsOnGo()
        {
            player.Location = GameBoardLength - 2;
            turnService.TakeTurn(player, 2);

            Assert.AreEqual(0, player.Location);
            Assert.AreEqual(200, player.Balance);
        }

        [TestMethod]
        public void MovePlayerPastGoIncreasesPlayerBalanceByTwoHundred()
        {
            var locationToStart = GameBoardLength - 2;
            var expectedLocation = 5;

            player.Location = locationToStart;
            turnService.TakeTurn(player, 7);

            Assert.AreEqual(expectedLocation, player.Location);
            Assert.AreEqual(200, player.Balance);
        }

        [TestMethod]
        public void MovePlayerPastGoTwiceIncreasesPlayerBalanceByFourHundred()
        {
            var locationToStart = GameBoardLength - 2;
            var expectedLocation = 5;

            player.Location = locationToStart;
            turnService.TakeTurn(player, GameBoardLength + 7);

            Assert.AreEqual(expectedLocation, player.Location);
            Assert.AreEqual(400, player.Balance);
        }

        [TestMethod]
        public void MovePlayerToGoToJailMovesPlayerToJustVisiting()
        {
            player.Location = MonopolyConstants.GoToJailLocation - 5;

            turnService.TakeTurn(player, 5);

            Assert.AreEqual(MonopolyConstants.JailLocation, player.Location);
            Assert.AreEqual(0, player.Balance);  
        }

        [TestMethod]
        public void MovePlayerOverGoToJailDoesNotMovePlayerToJail()
        {
            player.Location = MonopolyConstants.GoToJailLocation - 5;

            turnService.TakeTurn(player, 6);

            Assert.AreEqual(MonopolyConstants.GoToJailLocation + 1, player.Location);
            Assert.AreEqual(0, player.Balance);
        }

        [TestMethod]
        public void MovePlayerFromGoToGenericSpaceDoesNotIncreaseBalance()
        {
            turnService.TakeTurn(player, 2);

            Assert.AreEqual(2, player.Location);
            Assert.AreEqual(0, player.Balance);
        }
    }
}
