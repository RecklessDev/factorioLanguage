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
            Assert.AreEqual(SquareSides.Up, Matcher.Reverse(SquareSides.Down));
            Assert.AreEqual(SquareSides.Down, Matcher.Reverse(SquareSides.Up));
            Assert.AreEqual(SquareSides.Right, Matcher.Reverse(SquareSides.Left));
            Assert.AreEqual(SquareSides.Left, Matcher.Reverse(SquareSides.Right));
        }

        [TestMethod()]
        public void DirectionTest()
        {
            Assert.AreEqual(0, RailPiece.Direction(RailFlags.CounterClockwise | RailFlags.Vertical | RailFlags.Front));
            Assert.AreEqual(1, RailPiece.Direction(RailFlags.Clockwise        | RailFlags.Vertical | RailFlags.Front));
            Assert.AreEqual(2, RailPiece.Direction(RailFlags.CounterClockwise | RailFlags.Horizontal | RailFlags.Front));
            Assert.AreEqual(3, RailPiece.Direction(RailFlags.Clockwise        | RailFlags.Horizontal | RailFlags.Front));
            Assert.AreEqual(4, RailPiece.Direction(RailFlags.CounterClockwise | RailFlags.Vertical | RailFlags.Back));
            Assert.AreEqual(5, RailPiece.Direction(RailFlags.Clockwise        | RailFlags.Vertical | RailFlags.Back));
            Assert.AreEqual(6, RailPiece.Direction(RailFlags.CounterClockwise | RailFlags.Horizontal | RailFlags.Back));
            Assert.AreEqual(7, RailPiece.Direction(RailFlags.Clockwise        | RailFlags.Horizontal | RailFlags.Back));

            Assert.AreEqual(0, RailPiece.Direction(RailFlags.Vertical));
            Assert.AreEqual(2, RailPiece.Direction(RailFlags.Horizontal));
        }
    }
}