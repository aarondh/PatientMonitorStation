namespace WPFTest.Domain.Entities;

public class EcgReading
{
    public DateTime Timestamp { get; init; }
    public int Value { get; init; }
    public int LeadI { get; init; }
    public int LeadII { get; init; }
    public int LeadIII { get; init; }
    public int LeadAVR { get; init; }
    public int LeadAVL { get; init; }

    public EcgReading(DateTime timestamp, int value, int leadI, int leadII, int leadIII, int leadAVR, int leadAVL)
    {
        Timestamp = timestamp;
        Value = value;
        LeadI = leadI;
        LeadII = leadII;
        LeadIII = leadIII;
        LeadAVR = leadAVR;
        LeadAVL = leadAVL;
    }
}
