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
        public void DirectionTest()
        {
            //Turns
            Assert.AreEqual(0, RailPiece.Direction(RailFlags.CounterClockwise | RailFlags.Vertical | RailFlags.Front));
            Assert.AreEqual(1, RailPiece.Direction(RailFlags.Clockwise | RailFlags.Vertical | RailFlags.Front));
            Assert.AreEqual(2, RailPiece.Direction(RailFlags.CounterClockwise | RailFlags.Horizontal | RailFlags.Front));
            Assert.AreEqual(3, RailPiece.Direction(RailFlags.Clockwise | RailFlags.Horizontal | RailFlags.Front));
            Assert.AreEqual(4, RailPiece.Direction(RailFlags.CounterClockwise | RailFlags.Vertical | RailFlags.Back));
            Assert.AreEqual(5, RailPiece.Direction(RailFlags.Clockwise | RailFlags.Vertical | RailFlags.Back));
            Assert.AreEqual(6, RailPiece.Direction(RailFlags.CounterClockwise | RailFlags.Horizontal | RailFlags.Back));
            Assert.AreEqual(7, RailPiece.Direction(RailFlags.Clockwise | RailFlags.Horizontal | RailFlags.Back));

            //Straight
            Assert.AreEqual(0, RailPiece.Direction(RailFlags.Vertical));
            Assert.AreEqual(2, RailPiece.Direction(RailFlags.Horizontal));

            //Diagonal
            Assert.AreEqual(7, RailPiece.Direction(RailFlags.DiagonalUp));
            Assert.AreEqual(3, RailPiece.Direction(RailFlags.DiagonalDown));

            //Back Diagonal
            Assert.AreEqual(1, RailPiece.Direction(RailFlags.BackDiagonalUp));
            Assert.AreEqual(5, RailPiece.Direction(RailFlags.BackDiagonalDown));
        }

        [TestMethod()]
        public void IsCurvedRailTest()
        {
            Assert.AreEqual(true, RailPiece.IsCurvedRail(RailFlags.CounterClockwise | RailFlags.Vertical | RailFlags.Front));
            Assert.AreEqual(true, RailPiece.IsCurvedRail(RailFlags.Clockwise | RailFlags.Vertical | RailFlags.Front));
            Assert.AreEqual(true, RailPiece.IsCurvedRail(RailFlags.CounterClockwise | RailFlags.Horizontal | RailFlags.Front));
            Assert.AreEqual(true, RailPiece.IsCurvedRail(RailFlags.Clockwise | RailFlags.Horizontal | RailFlags.Front));
            Assert.AreEqual(true, RailPiece.IsCurvedRail(RailFlags.CounterClockwise | RailFlags.Vertical | RailFlags.Back));
            Assert.AreEqual(true, RailPiece.IsCurvedRail(RailFlags.Clockwise | RailFlags.Vertical | RailFlags.Back));
            Assert.AreEqual(true, RailPiece.IsCurvedRail(RailFlags.CounterClockwise | RailFlags.Horizontal | RailFlags.Back));
            Assert.AreEqual(true, RailPiece.IsCurvedRail(RailFlags.Clockwise | RailFlags.Horizontal | RailFlags.Back));

            //Straight
            Assert.AreEqual(false, RailPiece.IsCurvedRail(RailFlags.Vertical));
            Assert.AreEqual(false, RailPiece.IsCurvedRail(RailFlags.Horizontal));

            //Diagonal
            Assert.AreEqual(false, RailPiece.IsCurvedRail(RailFlags.DiagonalUp));
            Assert.AreEqual(false, RailPiece.IsCurvedRail(RailFlags.DiagonalDown));

            //Back Diagonal
            Assert.AreEqual(false, RailPiece.IsCurvedRail(RailFlags.BackDiagonalUp));
            Assert.AreEqual(false, RailPiece.IsCurvedRail(RailFlags.BackDiagonalDown));
        }
    }
}