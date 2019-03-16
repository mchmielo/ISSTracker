using System;
using NUnit.Framework;
using ISSTracker.Model;
using ISSTracker.Controller;

namespace ISSTracker.Test.Model
{
    [TestFixture]
    class PositionTests
    {
        [TestCase(91,0)]
        [TestCase(-91,0)]
        [TestCase(0,181)]
        [TestCase(0,-181)]
        public void IsCorrectPosition_NotCorrectPosition_ReturnsFalse(double lat, double lon)
        {
            Position position = new Position(lat, lon);
            Assert.AreEqual(false, position.IsPositionCorrect());
        }
        [TestCase(50, -56.5)]
        [TestCase(-5.5, 160.5)]
        public void IsCorrectPosition_CorrectPosition_ReturnsTrue(double lat, double lon)
        {
            Position position = new Position(lat, lon);
            Assert.AreEqual(true, position.IsPositionCorrect());
        }
    }
}
