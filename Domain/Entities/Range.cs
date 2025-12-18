namespace WPFTest.Domain.Entities;

public class Range
{
    public double Min { get; set; }

    public double Max { get; set; }

    public Range()
    {
    }

    public Range(double min, double max)
    {
        Min = min;
        Max = max;
    }
}
