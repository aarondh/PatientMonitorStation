# Patient Monitoring System - Architecture

This WPF application demonstrates hexagonal architecture (ports and adapters) with real-time multi-parameter patient monitoring for hospital environments.

## Architecture Overview

### Hexagonal Architecture Layers

```text
┌─────────────────────────────────────────────────────┐
│                 Presentation Layer                  │
│  (Views, ViewModels, Controls - WPF UI Components) │
└───────────────────┬─────────────────────────────────┘
                    │
┌───────────────────▼─────────────────────────────────┐
│                Application Layer                    │
│         (Use Cases, Business Services)              │
└───────────────────┬─────────────────────────────────┘
                    │
┌───────────────────▼─────────────────────────────────┐
│                  Domain Layer                       │
│      (Entities, Interfaces/Ports - Pure Logic)      │
└───────────────────┬─────────────────────────────────┘
                    │
┌───────────────────▼─────────────────────────────────┐
│              Infrastructure Layer                   │
│   (Adapters - Parameter Adapters, Persistence)     │
└─────────────────────────────────────────────────────┘
```

## Project Structure

### Domain Layer (`Domain/`)

#### Entities: Core business objects

- `Patient`: Patient demographic and clinical information
- `Room`: Hospital room entity
- `EpisodeOfCare`: Patient care episode linking patient, room, and monitor profile
- `MonitorProfile`: Named set of monitor settings
- `MonitorSetting`: Configuration for individual vital sign parameters (ranges, alarms)
- `MonitorType`: Enumeration of monitor types (HeartRate, BloodPressure, RespiratoryRate, SpO2, PulseRate)
- `MonitorViewState`: UI state for monitor visibility/layout

**Vital Sign Readings:**

- `EcgReading`: 5-lead ECG data (LeadI, LeadII, LeadIII, LeadAVR, LeadAVL, heart rate)
- `BloodPressureReading`: Systolic, diastolic, and mean arterial pressure
- `RespiratoryReading`: Respiratory rate
- `PulseOximetryReading`: SpO2 oxygen saturation and pulse rate

**Supporting Entities:**

- `AlarmSetting`: Alarm configuration (enabled, limits)
- `Range`: Value range for alarm thresholds
- `Period`: Time period representation

#### Ports (Interfaces): Define contracts for external dependencies

**Parameter Monitoring Ports:**

- `IEcgParameters`: ECG/heart rate monitoring interface
- `IBloodPressureParameter`: Blood pressure monitoring interface
- `IRespiratoryParameter`: Respiratory rate monitoring interface
- `IPulseOximetryParameter`: Pulse oximetry monitoring interface

**Repository Ports:**

- `IRoomRepository`: Room data access interface
- `IPatientRepository`: Patient and episode of care data access interface

### Application Layer (`Application/`)

#### Services: Business logic orchestration

- `MonitoringService`: Central orchestration service that coordinates all vital sign monitoring and data retrieval
  - Manages ECG, blood pressure, respiratory, and pulse oximetry streams
  - Provides access to rooms, patients, episodes of care, and monitor profiles
  - Handles start/stop of monitoring per patient

### Infrastructure Layer (`Infrastructure/`)

#### Adapters: Implementations of domain ports

**Parameter Adapters** (`Infrastructure/Adapters/`):

- `FakeEcgParametersAdapter`: Simulates realistic 5-lead ECG waveforms with physiological variations
- `FakeBloodPressureParameterAdapter`: Generates realistic blood pressure readings
- `FakeRespiratoryParameterAdapter`: Simulates respiratory rate variations
- `FakePulseOximetryParameterAdapter`: Generates SpO2 and pulse rate data

Each adapter:

- Implements the hexagonal architecture adapter pattern
- Uses Reactive Extensions (System.Reactive) for observable streams
- Manages per-patient monitoring sessions
- Includes internal generators for physiologically accurate simulation
- Implements `IDisposable` for proper resource cleanup

**Persistence** (`Infrastructure/Persistence/`):

- `InMemoryRoomRepository`: In-memory room data storage
- `InMemoryPatientRepository`: In-memory patient, episode of care, and monitor profile storage

### Presentation Layer (`Presentation/`)

#### ViewModels: MVVM pattern view models

- `MainViewModel`: Main window orchestration
- `PatientMonitorViewModel`: Individual patient monitoring view model with real-time vital sign data
- `PatientDetailsViewModel`: Patient demographic and episode of care management
- `MonitorSettingViewModel`: Monitor configuration view model
- `ViewModelBase`: Base class for view models with `INotifyPropertyChanged`

#### Views: WPF Windows and Controls

- `MainWindow`: Primary application window
- `PatientDetailsWindow`: Patient information and monitor settings editor

#### Controls: Custom WPF controls

- `MonitorGraph`: Real-time graph visualization control for waveform display
- `MonitorSettingEditor`: UI control for editing monitor settings
- `PatientMonitorsControl`: Composite control displaying all patient vital signs

## Key Features

1. **Hexagonal Architecture**: Clear separation of concerns with dependency inversion
2. **MVVM Pattern**: Clean separation between UI and business logic
3. **Dependency Injection**: Using Microsoft.Extensions.DependencyInjection
4. **Reactive Programming**: Using System.Reactive for real-time data streams
5. **Multi-Parameter Monitoring**: Simultaneous monitoring of ECG, blood pressure, respiratory rate, and pulse oximetry
6. **Real-time Visualization**: Custom graph controls for waveform display
7. **Configurable Alarms**: Per-parameter alarm settings with high/low thresholds
8. **Episode of Care**: Healthcare domain modeling with episodes linking patients to rooms

## Monitoring Parameters

### ECG (Electrocardiogram)

- 5-lead ECG system (Lead I, Lead II, Lead III, aVR, aVL)
- 100 Hz sampling rate (10ms intervals)
- Realistic PQRST waveform generation
- Physiological variations (respiratory sinus arrhythmia, slow drift, noise)
- See [5-LEAD-ECG-IMPLEMENTATION.md](5-LEAD-ECG-IMPLEMENTATION.md)

### Blood Pressure

- Systolic, diastolic, and mean arterial pressure
- Periodic readings with realistic intervals
- Physiological variations based on activity and position

### Respiratory Rate

- Breaths per minute monitoring
- Realistic variation patterns
- Integration with other vital signs

### Pulse Oximetry

- SpO2 oxygen saturation (90-100%)
- Pulse rate (40-180 BPM)
- 1-second update frequency
- Correlated with ECG heart rate
- See [PULSE-OXIMETRY-IMPLEMENTATION.md](PULSE-OXIMETRY-IMPLEMENTATION.md)

## Dependency Injection Configuration

Services are configured in [App.xaml.cs](App.xaml.cs):

- **Singleton Services**: Parameter adapters, repositories, MonitoringService
- **Transient Services**: ViewModels, Windows
- **Proper Disposal**: All disposable services cleaned up on application exit

## Running the Application

The application displays patient rooms with:

- Patient demographic information
- Real-time multi-parameter monitoring
- Configurable monitor settings and alarm thresholds
- Episode of care management
- Monitor profile selection

All vital sign generators produce realistic physiological variations with appropriate update frequencies.

## Dependencies

- .NET 9.0
- WPF (Windows Presentation Foundation)
- Microsoft.Extensions.DependencyInjection (9.0.0)
- System.Reactive (6.0.1)

## Additional Documentation

- [5-Lead ECG Implementation](5-LEAD-ECG-IMPLEMENTATION.md)
- [Pulse Oximetry Implementation](PULSE-OXIMETRY-IMPLEMENTATION.md)
