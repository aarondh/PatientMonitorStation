using WPFPatientMonitor.Domain.Entities;

namespace WPFPatientMonitor.Domain.Ports;

public interface IBloodPressureParameter
{
    IObservable<BloodPressureReading> GetBloodPressureStream(string patientId);
    void StartMonitoring(string patientId);
    void StopMonitoring(string patientId);
}
