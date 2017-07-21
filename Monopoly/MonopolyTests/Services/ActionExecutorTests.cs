using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly;
using Monopoly.Factories;
using Monopoly.GameBoards;
using Monopoly.Services;
using Monopoly.Spaces;
using Monopoly.Strategies;
using Moq;

namespace MonopolyTests.Services
{
    [TestClass]
    public class ActionExecutorTests
    {
        private ISpaceActionStrategy spaceActionStrategy;
        private Player player;
        private const Int32 GameBoardLength = MonopolyConstants.BoardLength;
        private ActionExecutor actionExecutor;
        private Mock<IGameBoard> mockGameBoard;

        public ActionExecutorTests(ISpaceActionStrategy spaceActionStrategy, Player player)
        {
            this.spaceActionStrategy = spaceActionStrategy;
            this.player = player;
            mockGameBoard = new Mock<IGameBoard>();
            actionExecutor = new ActionExecutor();
            SetUpMocks();
        }

        [TestInitialize]
        public void SetUp()
        {
            player = new Player("Timmayyyy");
            
        }
        private void SetUpMocks()
        {
            var emptySpaceActionStrategy = new EmptySpaceActionStrategy();
            var goEnterSpaceStrategy = new GoEnterSpaceStrategy();
            var goToJailLandOnSpaceStrategy = new GoToJailLandOnSpaceStrategy();
            var spaces = SetUpSpaces();

            mockGameBoard.Setup(b => b.Spaces).Returns(spaces).Verifiable();
        }

        private IEnumerable<ISpace> SetUpSpaces()
        {
            var spaces = new List<ISpace>();

            for (var i = 0; i < GameBoardLength; i++)
                spaces.Add(new GenericSpace());

            return spaces;
        }
        [TestMethod]
        public void MovePlayerByOneIncreasesPlayerLocationByOne()
        {
            var spacesToMove = 1;

            Assert.AreEqual(spacesToMove, player.Location);
        }
    }
}
