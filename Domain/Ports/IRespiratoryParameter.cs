using WPFPatientMonitor.Domain.Entities;

namespace WPFPatientMonitor.Domain.Ports;

public interface IRespiratoryParameter
{
    IObservable<RespiratoryReading> GetRespiratoryStream(string patientId);
    void StartMonitoring(string patientId);
    void StopMonitoring(string patientId);
}
