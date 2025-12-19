using WPFPatientMonitor.Domain.Entities;

namespace WPFPatientMonitor.Domain.Ports;

public interface IPulseOximetryParameter
{
    IObservable<PulseOximetryReading> GetPulseOximetryStream(string patientId);
    void StartMonitoring(string patientId);
    void StopMonitoring(string patientId);
}
