using WPFTest.Domain.Entities;

namespace WPFTest.Domain.Ports;

public interface IPulseOximetryMonitor
{
    IObservable<PulseOximetryReading> GetPulseOximetryStream(string patientId);
    void StartMonitoring(string patientId);
    void StopMonitoring(string patientId);
}
