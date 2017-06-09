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
            var carThenHorseList = CreatePlayersFromNames(new [] { "Car", "Horse" });
            var horseThenCarList = CreatePlayersFromNames(new [] { "Horse", "Car" });

            for (int i = 0; i < 100; i++)
            {
                var players = carThenHorseList.ToList();
                var game = new Game(players);
                game.Play(20);

                if (carThenHorseList.SequenceEqual<Player>(players))
                    horseThenCarOccurs = true;
                else if (horseThenCarList.SequenceEqual<Player>(players))
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
            var players = CreatePlayersFromNames(new[] { "Tim", "Lucas", "KPos" });
            var game = new Game(players);
            game.Play(1);
            var playersAfterOneRound = players.ToList();

            game.Play(1);

            Assert.IsTrue(playersAfterOneRound.SequenceEqual<Player>(players));
        }

        [TestMethod]
        public void MovingNewPlayerBySevenLandsOnSeven()
        {
            var players = CreatePlayersFromNames(new[] { "Tim", "Lucas" });
            var game = new Game(players.ToList());

            game.Play(1);
            
            Assert.AreEqual(2, players.First().Location);
        }

        [TestMethod]
        public void PlayerStartingOnThirtyNineAndMovesSixEndsOnFive()
        {
            var players = CreatePlayersFromNames(new[] { "Tim", "Lucas" });
            var game = new Game(players);

            game.Play(21);

            Assert.AreEqual(2, players.First().Location);
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