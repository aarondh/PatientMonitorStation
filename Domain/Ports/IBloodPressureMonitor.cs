using WPFTest.Domain.Entities;

namespace WPFTest.Domain.Ports;

public interface IBloodPressureMonitor
{
    IObservable<BloodPressureReading> GetBloodPressureStream(string patientId);
    void StartMonitoring(string patientId);
    void StopMonitoring(string patientId);
}
