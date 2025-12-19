namespace WPFPatientMonitor.Domain.Entities;

public class BloodPressureReading
{
    public DateTime Timestamp { get; init; }
    public int Systolic { get; init; }
    public int Diastolic { get; init; }

    public BloodPressureReading(DateTime timestamp, int systolic, int diastolic)
    {
        Timestamp = timestamp;
        Systolic = systolic;
        Diastolic = diastolic;
    }
}
