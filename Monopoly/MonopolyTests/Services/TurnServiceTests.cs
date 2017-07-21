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
using Monopoly.GameBoards;

namespace MonopolyTests.Services
{
    [TestClass]
    public class TurnServiceTests
    {
        private const Int32 GameBoardLength = MonopolyConstants.BoardLength;

        private Player player;
        private TurnService turnService;
        private Mock<ILandOnSpaceStrategyFactory> mockLandOnSpaceStrategyFactory;
        private Mock<IEnterSpaceStrategyFactory> mockEnterSpaceStrategyFactory;
        private Mock<IGameBoard> mockGameBoard;
        private Mock<IActionExecutor> mockActionExecutor;

        public TurnServiceTests()
        {
            player = new Player("Tim");
            mockLandOnSpaceStrategyFactory = new Mock<ILandOnSpaceStrategyFactory>();
            mockEnterSpaceStrategyFactory = new Mock<IEnterSpaceStrategyFactory>();
            mockGameBoard = new Mock<IGameBoard>();
            mockActionExecutor = new Mock<IActionExecutor>();
            SetUpMocks();
            turnService = new TurnService(mockGameBoard.Object, mockEnterSpaceStrategyFactory.Object, mockLandOnSpaceStrategyFactory.Object, mockActionExecutor.Object);
        }

        private void SetUpMocks()
        {
            var emptySpaceActionStrategy = new EmptySpaceActionStrategy();
            var goEnterSpaceStrategy = new GoEnterSpaceStrategy();
            var goToJailLandOnSpaceStrategy = new GoToJailLandOnSpaceStrategy();
            var spaces = SetUpSpaces();

            mockGameBoard.Setup(b => b.Spaces).Returns(spaces).Verifiable();
            mockEnterSpaceStrategyFactory.Setup(e => e.Create(It.IsAny<GenericSpace>(), player)).Returns(emptySpaceActionStrategy);
            mockLandOnSpaceStrategyFactory.Setup(e => e.Create(It.IsAny<GenericSpace>(), player)).Returns(emptySpaceActionStrategy);
            mockEnterSpaceStrategyFactory.Setup(e => e.Create(It.IsAny<GoSpace>(), player)).Returns(goEnterSpaceStrategy);
            mockLandOnSpaceStrategyFactory.Setup(e => e.Create(It.IsAny<GoSpace>(), player)).Returns(emptySpaceActionStrategy);
            mockEnterSpaceStrategyFactory.Setup(e => e.Create(It.IsAny<GoToJailSpace>(), player)).Returns(emptySpaceActionStrategy);
            mockLandOnSpaceStrategyFactory.Setup(e => e.Create(It.IsAny<GoToJailSpace>(), player)).Returns(goToJailLandOnSpaceStrategy);
            mockEnterSpaceStrategyFactory.Setup(e => e.Create(It.IsAny<JustVisitingSpace>(), player)).Returns(emptySpaceActionStrategy);
            mockLandOnSpaceStrategyFactory.Setup(e => e.Create(It.IsAny<JustVisitingSpace>(), player)).Returns(emptySpaceActionStrategy);
        }

        private IEnumerable<ISpace> SetUpSpaces()
        {
            var spaces = new List<ISpace>();

            for (var i = 0; i < GameBoardLength; i++)
                spaces.Add(new GenericSpace());

            return spaces;
        }

        

        [TestMethod]
        public void MovePlayerOnLastSquareByFiveSetsLocationToFour()
        {
            var spacesToMove = 5;
            player.Location = GameBoardLength - 1;

            turnService.TakeTurn(player, spacesToMove);

            Assert.AreEqual(4, player.Location);
            mockActionExecutor.Verify();
        }

        [TestMethod]
        public void PlayerMovesToSpace38ThenMovesLandsOnGo()
        {
            player.Location = GameBoardLength - 2;
            turnService.TakeTurn(player, 2);

            Assert.AreEqual(0, player.Location);
            Assert.AreEqual(200, player.Balance);
            mockActionExecutor.Verify();
        }

        [TestMethod]
        public void MovePlayerPastGoIncreasesPlayerBalanceByTwoHundred()
        {
            var goSpace = new GoSpace();
            var spaces = new ISpace[] { new GenericSpace(), goSpace, new GenericSpace() };
            mockGameBoard.Setup(b => b.Spaces).Returns(spaces).Verifiable();
            mockEnterSpaceStrategyFactory.Setup(f => f.Create(goSpace, player)).Returns(new GoEnterSpaceStrategy()).Verifiable();
            mockEnterSpaceStrategyFactory.Setup(f => f.Create(It.IsAny<GenericSpace>(), It.IsAny<Player>())).Returns(new EmptySpaceActionStrategy()).Verifiable();
            turnService = new TurnService(mockGameBoard.Object, mockEnterSpaceStrategyFactory.Object, mockLandOnSpaceStrategyFactory.Object, mockActionExecutor.Object);

            turnService.TakeTurn(player, 2);

            Assert.AreEqual(200, player.Balance);

            mockGameBoard.Verify();
            mockGameBoard.Verify();
            mockActionExecutor.Verify();
            mockEnterSpaceStrategyFactory.Verify();
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
            mockActionExecutor.Verify();
        }

        [TestMethod]
        public void MovePlayerToGoToJailMovesPlayerToJustVisiting()
        {
            player.Location = MonopolyConstants.GoToJailLocation - 5;

            turnService.TakeTurn(player, 5);

            Assert.AreEqual(MonopolyConstants.JailLocation, player.Location);
            Assert.AreEqual(0, player.Balance);
            mockActionExecutor.Verify();
        }

        [TestMethod]
        public void MovePlayerOverGoToJailDoesNotMovePlayerToJail()
        {
            player.Location = MonopolyConstants.GoToJailLocation - 5;

            turnService.TakeTurn(player, 6);

            Assert.AreEqual(MonopolyConstants.GoToJailLocation + 1, player.Location);
            Assert.AreEqual(0, player.Balance);
            mockActionExecutor.Verify();
        }

        [TestMethod]
        public void MovePlayerFromGoToGenericSpaceDoesNotIncreaseBalance()
        {
            turnService.TakeTurn(player, 2);

            Assert.AreEqual(2, player.Location);
            Assert.AreEqual(0, player.Balance);
            mockActionExecutor.Verify();
        }
    }
}
