namespace WPFTest.Domain.Entities;

public class RespiratoryReading
{
    public DateTime Timestamp { get; init; }
    public int Rate { get; init; }

    public RespiratoryReading(DateTime timestamp, int rate)
    {
        Timestamp = timestamp;
        Rate = rate;
    }
}
