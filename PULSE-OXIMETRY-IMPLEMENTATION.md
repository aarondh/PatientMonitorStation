# Pulse Oximetry Parameter Implementation

The application now includes a fake pulse oximetry parameter adapter that tracks two critical vital signs: oxygen saturation (SpO2) and pulse rate.

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

The FakePulseOximetryParameterAdapter (via its internal PulseOxGenerator) generates realistic variations:

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

The SpO2 and Pulse Rate readings are displayed as part of the patient monitoring interface, updated in real-time through observable streams.

## Technical Implementation

### Domain Layer

- [PulseOximetryReading.cs](Domain/Entities/PulseOximetryReading.cs): Entity containing SpO2 and pulse rate with timestamp
- [IPulseOximetryParameter.cs](Domain/Ports/IPulseOximetryParameter.cs): Port interface defining the parameter adapter contract
  - `IObservable<PulseOximetryReading> GetPulseOximetryStream(string patientId)`: Returns reactive stream of readings
  - `void StartMonitoring(string patientId)`: Initiates monitoring for a specific patient
  - `void StopMonitoring(string patientId)`: Stops monitoring for a specific patient

### Infrastructure Layer

- [FakePulseOximetryParameterAdapter.cs](Infrastructure/Adapters/PulseOximetryParameter/FakePulseOximetryParameterAdapter.cs):
  - Implements the hexagonal architecture adapter pattern
  - Manages per-patient observable streams using Reactive Extensions (Rx)
  - Contains internal `PulseOxGenerator` class for physiological simulation
  - Generates realistic SpO2 values (90-100%)
  - Generates correlated pulse rate (40-180 BPM)
  - Updates every 1 second (typical for pulse oximeters)
  - Includes multiple physiological variation patterns
  - Implements `IDisposable` for proper resource cleanup

### Application Layer

- [MonitoringService.cs](Application/Services/MonitoringService.cs:84-88):
  - `MonitorPulseOximetry(string patientId)` method orchestrates monitoring
  - Starts monitoring and returns the observable stream
  - Integrates with other monitoring parameters (ECG, BP, Respiratory)

### Presentation Layer

- [PatientMonitorViewModel.cs](Presentation/ViewModels/PatientMonitorViewModel.cs):
  - Properties: `CurrentSpO2` and `CurrentPulseRate` for data binding
  - Subscribes to pulse oximetry stream in `StartMonitoring()` method (lines 254-263)
  - Updates UI on dispatcher thread for thread-safe WPF binding
  - Proper disposal pattern for unsubscribing from streams

## Clinical Context

In a real medical setting, pulse oximetry:
- **Continuous Monitoring**: Provides constant feedback on oxygenation status
- **Early Warning**: Can detect respiratory compromise before visual signs appear
- **Non-invasive**: No needles or blood draws required
- **Dual Measurement**: Simultaneously measures both oxygenation and pulse
- **Critical Care**: Essential in ICU, OR, emergency, and post-operative settings

## Integration with Other Parameters

The pulse oximetry parameter works alongside other monitoring parameters in a unified system:

- **ECG Parameters**: Pulse rate should correlate with heart rate from Lead II
- **Blood Pressure Parameter**: Low SpO2 may indicate need for BP assessment
- **Respiratory Parameter**: Low SpO2 correlates with inadequate respiratory rate
- All parameters update independently with realistic timing intervals
- Each parameter follows the hexagonal architecture pattern with domain ports and infrastructure adapters
- Observable streams enable reactive, real-time updates across the entire monitoring system
