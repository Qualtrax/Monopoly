using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly;
using Monopoly.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyTests.Services
{
    [TestClass]
    public class MovementServiceTests
    {
        private const Int32 GameBoardLength = 40;

        private GameBoard gameBoard;
        private MovementService movementService;

        public MovementServiceTests()
        {
            gameBoard = new GameBoard(GameBoardLength);
            movementService = new MovementService(gameBoard);
        }

        [TestMethod]
        public void MovePlayerByOneIncreasesPlayerLocationByOne()
        {
            var player = new Player("David");
            var spacesToMove = 1;

            movementService.MovePlayer(player, spacesToMove);

            Assert.AreEqual(spacesToMove, player.Location);
        }

        [TestMethod]
        public void MovePlayerOnLastSquareByFiveSetsLocationToFour()
        {
            var player = new Player("Tim");
            var spacesToMove = 5;
            player.Location = GameBoardLength - 1;

            movementService.MovePlayer(player, spacesToMove);

            Assert.AreEqual(4, player.Location);
        }

        [TestMethod]
        public void PlayerMovesToSpace38ThenMovesLandsOnGo()
        {
            var player = new Player("Tim");
            
            player.Location = GameBoardLength - 2;
            movementService.MovePlayer(player, 2);

            Assert.AreEqual(0, player.Location);
            Assert.AreEqual(200, player.Balance);
        }

        [TestMethod]
        public void MovePlayerPastGoIncreasesPlayerBalanceByTwoHundred()
        {
            var player = new Player("Tim");
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
            var player = new Player("Tim");
            var locationToStart = GameBoardLength - 2;
            var expectedLocation = 5;

            player.Location = locationToStart;
            movementService.MovePlayer(player, GameBoardLength + 7);

            Assert.AreEqual(expectedLocation, player.Location);
            Assert.AreEqual(400, player.Balance);
        }

        [TestMethod]
        public void MovePlayerFromGoToGenericSpaceDoesNotIncreaseBalance()
        {
            var player = new Player("Tim");

            movementService.MovePlayer(player, 2);

            Assert.AreEqual(2, player.Location);
            Assert.AreEqual(0, player.Balance);
        }
    }
}
