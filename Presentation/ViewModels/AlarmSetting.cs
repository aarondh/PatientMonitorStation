namespace WPFTest.Presentation.ViewModels;

public class AlarmSetting : ViewModelBase
{
    private Range _normalRange = new();
    private string _alarmColor = "#E74C3C";

    public Range NormalRange
    {
        get => _normalRange;
        set => SetProperty(ref _normalRange, value);
    }

    public string AlarmColor
    {
        get => _alarmColor;
        set => SetProperty(ref _alarmColor, value);
    }
}
