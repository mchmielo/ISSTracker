namespace ISSTracker.Model
{
    public class ISSPosition
    {
        public string Message { get; set; }
        public int Timestamp { get; set; }
        public Position ISS_Position { get; set; }
    }
}
