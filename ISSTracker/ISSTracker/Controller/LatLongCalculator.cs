using ISSTracker.Model;
using System;

namespace ISSTracker.Controller
{
    public class LatLongCalculator
    {
        const double R = 6779e3;    // Earth radius + 408 km ISS's altitude (in metres)

        public double CalculatePath(Position a, Position b)
        {
            if(!ArePositionsCorrect(a, b))
            {
                throw new ArgumentOutOfRangeException($"Incorrect passed positions.");
            }
            double parA = Math.Sin((b.Latitude - a.Latitude).ToRadians() / 2)
                * Math.Sin((b.Latitude - a.Latitude).ToRadians() / 2)
                + Math.Cos(a.Latitude.ToRadians()) * Math.Cos(b.Latitude.ToRadians())
                * Math.Sin((b.Longitude - a.Longitude).ToRadians() / 2) 
                * Math.Sin((b.Longitude - a.Longitude).ToRadians() / 2);
            double parC = 2 * Math.Atan2(Math.Sqrt(parA), Math.Sqrt(1 - parA));
            return R*parC/1000;
        }

        public double CalculateSpeed(ISSPosition a, ISSPosition b)
        {
            if (a.Timestamp == 0 || b.Timestamp == 0)
            {
                throw new ArgumentOutOfRangeException($"Incorrect passed positions.");
            }
            int deltaTime = b.Timestamp - a.Timestamp;
            double distance = CalculatePath(a.ISS_Position, b.ISS_Position);
            return distance/(deltaTime);
        }

        private bool ArePositionsCorrect(Position a, Position b)
        {
            return a.IsPositionCorrect() && b.IsPositionCorrect();
        }
    }
}
