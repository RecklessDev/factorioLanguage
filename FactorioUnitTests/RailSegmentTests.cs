using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using factorioLanguage.Rails;
using System.Numerics;
namespace FactorioUnitTests
{
    [TestClass]
    public class RailSegmentTests
    {
        [TestMethod]
        public void CurvedRailSegmentsWorksForBothOrientations()
        {
            var startPos = new Vector2(5, 12);

            for (int angle = 0; angle <= 360; angle += 45)
            {
                var initial = new CurvedRailSegment(startPos, angle, TurnDirection.Clockwise);
                var second = new CurvedRailSegment(initial.EndPosition, angle + 45, TurnDirection.Counterclockwise);
                Assert.AreEqual(initial.FromPosition, second.EndPosition, "Failed at angle {0}.", angle);
                Assert.AreEqual(initial.EndPosition, second.FromPosition, "Failed at angle {0}.", angle);
                Assert.AreEqual(initial.Center, second.Center, "Failed at angle {0}.", angle);
            }

            
            
        }

        [TestMethod]
        public void ClockwiseAndCounterclockwiseCirclesShouldBeIdentical()
        {
            var startPos = new Vector2(5, 12);
            List<CurvedRailSegment> clockwise = new List<CurvedRailSegment>();
            {
                for (int angle = 0; angle < 360; angle += 45)
                {
                    clockwise.Add(new CurvedRailSegment(startPos, angle, TurnDirection.Clockwise));
                    startPos = clockwise.Last().EndPosition;
                }
            }

            startPos = new Vector2(5, 12);
            List<CurvedRailSegment> counterClockwise = new List<CurvedRailSegment>();
            {
                for (int angle = 360; angle > 0; angle -= 45)
                {
                    counterClockwise.Add(new CurvedRailSegment(startPos, angle, TurnDirection.Counterclockwise));
                    startPos = counterClockwise.Last().EndPosition;
                }
            }

            Assert.AreEqual(clockwise.Count, counterClockwise.Count);

            for (int i = 0; i < clockwise.Count; i++)
            {
                var railC = clockwise[i];
                var railCC = counterClockwise[clockwise.Count - i - 1];

                Assert.AreEqual(railC.Center, railCC.Center);
                Assert.AreEqual(railC.FromPosition, railCC.EndPosition);
                Assert.AreEqual(railC.EndPosition, railCC.FromPosition);
            }
        }
    }
}
