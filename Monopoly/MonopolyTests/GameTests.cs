using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Monopoly;
using Moq;
using Monopoly.Services;

namespace MonopolyTests
{
    [TestClass]
    public class GameTests
    {
        private GameBoard gameBoard;
        private Mock<ITurnService> mockTurnService;
        private List<Player> players;
        private Game game;

        public GameTests()
        {
            gameBoard = new GameBoard(40);
            mockTurnService = new Mock<ITurnService>();
            players = new List<Player>();

            game = new Game(players, mockTurnService.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException),
            "Cannot start game with less than 2 players")]
        public void CreateAndPlayGameWithOnePlayerFails()
        {
            players.AddRange(new[] { new Player("Tim") });

            game.Play(20);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException),
            "Cannot start game with more than 8 players")]
        public void CreateAndPlayGameWithNinePlayersFails()
        {
            AddPlayers(new [] { "Tim", "Richard", "Dick", "Robert", "Bob",
                "Kevin", "David", "Lucas", "Luke"});

            game.Play(20);
        }

        [TestMethod]
        public void CreateHorseAndCarBothNamingOrdersOccurWithin100Games()
        {
            var horseThenCarOccurs = false;
            var carThenHorseOccurs = false;
            var carThenHorseList = new[] { "Car", "Horse" };
            var horseThenCarList = new[] { "Horse", "Car" };
            var carThenHorsePlayerList = carThenHorseList.Select(s => new Player(s));
            var horseThenCarPlayerList = horseThenCarList.Select(s => new Player(s));

            for (int i = 0; i < 100; i++)
            {
                var game = new Game(carThenHorsePlayerList, mockTurnService.Object);
                var players = carThenHorsePlayerList.ToList();
                game.Play(20);
                var playersStrings = game.Players.Select(p => p.Name).ToList();

                if (carThenHorseList.SequenceEqual(playersStrings))
                    horseThenCarOccurs = true;
                else if (horseThenCarList.SequenceEqual(playersStrings))
                    carThenHorseOccurs = true;
            }

            Assert.IsTrue(horseThenCarOccurs);
            Assert.IsTrue(carThenHorseOccurs);
        }

        [TestMethod]
        public void PlayingTwentyRoundsMakesRoundsCountTwenty()
        {
            AddPlayers(new[] { "Tim", "Lucas", "KPos" });
            game.Play(20);

            Assert.AreEqual(20, game.GetRoundsPlayed());
        }

        [TestMethod]
        public void PlayingTwentyRoundsMakesEachPlayersRoundCountTwenty()
        {
            AddPlayers(new[] { "Tim", "Lucas", "KPos" });
            game.Play(20);

            Assert.AreEqual(20, game.GetRoundsPlayed("Tim"));
            Assert.AreEqual(20, game.GetRoundsPlayed("Lucas"));
            Assert.AreEqual(20, game.GetRoundsPlayed("KPos"));
        }

        [TestMethod]
        public void PlayingTwoRoundsCheckThatPlayerOrderIsSameEachRound()
        {
            var firstPlayer = new Player("Tim");
            var secondPlayer = new Player("Lucas");
            var originalPlayers = new[] { firstPlayer, secondPlayer };
            var game = new Game(originalPlayers, mockTurnService.Object);

            game.Play(1);

            firstPlayer = game.Players.ElementAt(0);
            secondPlayer = game.Players.ElementAt(1);

            for (int i = 0; i < 10; i++) {
                game.Play(1);

                var playersAfterOneRound = game.Players.ToList();
                Assert.AreEqual(firstPlayer, playersAfterOneRound[0]);
                Assert.AreEqual(secondPlayer, playersAfterOneRound[1]);
            }
        }

        [TestMethod]
        public void PlayingOneRoundCallsMovePlayerWithSpacesToMoveOne()
        {
            AddPlayers(new[] { "Tim", "Lucas" });
            var playerOne = players[0];

            game.Play(1);

            mockTurnService.Verify(s => s.TakeTurn(playerOne, 1), Times.Once());
            mockTurnService.Verify(s => s.TakeTurn(It.IsAny<Player>(), It.IsAny<Int32>()), Times.Exactly(2));
        }

        [TestMethod]
        public void CreateGameWithTwoPlayersHorseAndCar()
        {
            var player1 = new Player("Horse");
            var player2 = new Player("Car");
            var playerList = new List<Player> { player1, player2 };

            Assert.AreEqual(1, playerList.Count(p => p.Name == player1.Name));
            Assert.AreEqual(1, playerList.Count(p => p.Name == player2.Name));
            Assert.AreEqual(2, playerList.Count);
        }

        private void AddPlayers(IEnumerable<String> playerNames)
        {
            players.AddRange(playerNames.Select(p => new Player(p)));
        }
    }
}