using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly;
using Monopoly.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyTests
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
            var player = new Player();
            var spacesToMove = 1;

            movementService.MovePlayer(player, spacesToMove);

            Assert.AreEqual(spacesToMove, player.Location);
        }

        [TestMethod]
        public void MovePlayerOnLastSquareByFiveSetsLocationToFour()
        {
            var player = new Player();
            var spacesToMove = 5;
            player.Location = GameBoardLength - 1;

            movementService.MovePlayer(player, spacesToMove);

            Assert.AreEqual(4, player.Location);
        }
    }
}
