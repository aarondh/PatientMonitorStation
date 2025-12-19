# 5-Lead ECG Parameter Implementation

The ECG parameter adapter has been implemented to simulate a 5-lead ECG system, providing multiple perspectives of the heart's electrical activity.

## ECG Leads Implemented

### 1. **Lead I** (Limb Lead)
- Measures voltage between left arm and right arm
- Amplitude: 70% of reference
- Views lateral wall of left ventricle

### 2. **Lead II** (Limb Lead)
- Measures voltage between left leg and right arm
- Amplitude: 100% (reference lead)
- Most commonly used for rhythm analysis
- Best view of P waves and overall cardiac rhythm

### 3. **Lead III** (Limb Lead)
- Measures voltage between left leg and left arm
- Amplitude: 50% of reference
- Views inferior wall of heart

### 4. **aVR** (Augmented Limb Lead)
- Augmented voltage from right arm
- Amplitude: -60% (inverted)
- Unique inverted waveform (all deflections typically negative)
- Views right atrium and cavity of left ventricle

### 5. **aVL** (Augmented Limb Lead)
- Augmented voltage from left arm
- Amplitude: 40% of reference
- Views lateral wall of left ventricle

## ECG Waveform Components

Each lead displays the complete cardiac cycle:

### **P Wave** (Atrial Depolarization)
- Duration: ~5% of cardiac cycle
- Amplitude: 4x base amplitude
- Represents atrial contraction

### **PR Segment** (AV Node Delay)
- Duration: ~10% of cardiac cycle
- Low amplitude: 1.5x base amplitude
- Electrical pause at AV node

### **QRS Complex** (Ventricular Depolarization)
- Duration: ~10% of cardiac cycle
- Three phases:
  - **Q wave**: Small negative deflection (-10x amplitude)
  - **R wave**: Sharp positive spike (35x amplitude peak)
  - **S wave**: Return to baseline
- Most prominent feature of ECG

### **ST Segment** (Early Ventricular Repolarization)
- Duration: ~5% of cardiac cycle
- Low amplitude: 1x base amplitude
- Baseline between QRS and T wave

### **T Wave** (Ventricular Repolarization)
- Duration: ~20% of cardiac cycle
- Amplitude: 10x base amplitude
- Smooth rounded wave

### **Baseline** (Diastole)
- Remaining ~50% of cardiac cycle
- Low amplitude variations (2x amplitude)
- Electrical rest period

## Physiological Variations

All leads include realistic variations:
- **Respiratory Sinus Arrhythmia**: Heart rate varies with breathing (±3 BPM, 0.25 Hz)
- **Slow Drift**: Long-term HR variations (±5 BPM, 0.05 Hz)
- **Random Noise**: Beat-to-beat variability (±1 BPM)

## Display Layout

The patient monitoring interface displays ECG waveforms with real-time updates at 100 Hz (10ms intervals) for smooth visualization. The waveform data is streamed through observable collections for reactive UI updates.

## Technical Implementation

### Domain Layer

- [EcgReading.cs](Domain/Entities/EcgReading.cs): Entity containing all ECG data
  - `Value`: Primary heart rate value
  - `LeadI`, `LeadII`, `LeadIII`: Limb lead waveform values
  - `LeadAVR`, `LeadAVL`: Augmented limb lead waveform values
  - `Timestamp`: Reading timestamp
- [IEcgParameters.cs](Domain/Ports/IEcgParameters.cs): Port interface defining the ECG parameter adapter contract
  - `IObservable<EcgReading> GetEcgParameterStream(string patientId)`: Returns reactive stream of ECG readings
  - `void StartMonitoring(string patientId)`: Initiates ECG monitoring for a specific patient
  - `void StopMonitoring(string patientId)`: Stops ECG monitoring for a specific patient

### Infrastructure Layer

- [FakeEcgParametersAdapter.cs](Infrastructure/Adapters/EcgParameters/FakeEcgParameterAdapter.cs):
  - Implements the hexagonal architecture adapter pattern
  - Manages per-patient observable streams using Reactive Extensions (Rx)
  - Contains internal `HeartBeatGenerator` class for ECG waveform simulation
  - Updates at 100 Hz (10ms intervals) for smooth real-time waveforms
  - Generates synchronized waveforms for all 5 leads with proper amplitude scaling
  - Each lead uses different amplitude scaling (Lead I: 0.7, Lead II: 1.0, Lead III: 0.5, aVR: -0.6, aVL: 0.4)
  - aVR uses inverted polarity (-0.6) as seen in real ECG systems
  - Complete PQRST complex simulation with physiologically accurate phase durations
  - Implements `IDisposable` for proper resource cleanup

### Application Layer

- [MonitoringService.cs](Application/Services/MonitoringService.cs:66-70):
  - `MonitorHeartRate(string patientId)` method orchestrates ECG monitoring
  - Starts monitoring and returns the observable stream
  - Integrates with other monitoring parameters (BP, Respiratory, Pulse Oximetry)

### Presentation Layer

- [PatientMonitorViewModel.cs](Presentation/ViewModels/PatientMonitorViewModel.cs):
  - Property: `CurrentHeartRate` for displaying current BPM value
  - Observable collections for each lead: `HeartRateData`, `LeadIData`, `LeadIIData`, `LeadIIIData`, `LeadAVRData`, `LeadAVLData`
  - Subscribes to ECG stream in `StartMonitoring()` method (lines 200-225)
  - Updates UI on dispatcher thread for thread-safe WPF binding
  - Maintains sliding window of max 500 data points per lead for efficient rendering
  - Proper disposal pattern for unsubscribing from streams

## Medical Accuracy

The simulation provides educationally accurate ECG patterns:
- Proper phase relationships between leads
- Realistic amplitude variations based on lead positioning
- Inverted aVR waveform (as seen in real ECG)
- Complete cardiac cycle with all components
- Physiologically plausible heart rate variability
