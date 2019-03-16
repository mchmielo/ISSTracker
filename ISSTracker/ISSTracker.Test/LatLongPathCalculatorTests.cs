using System;
using NUnit.Framework;
using ISSTracker.Model;
using ISSTracker.Controller;

namespace ISSTracker.Test
{
    [TestFixture]
    public class LatLongPathCalculatorTests
    {
        [TestCase(91, 0, 0, 0)]
        [TestCase(0, -181, 0, 0)]
        [TestCase(0, 4.4, -91.7, 0)]
        [TestCase(0, 4.4, 50, 181.1)]
        public void CalculatePath_IncorrectParameters_Throws(double lat1, double long1, double lat2, double long2)
        {
            Position a = new Position(lat1, long1);
            Position b = new Position(lat2, long2);
            LatLongPathCalculator pathCalculator = new LatLongPathCalculator();
            Assert.Throws<ArgumentOutOfRangeException>(() => pathCalculator.CalculatePath(a, b));
        }
        [TestCase(0, 0, 50, 50, 7761)]
        [TestCase(-50, -100, 50, 100, 19780)]
        public void CalculatePath_ReturnsValue(double lat1, double long1, double lat2, double long2, double result)
        {
            Position a = new Position(lat1, long1);
            Position b = new Position(lat2, long2);
            LatLongPathCalculator pathCalulator = new LatLongPathCalculator();
            Assert.AreEqual(result, pathCalulator.CalculatePath(a, b));
        }
    }
}