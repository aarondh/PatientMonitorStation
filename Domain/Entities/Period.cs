namespace WPFPatientMonitor.Domain.Entities;

public class Period
{
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }

    public Period()
    {
    }

    public Period(DateTimeOffset startDate, DateTimeOffset endDate)
    {
        if (endDate < startDate)
        {
            throw new ArgumentException("End date must be greater than or equal to start date.");
        }

        StartDate = startDate;
        EndDate = endDate;
    }

    public TimeSpan Duration => EndDate - StartDate;

    public bool Contains(DateTime date)
    {
        return date >= StartDate && date <= EndDate;
    }

    public bool Overlaps(Period other)
    {
        return StartDate <= other.EndDate && EndDate >= other.StartDate;
    }
}
