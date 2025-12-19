using System;
using WPFPatientMonitor.Domain.Entities;

namespace WPFPatientMonitor.Presentation.ViewModels;

public class MonitorSettingViewModel : ViewModelBase
{
    private readonly MonitorSetting _monitorSetting;

    public MonitorSettingViewModel(MonitorSetting monitorSetting)
    {
        _monitorSetting = monitorSetting ?? throw new ArgumentNullException(nameof(monitorSetting));

        // Subscribe to property changes from the underlying entity
        _monitorSetting.PropertyChanged += (s, e) => OnPropertyChanged(e.PropertyName);
    }

    public MonitorSetting MonitorSetting => _monitorSetting;

    public string Label
    {
        get => _monitorSetting.Label;
        set => _monitorSetting.Label = value;
    }

    public string GraphLabel
    {
        get => _monitorSetting.GraphLabel;
        set => _monitorSetting.GraphLabel = value;
    }

    public string Unit
    {
        get => _monitorSetting.Unit;
        set => _monitorSetting.Unit = value;
    }

    public MonitorViewState State
    {
        get => _monitorSetting.State;
        set => _monitorSetting.State = value;
    }

    public string StrokeColor
    {
        get => _monitorSetting.StrokeColor;
        set => _monitorSetting.StrokeColor = value;
    }

    public string FillColor
    {
        get => _monitorSetting.FillColor;
        set => _monitorSetting.FillColor = value;
    }

    public string BorderColor
    {
        get => _monitorSetting.BorderColor;
        set => _monitorSetting.BorderColor = value;
    }

    public AlarmSetting Alarm
    {
        get => _monitorSetting.Alarm;
        set => _monitorSetting.Alarm = value;
    }

    public Array ViewStates => Enum.GetValues(typeof(MonitorViewState));
}
