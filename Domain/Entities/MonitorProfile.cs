namespace WPFTest.Domain.Entities;

public class MonitorProfile
{
    public string Id { get; init; } = string.Empty;
    public string ProfileName { get; set; } = null!;
    public Dictionary<MonitorType, MonitorSetting> MonitorSettings { get; set; }

    public MonitorProfile()
    {
        Id = Guid.NewGuid().ToString();
        MonitorSettings = new Dictionary<MonitorType, MonitorSetting>();
    }
}