namespace WPFTest.Domain.Entities;

public class MonitorSetting
{
    public MonitorViewState State { get; set; } = MonitorViewState.Both;
    public string StrokeColor { get; set; } = "#2ECC71";
    public string FillColor { get; set; } = "#27AE60";
    public string BorderColor { get; set; } = "#34495E";
    public AlarmSetting Alarm { get; set; } = new();
}
