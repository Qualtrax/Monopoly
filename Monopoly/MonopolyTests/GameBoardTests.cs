using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly;

namespace MonopolyTests
{
    [TestClass]
    public class GameBoardTests
    {
        private Player player;
        private GameBoard board;

        public GameBoardTests()
        {
            player = new Player("Richard");
            board = new GameBoard(new[] { player });
        }

        [TestMethod]
        public void NewPlayerStartWithLocationZero()
        {
            Assert.AreEqual(0, player.Location);
        }

        [TestMethod]
        public void MovingNewPlayerBySevenLandsOnSeven()
        {
            board.MovePlayer(7);

            Assert.AreEqual(7, player.Location);
        }

        [TestMethod]
        public void PlayerStartingOnThirtyNineAndMovesSixEndsOnFive()
        {
            player.Location = 39;
            board.MovePlayer(6);

            Assert.AreEqual(5, player.Location);
        }

        [TestMethod]
        public void CreateGameWithTwoPlayersHorseAndCar()
        {
            var player1 = new Player("Horse");
            var player2 = new Player("Car");
            var playerList = new List<Player> { player1, player2 };

            var gameBoard = new GameBoard(playerList);
            
            var gameBoardPlayerList = gameBoard.Players.ToList();
            Assert.AreEqual(1, gameBoardPlayerList.Count(p => p.Name == player1.Name));
            Assert.AreEqual(1, gameBoardPlayerList.Count(p => p.Name == player2.Name));
            Assert.AreEqual(2, gameBoardPlayerList.Count);
        }
    }
}
