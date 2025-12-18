namespace WPFTest.Domain.Entities;

public class AlarmSetting
{
    public Range NormalRange { get; set; } = new Range();
    public Range WarningRange { get; set; } = new Range();
    public Range CriticalRange { get; set; } = new Range();
    public bool IsEnabled { get; set; } = false;
    public string WarningColor { get; set; } = "#E74C3C";
    public string CriticalColor { get; set; } = "#C0392B";
}
