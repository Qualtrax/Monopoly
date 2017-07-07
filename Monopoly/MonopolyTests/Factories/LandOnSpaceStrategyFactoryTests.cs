using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Factories;
using Monopoly.Spaces;
using Monopoly.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly;

namespace MonopolyTests.Factories
{
    public class LandOnSpaceStrategyFactoryTests
    {
        [TestClass]
        public class LandOnStrategyFactoryTests
        {
            private ILandOnSpaceStrategyFactory factory;
            private Player player;

            public LandOnStrategyFactoryTests()
            {
                factory = new LandOnSpaceStrategyFactory();
                player = new Player("Lucas");
            }

            [TestMethod]
            public void CreateForGoToJailSpaceReturnsGoToJailStrategy()
            {
                var goToJailSpace = new GoToJailSpace();

                var resultStrategy = factory.Create(goToJailSpace, player);

                Assert.IsInstanceOfType(resultStrategy, typeof(GoToJailLandOnSpaceStrategy));
            }

            [TestMethod]
            public void CreateForJustVisitingSpaceReturnsEmptySpaceActionStrategy()
            {
                var justVisitingSpace = new JustVisitingSpace();

                var resultStrategy = factory.Create(justVisitingSpace, player);

                Assert.IsInstanceOfType(resultStrategy, typeof(EmptySpaceActionStrategy));
            }

            [TestMethod]
            public void CreateForIncomeTaxSpaceReturnsJustVisitingStrategy()
            {
                var incomeTaxSpace = new IncomeTaxSpace();

                var resultStrategy = factory.Create(incomeTaxSpace, player);

                Assert.IsInstanceOfType(resultStrategy, typeof(IncomeTaxLandOnSpaceStrategy));
            }


        }
    }
}
