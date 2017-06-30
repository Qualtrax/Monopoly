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
    public class MovementServiceTests
    {
        private const Int32 GameBoardLength = MonopolyConstants.BoardLength;

        private GameBoard gameBoard;
        private Player player;
        private MovementService movementService;
        private Mock<ILandOnSpaceStrategyFactory> mockLandOnSpaceStrategyFactory;
        private Mock<IEnterSpaceStrategyFactory> mockEnterSpaceStrategyFactory;

        public MovementServiceTests()
        {
            player = new Player("Tim");
            mockLandOnSpaceStrategyFactory = new Mock<ILandOnSpaceStrategyFactory>();
            mockEnterSpaceStrategyFactory = new Mock<IEnterSpaceStrategyFactory>();
            SetUpMocks();
            gameBoard = new GameBoard(GameBoardLength);
            movementService = new MovementService(gameBoard, mockEnterSpaceStrategyFactory.Object, mockLandOnSpaceStrategyFactory.Object);
        }

        private void SetUpMocks()
        {
            var genericEnterSpaceStrategy = new GenericEnterSpaceStrategy();
            var genericLandOnSpaceStrategy = new GenericLandOnSpaceStrategy();
            var goEnterSpaceStrategy = new GoEnterSpaceStrategy(player);
            var goLandOnSpaceStrategy = new GoLandOnSpaceStrategy();
            var goToJailLandOnSpaceStrategy = new GoToJailLandOnSpaceStrategy(player);
            var goToJailEnterSpaceStrategy = new GoToJailEnterSpaceStrategy();
            var justVisitingEnterSpaceStrategy = new JustVisitingEnterSpaceStrategy();
            var justVisitingLandOnSpaceStrategy = new JustVisitingLandOnSpaceStrategy();

            mockEnterSpaceStrategyFactory.Setup(e => e.Create(It.IsAny<GenericSpace>(), player)).Returns(genericEnterSpaceStrategy);
            mockLandOnSpaceStrategyFactory.Setup(e => e.Create(It.IsAny<GenericSpace>(), player)).Returns(genericLandOnSpaceStrategy);
            mockEnterSpaceStrategyFactory.Setup(e => e.Create(It.IsAny<GoSpace>(), player)).Returns(goEnterSpaceStrategy);
            mockLandOnSpaceStrategyFactory.Setup(e => e.Create(It.IsAny<GoSpace>(), player)).Returns(goLandOnSpaceStrategy);
            mockEnterSpaceStrategyFactory.Setup(e => e.Create(It.IsAny<GoToJailSpace>(), player)).Returns(goToJailEnterSpaceStrategy);
            mockLandOnSpaceStrategyFactory.Setup(e => e.Create(It.IsAny<GoToJailSpace>(), player)).Returns(goToJailLandOnSpaceStrategy);
            mockEnterSpaceStrategyFactory.Setup(e => e.Create(It.IsAny<JustVisitingSpace>(), player)).Returns(justVisitingEnterSpaceStrategy);
            mockLandOnSpaceStrategyFactory.Setup(e => e.Create(It.IsAny<JustVisitingSpace>(), player)).Returns(justVisitingLandOnSpaceStrategy);
        }

        [TestMethod]
        public void MovePlayerByOneIncreasesPlayerLocationByOne()
        {
            var spacesToMove = 1;

            movementService.MovePlayer(player, spacesToMove);

            Assert.AreEqual(spacesToMove, player.Location);
            mockEnterSpaceStrategyFactory.Verify(e => e.Create(It.IsAny<GenericSpace>(), player), Times.Once());
            mockLandOnSpaceStrategyFactory.Verify(e => e.Create(It.IsAny<GenericSpace>(), player), Times.Once());
        }

        [TestMethod]
        public void MovePlayerOnLastSquareByFiveSetsLocationToFour()
        {
            var spacesToMove = 5;
            player.Location = GameBoardLength - 1;

            movementService.MovePlayer(player, spacesToMove);

            Assert.AreEqual(4, player.Location);
        }

        [TestMethod]
        public void PlayerMovesToSpace38ThenMovesLandsOnGo()
        {
            player.Location = GameBoardLength - 2;
            movementService.MovePlayer(player, 2);

            Assert.AreEqual(0, player.Location);
            Assert.AreEqual(200, player.Balance);
        }

        [TestMethod]
        public void MovePlayerPastGoIncreasesPlayerBalanceByTwoHundred()
        {
            var locationToStart = GameBoardLength - 2;
            var expectedLocation = 5;

            player.Location = locationToStart;
            movementService.MovePlayer(player, 7);

            Assert.AreEqual(expectedLocation, player.Location);
            Assert.AreEqual(200, player.Balance);
        }

        [TestMethod]
        public void MovePlayerPastGoTwiceIncreasesPlayerBalanceByFourHundred()
        {
            var locationToStart = GameBoardLength - 2;
            var expectedLocation = 5;

            player.Location = locationToStart;
            movementService.MovePlayer(player, GameBoardLength + 7);

            Assert.AreEqual(expectedLocation, player.Location);
            Assert.AreEqual(400, player.Balance);
        }

        [TestMethod]
        public void MovePlayerToGoToJailMovesPlayerToJustVisiting()
        {
            player.Location = MonopolyConstants.GoToJailLocation - 5;

            movementService.MovePlayer(player, 5);

            Assert.AreEqual(MonopolyConstants.JailLocation, player.Location);
            Assert.AreEqual(0, player.Balance);  
        }

        [TestMethod]
        public void MovePlayerOverGoToJailDoesNotMovePlayerToJail()
        {
            player.Location = MonopolyConstants.GoToJailLocation - 5;

            movementService.MovePlayer(player, 6);

            Assert.AreEqual(MonopolyConstants.GoToJailLocation + 1, player.Location);
            Assert.AreEqual(0, player.Balance);
        }

        [TestMethod]
        public void MovePlayerFromGoToGenericSpaceDoesNotIncreaseBalance()
        {
            movementService.MovePlayer(player, 2);

            Assert.AreEqual(2, player.Location);
            Assert.AreEqual(0, player.Balance);
        }
    }
}
