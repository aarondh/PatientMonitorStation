using System.Windows;
using System.Windows.Controls;
using WPFTest.Domain.Entities;

namespace WPFTest.Presentation.Controls;

public partial class MonitorSettingEditor : UserControl
{
    public static readonly DependencyProperty MonitorNameProperty =
        DependencyProperty.Register(
            nameof(MonitorName),
            typeof(string),
            typeof(MonitorSettingEditor),
            new PropertyMetadata(string.Empty));

    public static readonly DependencyProperty SettingProperty =
        DependencyProperty.Register(
            nameof(Setting),
            typeof(MonitorSetting),
            typeof(MonitorSettingEditor),
            new PropertyMetadata(null));

    public string MonitorName
    {
        get => (string)GetValue(MonitorNameProperty);
        set => SetValue(MonitorNameProperty, value);
    }

    public MonitorSetting Setting
    {
        get => (MonitorSetting)GetValue(SettingProperty);
        set => SetValue(SettingProperty, value);
    }

    public MonitorSettingEditor()
    {
        InitializeComponent();
        DataContext = this;
    }
}
