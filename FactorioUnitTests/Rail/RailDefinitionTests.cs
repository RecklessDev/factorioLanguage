using Microsoft.VisualStudio.TestTools.UnitTesting;
using Factorio.Rail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factorio.Rail.Tests
{
    [TestClass()]
    public class RailDefinitionTests
    {
        [TestMethod()]
        public void OrientationTest()
        {
            Assert.AreEqual(SquareSides.Up, Square.Reverse(SquareSides.Down));
            Assert.AreEqual(SquareSides.Down, Square.Reverse(SquareSides.Up));
            Assert.AreEqual(SquareSides.Right, Square.Reverse(SquareSides.Left));
            Assert.AreEqual(SquareSides.Left, Square.Reverse(SquareSides.Right));
        }
    }
}