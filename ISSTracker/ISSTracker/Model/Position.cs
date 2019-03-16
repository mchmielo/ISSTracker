using System;

namespace ISSTracker.Model
{
    public class Position
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public Position(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public bool IsPositionCorrect()
        {
            return (Latitude >= -90 && Latitude <= 90) && (Longitude >= -180 && Longitude <= 180);
        }
    }
}
