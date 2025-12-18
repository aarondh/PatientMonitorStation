namespace WPFTest.Presentation.ViewModels;

public class VitalSignSetting : ViewModelBase
{
    private VitalSignState _state = VitalSignState.Both;
    private AlarmSetting _alarm = new();

    public VitalSignState State
    {
        get => _state;
        set => SetProperty(ref _state, value);
    }

    public AlarmSetting Alarm
    {
        get => _alarm;
        set => SetProperty(ref _alarm, value);
    }
}
