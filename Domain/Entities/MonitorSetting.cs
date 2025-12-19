using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPFPatientMonitor.Domain.Entities;

public class MonitorSetting : INotifyPropertyChanged
{
    private MonitorViewState _state = MonitorViewState.Both;
    private string _label = string.Empty;
    private string _graphLabel = string.Empty;
    private string _unit = string.Empty;
    private string _strokeColor = "#2ECC71";
    private string _fillColor = "#27AE60";
    private string _borderColor = "#34495E";
    private AlarmSetting _alarm = new();

    public MonitorViewState State
    {
        get => _state;
        set => SetProperty(ref _state, value);
    }

    public string Label
    {
        get => _label;
        set => SetProperty(ref _label, value);
    }

    public string GraphLabel
    {
        get => _graphLabel;
        set => SetProperty(ref _graphLabel, value);
    }

    public string Unit
    {
        get => _unit;
        set => SetProperty(ref _unit, value);
    }

    public string StrokeColor
    {
        get => _strokeColor;
        set => SetProperty(ref _strokeColor, value);
    }

    public string FillColor
    {
        get => _fillColor;
        set => SetProperty(ref _fillColor, value);
    }

    public string BorderColor
    {
        get => _borderColor;
        set => SetProperty(ref _borderColor, value);
    }

    public AlarmSetting Alarm
    {
        get => _alarm;
        set => SetProperty(ref _alarm, value);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
            return false;

        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}
