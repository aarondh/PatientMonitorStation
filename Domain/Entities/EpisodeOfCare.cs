namespace WPFTest.Domain.Entities;

public class EpisodeOfCare
{
    public string PatientId { get; init; } = string.Empty;
    public Patient Patient { get; set; } = null!;
    public string RoomId { get; set; } = string.Empty;
    public Room Room { get; set; } = null!;

    public Period Period { get; set; } = new Period();

    public string MonitorProfileId { get; set; } = string.Empty;
    public MonitorProfile MonitorProfile { get; set; } = new MonitorProfile();
    public Dictionary<MonitorType, MonitorSetting> MonitorSettings { get; set; } = new Dictionary<MonitorType, MonitorSetting>();

    public EpisodeOfCare()
    {
    }
}
