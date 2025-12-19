namespace WPFPatientMonitor.Domain.Entities;

public class PulseOximetryReading
{
    public DateTime Timestamp { get; init; }
    public int SpO2 { get; init; }
    public int PulseRate { get; init; }

    public PulseOximetryReading(DateTime timestamp, int spO2, int pulseRate)
    {
        Timestamp = timestamp;
        SpO2 = spO2;
        PulseRate = pulseRate;
    }
}
