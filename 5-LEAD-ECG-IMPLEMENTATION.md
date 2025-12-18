# 5-Lead ECG Implementation

The fake heart monitor has been updated to simulate a 5-lead ECG system, providing multiple perspectives of the heart's electrical activity.

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

Each patient room displays all 6 waveforms in a scrollable view:
- Lead I (green)
- Lead II (green)
- Lead III (green)
- aVR (green)
- aVL (green)
- RESP (red) - Respiratory rate

All waveforms update in real-time at 100 Hz (10ms intervals) for smooth visualization.

## Technical Implementation

### Domain Layer
- [HeartRateReading.cs](Domain/Entities/HeartRateReading.cs): Extended to include all 5 lead values

### Infrastructure Layer
- [FakeHeartRateMonitor.cs](Infrastructure/HeartRateMonitor/FakeHeartRateMonitor.cs):
  - Generates synchronized waveforms for all leads
  - Each lead uses different amplitude scaling
  - aVR uses inverted polarity
  - Complete PQRST complex simulation

### Presentation Layer
- [RoomMonitorViewModel.cs](Presentation/ViewModels/RoomMonitorViewModel.cs): Tracks data for all 5 leads
- [MainWindow.xaml](MainWindow.xaml): Displays all leads in labeled, scrollable graphs

## Medical Accuracy

The simulation provides educationally accurate ECG patterns:
- Proper phase relationships between leads
- Realistic amplitude variations based on lead positioning
- Inverted aVR waveform (as seen in real ECG)
- Complete cardiac cycle with all components
- Physiologically plausible heart rate variability
