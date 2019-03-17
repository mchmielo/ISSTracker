using System;
using NUnit.Framework;
using ISSTracker.Model;
using ISSTracker.Controller;

namespace ISSTracker.Test
{
    [TestFixture]
    public class LatLongCalculatorTests
    {
        [TestCase(91, 0, 0, 0)]
        [TestCase(0, -181, 0, 0)]
        [TestCase(0, 4.4, -91.7, 0)]
        [TestCase(0, 4.4, 50, 181.1)]
        public void CalculatePath_IncorrectParameters_Throws(double lat1, double long1, double lat2, double long2)
        {
            Position a = new Position(lat1, long1);
            Position b = new Position(lat2, long2);
            LatLongCalculator pathCalculator = new LatLongCalculator();
            Assert.Throws<ArgumentOutOfRangeException>(() => pathCalculator.CalculatePath(a, b));
        }
        [TestCase(0, 0, 50, 50, 7761)]
        [TestCase(-50, -100, 50, 100, 19780)]
        public void CalculatePath_ReturnsValue(double lat1, double long1, double lat2, double long2, double result)
        {
            Position a = new Position(lat1, long1);
            Position b = new Position(lat2, long2);
            LatLongCalculator pathCalulator = new LatLongCalculator();
            Assert.AreEqual(result, pathCalulator.CalculatePath(a, b));
        }

        [TestCase(91, 0, 1552757126, 0, 0, 1552757413)]
        [TestCase(0, 0, 0, 50, 50, 0)]
        public void CalculateSpeed_IncorrectParameters_Throws(double lat1, double long1, int time1, double lat2, double long2, int time2)
        {
            ISSPosition issPosition1 = new ISSPosition();
            issPosition1.ISS_Position = new Position(lat1, long1);
            issPosition1.Timestamp = time1;
            ISSPosition issPosition2 = new ISSPosition();
            issPosition2.ISS_Position = new Position(lat2, long2);
            issPosition2.Timestamp = time2;
            LatLongCalculator pathCalculator = new LatLongCalculator();
            Assert.Throws<ArgumentOutOfRangeException>(() => pathCalculator.CalculateSpeed(issPosition1, issPosition2));
        }

        [TestCase(44.811135, -109.5421143, 1552817684, 44.394142, -108.57043, 1552817697, 7.62)]
        [TestCase(26.45, 150.29, 1552817684, 27.06, 150.89, 1552817697, 7.66)]
        public void CalculateSpeed_CorrectParameters_ReturnsValue(double lat1, double long1, int time1, double lat2, double long2, int time2, double speed)
        {
            ISSPosition issPosition1 = new ISSPosition();
            issPosition1.ISS_Position = new Position(lat1, long1);
            issPosition1.Timestamp = time1;
            ISSPosition issPosition2 = new ISSPosition();
            issPosition2.ISS_Position = new Position(lat2, long2);
            issPosition2.Timestamp = time2;
            LatLongCalculator pathCalculator = new LatLongCalculator();
            Assert.AreEqual(speed, pathCalculator.CalculateSpeed(issPosition1, issPosition2));
        }
    }
}