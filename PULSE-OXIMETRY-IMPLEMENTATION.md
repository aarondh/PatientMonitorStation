# Pulse Oximetry Monitor Implementation

The application now includes a fake pulse oximetry monitor that tracks two critical vital signs: oxygen saturation (SpO2) and pulse rate.

## What is Pulse Oximetry?

Pulse oximetry is a non-invasive method for monitoring a patient's oxygen saturation (SpO2) and pulse rate. It uses light absorption to measure the oxygen level in the blood and detects the pulse simultaneously.

## Vital Signs Monitored

### 1. **SpO2 (Oxygen Saturation)**
- **Normal Range**: 95-100%
- **Generated Range**: 90-100%
- **Update Frequency**: Every 1 second
- **Clinical Significance**:
  - ≥95%: Normal oxygenation
  - 90-94%: Mild hypoxemia
  - <90%: Significant hypoxemia requiring intervention

### 2. **Pulse Rate**
- **Normal Range**: 60-100 BPM (at rest)
- **Generated Range**: 40-180 BPM
- **Update Frequency**: Every 1 second
- **Correlation**: Should generally match the heart rate from ECG

## Physiological Simulation

The FakePulseOximetryMonitor generates realistic variations:

### SpO2 Variations
- **Breathing Effect** (±0.5%): Small fluctuations with each breath
- **Activity Variation** (±1.5%): Changes due to patient movement or activity
- **Slow Drift** (±1%): Gradual changes over time
- **Measurement Noise** (±0.5%): Sensor/measurement variability

### Pulse Rate Variations
- **Respiratory Sinus Arrhythmia** (±3 BPM): Heart rate increases with inhalation
- **Activity Effect** (±8 BPM): Higher variations due to patient activity
- **Slow Drift** (±5 BPM): Gradual baseline changes
- **Noise** (±2 BPM): Beat-to-beat variability

## Display

The SpO2 reading is displayed in **purple** (#9B59B6) in a floating badge at the top-right of each patient room, next to the respiratory rate display.

### Display Format
```
┌──────────┐  ┌────────┐
│ SpO2     │  │ RESP   │
│   97 %   │  │  14/min│
└──────────┘  └────────┘
```

## Technical Implementation

### Domain Layer
- [PulseOximetryReading.cs](Domain/Entities/PulseOximetryReading.cs): Entity containing SpO2 and pulse rate
- [IPulseOximetryMonitor.cs](Domain/Ports/IPulseOximetryMonitor.cs): Port interface for pulse oximetry monitoring

### Infrastructure Layer
- [FakePulseOximetryMonitor.cs](Infrastructure/PulseOximetryMonitor/FakePulseOximetryMonitor.cs):
  - Generates realistic SpO2 values (90-100%)
  - Generates correlated pulse rate (40-180 BPM)
  - Updates every 1 second (typical for pulse oximeters)
  - Includes multiple physiological variation patterns

### Application Layer
- [MonitoringService.cs](Application/Services/MonitoringService.cs): Added MonitorPulseOximetry method

### Presentation Layer
- [RoomMonitorViewModel.cs](Presentation/ViewModels/RoomMonitorViewModel.cs): Added CurrentSpO2 and CurrentPulseRate properties
- [MainWindow.xaml](MainWindow.xaml): Added SpO2 display badge

## Clinical Context

In a real medical setting, pulse oximetry:
- **Continuous Monitoring**: Provides constant feedback on oxygenation status
- **Early Warning**: Can detect respiratory compromise before visual signs appear
- **Non-invasive**: No needles or blood draws required
- **Dual Measurement**: Simultaneously measures both oxygenation and pulse
- **Critical Care**: Essential in ICU, OR, emergency, and post-operative settings

## Integration with Other Monitors

The pulse oximeter works alongside:
- **5-Lead ECG**: Pulse rate should correlate with heart rate from Lead II
- **Blood Pressure Monitor**: Low SpO2 may indicate need for BP assessment
- **Respiratory Monitor**: Low SpO2 correlates with inadequate respiratory rate
- All monitors update independently with realistic timing intervals
