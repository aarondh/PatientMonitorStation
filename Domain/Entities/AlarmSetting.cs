using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPFTest.Domain.Entities;

public class AlarmSetting : INotifyPropertyChanged
{
    private Range _normalRange = new Range();
    private Range _warningRange = new Range();
    private Range _criticalRange = new Range();
    private bool _isEnabled = false;
    private string _warningColor = "#E74C3C";
    private string _criticalColor = "#C0392B";

    public Range NormalRange
    {
        get => _normalRange;
        set => SetProperty(ref _normalRange, value);
    }

    public Range WarningRange
    {
        get => _warningRange;
        set => SetProperty(ref _warningRange, value);
    }

    public Range CriticalRange
    {
        get => _criticalRange;
        set => SetProperty(ref _criticalRange, value);
    }

    public bool IsEnabled
    {
        get => _isEnabled;
        set => SetProperty(ref _isEnabled, value);
    }

    public string WarningColor
    {
        get => _warningColor;
        set => SetProperty(ref _warningColor, value);
    }

    public string CriticalColor
    {
        get => _criticalColor;
        set => SetProperty(ref _criticalColor, value);
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
