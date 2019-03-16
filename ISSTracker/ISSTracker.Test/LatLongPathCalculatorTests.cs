using System;
using NUnit.Framework;
using ISSTracker.Model;
using ISSTracker.Controller;

namespace ISSTracker.Test
{
    [TestFixture]
    public class LatLongPathCalculatorTests
    {
        [TestCase(90, 0, 0, 0)]
        public void CalculatePath_IncorrectParameters_Throws(int lat1, int long1, int lat2, int long2)
        {
            Position a = new Position(lat1, long1);
            Position b = new Position(lat2, long2);
            LatLongPathCalculator pathCalculator = new LatLongPathCalculator();
            Assert.Throws<ArgumentOutOfRangeException>(() => pathCalculator.CalculatePath(a, b));
        }
    }
}