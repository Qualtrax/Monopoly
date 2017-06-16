using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Monopoly;

namespace MonopolyTests
{
    [TestClass]
    public class GameTests
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException),
            "Cannot start game with less than 2 players")]
        public void CreateAndPlayGameWithOnePlayerFails()
        {
            var players = new List<Player>() { new Player("Tim") };

            var game = new Game(players);
            game.Play(20);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException),
            "Cannot start game with more than 8 players")]
        public void CreateAndPlayGameWithNinePlayersFails()
        {
            var players = CreatePlayersFromNames(new [] { "Tim", "Richard", "Dick", "Robert", "Bob",
                "Kevin", "David", "Lucas", "Luke"});
            var game = new Game(players);

            game.Play(20);
        }

        [TestMethod]
        public void CreateHorseAndCarBothNamingOrdersOccurWithin100Games()
        {
            var horseThenCarOccurs = false;
            var carThenHorseOccurs = false;
            var carThenHorseList = new[] { "Car", "Horse" };
            var horseThenCarList = new[] { "Horse", "Car" };
            var carThenHorsePlayerList = CreatePlayersFromNames(carThenHorseList);
            var horseThenCarPlayerList = CreatePlayersFromNames(horseThenCarList);

            for (int i = 0; i < 100; i++)
            {
                var players = carThenHorsePlayerList.ToList();
                var game = new Game(players);
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
            var players = CreatePlayersFromNames(new[] { "Tim", "Lucas", "KPos" });
            var game = new Game(players);
            game.Play(20);

            Assert.AreEqual(20, game.GetRoundsPlayed());
        }

        [TestMethod]
        public void PlayingTwentyRoundsMakesEachPlayersRoundCountTwenty()
        {
            var players = CreatePlayersFromNames(new[] { "Tim", "Lucas", "KPos" });
            var game = new Game(players);
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

            var game = new Game(originalPlayers);
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
        public void PlayingOneRoundMovesPlayerByOneSpace()
        {
            var players = CreatePlayersFromNames(new[] { "Tim", "Lucas" });
            var game = new Game(players.ToList());

            game.Play(1);
            
            Assert.AreEqual(1, game.Players.First().Location);
        }

        [TestMethod]
        public void PlayerStartingLastSpaceAndMovesSixEndsOnFive()
        {
            var players = CreatePlayersFromNames(new[] { "Tim", "Lucas" }).ToList();
            players[0].Location = GameBoard.GameBoardLength - 1;
            players[1].Location = GameBoard.GameBoardLength - 1;

            var game = new Game(players);

            game.Play(6);

            Assert.AreEqual(5, game.Players.First().Location);
        }

        [TestMethod]
        public void CreateGameWithTwoPlayersHorseAndCar()
        {
            var player1 = new Player("Horse");
            var player2 = new Player("Car");
            var playerList = new List<Player> { player1, player2 };

            var game = new Game(playerList);

            Assert.AreEqual(1, playerList.Count(p => p.Name == player1.Name));
            Assert.AreEqual(1, playerList.Count(p => p.Name == player2.Name));
            Assert.AreEqual(2, playerList.Count);
        }

        private IEnumerable<Player> CreatePlayersFromNames(IEnumerable<String> playerNames)
        {
            return playerNames.Select(p => new Player(p));
        }
    }
}