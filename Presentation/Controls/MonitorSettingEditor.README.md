# MonitorSettingEditor Control

A comprehensive WPF UserControl for editing all properties of a `MonitorSetting`, including its nested `AlarmSetting`.

## Features

- **View State Selection**: Dropdown to select monitor display state (None, Vital, Graph, Both)
- **Color Configuration**: Edit stroke, fill, and border colors with live preview
- **Alarm Settings**:
  - Enable/disable alarms
  - Configure Normal, Warning, and Critical ranges
  - Set Warning and Critical alarm colors
- **Real-time Updates**: All bindings use TwoWay mode for immediate updates

## Usage

```xaml
<controls:MonitorSettingEditor
    MonitorName="Heart Rate"
    Setting="{Binding PatientMonitor.MonitorSettings[HeartRate]}" />
```

## Properties

| Property | Type | Description |
|----------|------|-------------|
| MonitorName | string | Display name for the monitor (e.g., "Heart Rate", "SpO₂") |
| Setting | MonitorSetting | The MonitorSetting object to edit |

## Example

```xaml
<StackPanel>
    <controls:MonitorSettingEditor
        MonitorName="Heart Rate"
        Setting="{Binding HeartRateSetting}" />

    <controls:MonitorSettingEditor
        MonitorName="Blood Pressure"
        Setting="{Binding BloodPressureSetting}" />
</StackPanel>
```

## Styling

The control uses a dark theme that matches the application's color scheme:
- Background: `#252525`
- Border: `#444`
- Inputs: `#1a1a1a` background with `#fff` text
- Accent: `#2ECC71` for headers
- Alarm section: `#E74C3C` accent color
