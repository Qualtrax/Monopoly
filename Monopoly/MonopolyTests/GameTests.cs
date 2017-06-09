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
        private Game game;

        public GameTests()
        {
            game = new Game();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Cannot start game with less than 2 players")]
        public void CreateAndPlayGameWithOnePlayerFails()
        {
            var players = new[] { "Tim" };
            game.SetupGame(players);
            game.Play(20);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Cannot start game with more than 8 players")]
        public void CreateAndPlayGameWithNinePlayersFails()
        {
            var players = new[] { "Tim", "Richard", "Dick", "Robert", "Bob",
                "Kevin", "David", "Lucas", "Luke"};
            game.SetupGame(players);
            game.Play(20);
        }

        [TestMethod]
        public void CreateHorseAndCarBothNamingOrdersOccurWithin100Games()
        {
            var horseThenCarOccurs = false;
            var carThenHorseOccurs = false;
            var carThenHorseList = new List<String> { "Car", "Horse" };
            var horseThenCarList = new List<String> { "Horse", "Car" };

            for (int i = 0; i < 100; i++)
            {
                game.SetupGame(carThenHorseList.ToArray());
                game.Play(20);
                var randomizedPlayerList = game.GetPlayerNames().ToList<String>();

                if (carThenHorseList.SequenceEqual<String>(randomizedPlayerList))
                    horseThenCarOccurs = true;
                else if (horseThenCarList.SequenceEqual<String>(randomizedPlayerList))
                    carThenHorseOccurs = true;
            }

            Assert.IsTrue(horseThenCarOccurs);
            Assert.IsTrue(carThenHorseOccurs);
        }

        [TestMethod]
        public void PlayingTwentyRoundsMakesRoundsCountTwenty()
        {
            var players = new[] { "Tim", "Lucas", "KPos" };
            game.SetupGame(players);
            game.Play(20);

            Assert.AreEqual(20, game.GetRoundsPlayed());
        }

        [TestMethod]
        public void PlayingTwentyRoundsMakesEachPlayersRoundCountTwenty()
        {
            var players = new[] { "Tim", "Lucas", "KPos" };
            game.SetupGame(players);
            game.Play(20);

            Assert.AreEqual(20, game.GetRoundsPlayed("Tim"));
            Assert.AreEqual(20, game.GetRoundsPlayed("Lucas"));
            Assert.AreEqual(20, game.GetRoundsPlayed("KPos"));
        }

        [TestMethod]
        public void PlayingTwoRoundsCheckThatPlayerOrderIsSameEachRound()
        {
            var players = new[] { "Tim", "Lucas", "KPos" };
            game.SetupGame(players);
            players = game.GetPlayerNames().ToArray();
            game.Play(1);
            var playersAfterRounds = game.GetPlayerNames().ToArray();

            Assert.AreEqual(players[0], playersAfterRounds[0]);
            Assert.AreEqual(players[1], playersAfterRounds[1]);
            Assert.AreEqual(players[2], playersAfterRounds[2]);

            game.Play(1);
            playersAfterRounds = game.GetPlayerNames().ToArray();

            Assert.AreEqual(players[0], playersAfterRounds[0]);
            Assert.AreEqual(players[1], playersAfterRounds[1]);
            Assert.AreEqual(players[2], playersAfterRounds[2]);
        }
    }
}