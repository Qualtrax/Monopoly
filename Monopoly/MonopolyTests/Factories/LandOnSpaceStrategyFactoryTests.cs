using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monopoly.Factories;
using Monopoly.Spaces;
using Monopoly.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyTests.Factories
{
    public class LandOnSpaceStrategyFactoryTests
    {
        [TestClass]
        public class LandOnStrategyFactoryTests
        {
            private ILandOnSpaceStrategyFactory factory;

            public LandOnStrategyFactoryTests()
            {
                factory = new LandOnSpaceStrategyFactory();
            }

            [TestMethod]
            public void CreateForGenericSpaceReturnsGenericStrategy()
            {
                var genericSpace = new GenericSpace();

                var resultStrategy = factory.Create(genericSpace);

                Assert.IsInstanceOfType(resultStrategy, typeof(GenericLandOnSpaceStrategy));
            }

            [TestMethod]
            public void CreateForGoSpaceReturnsGoStrategy()
            {
                var goSpace = new GoSpace();

                var resultStrategy = factory.Create(goSpace);

                Assert.IsInstanceOfType(resultStrategy, typeof(GoLandOnSpaceStrategy));
            }

            [TestMethod]
            public void CreateForGoToJailSpaceReturnsGoToJailStrategy()
            {
                var goToJailSpace = new GoToJailSpace();

                var resultStrategy = factory.Create(goToJailSpace);

                Assert.IsInstanceOfType(resultStrategy, typeof(GoToJailLandOnStrategy));
            }

            [TestMethod]
            public void CreateForJustVisitingSpaceReturnsJustVisitingStrategy()
            {
                var justVisitingSpace = new JustVisitingSpace();

                var resultStrategy = factory.Create(justVisitingSpace);

                Assert.IsInstanceOfType(resultStrategy, typeof(JustVisitingLandOnSpaceStrategy));
            }
        }
    }
}
