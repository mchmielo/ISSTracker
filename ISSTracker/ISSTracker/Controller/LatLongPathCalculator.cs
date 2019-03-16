using ISSTracker.Model;
using System;

namespace ISSTracker.Controller
{
    public class LatLongPathCalculator
    {
        const double R = 6779e3;    // Earth radius + 408 km ISS's altitude (in metres)

        public double CalculatePath(Position a, Position b)
        {
            if(!ArePositionsCorrect(a, b))
            {
                throw new ArgumentOutOfRangeException($"Incorrect passed positions.");
            }
            return 0;
        }

        private bool ArePositionsCorrect(Position a, Position b)
        {
            return a.IsPositionCorrect() && b.IsPositionCorrect();
        }
    }
}
