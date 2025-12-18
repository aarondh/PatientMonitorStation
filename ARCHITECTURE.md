# Heart Monitor Dashboard - Architecture

This WPF application demonstrates hexagonal architecture (ports and adapters) with real-time heart rate monitoring for multiple patient rooms.

## Architecture Overview

### Hexagonal Architecture Layers

```
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
│    (Adapters - Fake Data Generator, Persistence)   │
└─────────────────────────────────────────────────────┘
```

## Project Structure

### Domain Layer (`Domain/`)
- **Entities**: Core business objects
  - `HeartRateReading`: Represents a single heart rate measurement
  - `Room`: Represents a patient room

- **Ports** (Interfaces): Define contracts for external dependencies
  - `IHeartRateMonitor`: Interface for heart rate monitoring
  - `IRoomRepository`: Interface for room data access

### Application Layer (`Application/`)
- **Services**: Business logic orchestration
  - `MonitoringService`: Coordinates heart rate monitoring and room management

### Infrastructure Layer (`Infrastructure/`)
- **Adapters**: Implementations of domain ports
  - `FakeHeartRateMonitor`: Generates simulated heart rate data
  - `InMemoryRoomRepository`: Provides room data from memory

### Presentation Layer (`Presentation/`)
- **ViewModels**: MVVM pattern view models
  - `MainViewModel`: Main window view model
  - `RoomMonitorViewModel`: Individual room monitoring view model
  - `ViewModelBase`: Base class for view models with INotifyPropertyChanged

- **Controls**: Custom WPF controls
  - `MonitorGraph`: Real-time graph visualization control

## Key Features

1. **Hexagonal Architecture**: Clear separation of concerns with dependency inversion
2. **MVVM Pattern**: Clean separation between UI and business logic
3. **Dependency Injection**: Using Microsoft.Extensions.DependencyInjection
4. **Reactive Programming**: Using System.Reactive for real-time data streams
5. **Real-time Visualization**: Custom graph control for heart rate display

## Running the Application

The application displays 4 patient rooms (101-104), each with:
- Room name and patient information
- Real-time heart rate graph
- Current BPM (beats per minute) display

The fake heart rate generator produces realistic variations (40-180 BPM) at 100ms intervals.

## Dependencies

- .NET 9.0
- WPF (Windows Presentation Foundation)
- Microsoft.Extensions.DependencyInjection (9.0.0)
- System.Reactive (6.0.1)
